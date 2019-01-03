using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DuyurulariGetir();
    }

    private void DuyurulariGetir()
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);

        string sorgu = "Select * from Duyurular order by Tarih";


        SqlCommand cmd = new SqlCommand(sorgu, con);
        con.Open();

        SqlDataReader dr = cmd.ExecuteReader();

        lstDuyuru.DataSource = dr;
        lstDuyuru.DataBind();

        con.Close();
    }

    protected void btnKayit_Click(object sender, EventArgs e)
    {
        if (txtKullaniciAdi.Text != "" && txtSifre.Text != "")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);

            string sorgu = "Insert into Kullanicilar (KullaniciAdi, Sifre) Values (@kullaniciadi, @sifre)";


            SqlCommand cmd = new SqlCommand(sorgu, con);
            con.Open();
            try
            {
                cmd.Parameters.AddWithValue("@kullaniciadi", txtKullaniciAdi.Text);
                cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);

                cmd.ExecuteNonQuery();

                con.Close();

                lblSonuc.Text = "Basariyla kayit yapilmistir";

            }
            catch (Exception)
            {

                lblSonuc.Text = "Kaydiniz yapilamamistir";
            }

        }

        else
        {
            lblSonuc.Text = "Bos alanlari doldurmaniz gerekir!!";

        }

        txtKullaniciAdi.Text = "";
        txtSifre.Text = "";        


    }
}
