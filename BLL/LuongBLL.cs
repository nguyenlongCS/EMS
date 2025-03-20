using System.Collections.Generic;
using System.Data;

public class LuongBLL
{
    private LuongDAL luongDAL = new LuongDAL();

    // Lấy danh sách lương
    public List<LuongDTO> GetDanhSachLuong()
    {
        return luongDAL.GetDanhSachLuong();
    }

    // Lấy chi tiết lương
    public DataRow GetLuongChiTiet(string maNV)
    {
        return luongDAL.GetLuongChiTiet(maNV);
    }
}
