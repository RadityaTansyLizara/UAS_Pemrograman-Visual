using System.ComponentModel.DataAnnotations;

namespace BabyShopWeb2.Models
{
    public enum EmployeePosition
    {
        Kasir,
        Supervisor,
        Admin,
        Manager,
        StaffGudang
    }
    
    public enum EmployeeShift
    {
        Pagi,
        Siang
    }
    
    public enum EmployeeStatus
    {
        Aktif,
        Nonaktif,
        Cuti
    }
    
    public class Employee
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "ID Karyawan wajib diisi")]
        [StringLength(20)]
        public string EmployeeId { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Nama lengkap wajib diisi")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Jabatan wajib dipilih")]
        public EmployeePosition Position { get; set; }
        
        [Required(ErrorMessage = "No. HP wajib diisi")]
        [Phone(ErrorMessage = "Format nomor HP tidak valid")]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Alamat wajib diisi")]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Tanggal masuk wajib diisi")]
        public DateTime JoinDate { get; set; }
        
        [Required(ErrorMessage = "Status wajib dipilih")]
        public EmployeeStatus Status { get; set; }
        
        [Required(ErrorMessage = "Shift kerja wajib dipilih")]
        public EmployeeShift Shift { get; set; }
        
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        [StringLength(100)]
        public string? Email { get; set; }
        
        public decimal? Salary { get; set; }
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
