using System.Collections.Generic;
using System.Data;
using DAL;
using DTO;

public class LuongBLL
{
    private LuongDAL luongDAL = new LuongDAL();

    // Get salary data
    public List<LuongDTO> GetDanhSachLuong()
    {
        return luongDAL.GetDanhSachLuong();
    }

    // Get detailed salary data for a specific employee
    public DataRow GetLuongChiTiet(string maNV)
    {
        return luongDAL.GetLuongChiTiet(maNV);
    }
}

