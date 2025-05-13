using System;
using System.Collections.Generic;
using System.Data;
using DAL;
using DTO;

public class LuongBLL
{
    private LuongDAL luongDAL = new LuongDAL();

    // Lấy danh sách lương của tất cả nhân viên
    public List<LuongDTO> GetDanhSachLuong()
    {
        return luongDAL.GetDanhSachLuong();
    }

    // Lấy thông tin lương chi tiết cho nhân viên
    public DataRow GetLuongChiTiet(string maNV)
    {
        try
        {
            return luongDAL.GetLuongChiTiet(maNV);
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi BLL khi lấy thông tin lương chi tiết: {ex.Message}");
        }
    }
    // Lấy thông tin lương theo bậc
    public DataRow GetLuongTheoBac(string maBacLuong)
    {
        try
        {
            return luongDAL.GetLuongTheoBac(maBacLuong);  // Truyền maBacLuong vào thay vì maLuong
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi BLL khi lấy thông tin lương theo bậc: {ex.Message}");
        }
    }
    public bool CapNhatDuLieuLuong()
    {
        try
        {
            return luongDAL.ExecuteUpdateLuong();
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi khi gọi UpdateLuong: {ex.Message}");
        }
    }


}
