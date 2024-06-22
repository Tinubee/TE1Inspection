using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvUtils;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace TE1.Schemas
{
    public class 검사결과테이블 : Data.BaseTable
    {
        private TranslationAttribute 로그영역 = new TranslationAttribute("Inspection Data", "검사자료");
        private TranslationAttribute 삭제오류 = new TranslationAttribute("An error occurred while deleting data.", "자료 삭제중 오류가 발생하였습니다.");
        private DbSet<검사결과> 검사결과 { get; set; }
        private DbSet<검사정보> 검사정보 { get; set; }
        private DbSet<표면불량> 표면불량 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<검사결과>().Property(e => e.모델구분).HasConversion(new EnumToNumberConverter<모델구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.측정결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.치수결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.외관결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.큐알등급).HasConversion(new EnumToNumberConverter<큐알등급, Int32>());

            modelBuilder.Entity<검사정보>().HasKey(e => new { e.검사일시, e.검사항목 });
            modelBuilder.Entity<검사정보>().Property(e => e.검사항목).HasConversion(new EnumToNumberConverter<검사항목, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.검사그룹).HasConversion(new EnumToNumberConverter<검사그룹, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.검사장치).HasConversion(new EnumToNumberConverter<장치구분, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.측정단위).HasConversion(new EnumToNumberConverter<단위구분, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.측정결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());

            modelBuilder.Entity<표면불량>().Property(e => e.장치구분).HasConversion(new EnumToNumberConverter<장치구분, Int32>());
            base.OnModelCreating(modelBuilder);
        }

        public void Add(검사결과 정보)
        {
            this.검사결과.Add(정보);
            this.검사정보.AddRange(정보.검사내역);
            this.표면불량.AddRange(정보.표면불량);
        }

        public void Remove(List<검사정보> 자료) => this.검사정보.RemoveRange(자료);

        public void Save()
        {
            try { this.SaveChanges(); }
            catch (Exception ex) { Debug.WriteLine(ex.ToString(), "자료저장"); }
        }

        public void SaveAsync()
        {
            try { this.SaveChangesAsync(); }
            catch (Exception ex) { Debug.WriteLine(ex.ToString(), "자료저장"); }
        }

        public 검사결과 Select(DateTime 일시, 모델구분 모델, Int32 코드) => this.Select(일시, 일시, 모델, 코드).FirstOrDefault();
        public 검사결과 Select(DateTime 시작, DateTime 종료, 모델구분 모델, String 큐알, String serial) => this.Select(시작, 종료, 모델, 0, 큐알, serial).FirstOrDefault();
        public List<검사결과> Select(DateTime 시작, DateTime 종료, 모델구분 모델 = 모델구분.None, Int32 코드 = 0, String 큐알 = null, String serial = null)
        {
            IQueryable<검사결과> query1 =
                from l in 검사결과
                where l.검사일시 >= 시작 && l.검사일시 < 종료.AddDays(1)
                where (코드 <= 0 || l.검사번호 == 코드)
                where (모델 == 모델구분.None || l.모델구분 == 모델)
                where (String.IsNullOrEmpty(큐알) || l.큐알내용 == 큐알)
                where (String.IsNullOrEmpty(serial) || l.큐알내용.Contains(serial))
                orderby l.검사일시 descending
                select l;
            List<검사결과> 자료 = query1.AsNoTracking().ToList();
            if (자료 == null || 자료.Count < 1) return new List<검사결과>();

            IQueryable<검사정보> query2 =
                from d in 검사정보
                join l in 검사결과 on d.검사일시 equals l.검사일시
                where l.검사일시 >= 시작 && l.검사일시 < 종료.AddDays(1)
                where (코드 <= 0 || l.검사번호 == 코드)
                where (모델 == 모델구분.None || l.모델구분 == 모델)
                where (String.IsNullOrEmpty(큐알) || l.큐알내용 == 큐알)
                where (String.IsNullOrEmpty(serial) || l.큐알내용.Contains(serial))
                orderby d.검사일시 descending
                orderby d.검사항목 ascending
                select d;
            List<검사정보> 정보 = query2.AsNoTracking().ToList();

            IQueryable<표면불량> query3 =
                from d in 표면불량
                join l in 검사결과 on d.검사일시 equals l.검사일시
                where l.검사일시 >= 시작 && l.검사일시 < 종료.AddDays(1)
                where (코드 <= 0 || l.검사번호 == 코드)
                where (모델 == 모델구분.None || l.모델구분 == 모델)
                where (String.IsNullOrEmpty(큐알) || l.큐알내용 == 큐알)
                where (String.IsNullOrEmpty(serial) || l.큐알내용.Contains(serial))
                orderby d.검사일시 descending
                select d;
            List<표면불량> 표면 = query3.AsNoTracking().ToList();

            자료.ForEach(l => {
                if (정보 != null && 정보.Count > 0)
                    l.AddRange(정보.Where(d => d.검사일시 == l.검사일시).ToList());
                if (표면 != null && 표면.Count > 0)
                    l.AddRange(표면.Where(d => d.검사일시 == l.검사일시).ToList());
            });
            return 자료;
        }

        public Int32 Delete(검사결과 정보)
        {
            String Sql = $"DELETE FROM inspd WHERE idwdt = @idwdt;\nDELETE FROM insuf WHERE iswdt = @iswdt;\nDELETE FROM inspl WHERE ilwdt = @ilwdt;";
            try
            {
                int AffectedRows = 0;
                using (NpgsqlCommand cmd = new NpgsqlCommand(Sql, this.DbConn))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("@idwdt", 정보.검사일시));
                    cmd.Parameters.Add(new NpgsqlParameter("@iswdt", 정보.검사일시));
                    cmd.Parameters.Add(new NpgsqlParameter("@ilwdt", 정보.검사일시));
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    AffectedRows = cmd.ExecuteNonQuery();
                    Debug.WriteLine(Utils.FormatDate(정보.검사일시, "{0:yyyy-MM-dd HH:mm:ss.ffffff}"), "검사일시");
                    Debug.WriteLine(AffectedRows, "AffectedRows");
                    cmd.Connection.Close();
                }
                return AffectedRows;
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), Localization.삭제.GetString(), $"{삭제오류.GetString()}\r\n{ex.Message}", true);
            }
            return 0;
        }

        public Int32 자료정리(Int32 일수)
        {
            DateTime 일자 = DateTime.Today.AddDays(-일수);
            String day = Utils.FormatDate(일자, "{0:yyyy-MM-dd}");
            String sql = $"DELETE FROM inspd WHERE idwdt < DATE('{day}');\nDELETE FROM insuf WHERE iswdt < DATE('{day}');\nDELETE FROM inspl WHERE ilwdt < DATE('{day}')";
            try
            {
                int AffectedRows = 0;
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, this.DbConn))
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    AffectedRows = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                return AffectedRows;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Global.오류로그(로그영역.GetString(), "Remove Datas", ex.Message, false);
            }
            return -1;
        }
    }
}
