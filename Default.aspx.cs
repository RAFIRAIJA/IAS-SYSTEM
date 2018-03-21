using System;
using System.Web.UI;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

public partial class _Default : Page
{
    public class UserInfo
    {
        public string customfield1 { get; set; }
        public string Fullname { get; set; }
        public string position_name_en { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
        public string jobjobtask_code { get; set; }
        public string Department_Name { get; set; }
        public string costcenter_name_en { get; set; }
        public string branch { get; set; }
        public string level_code { get; set; }
        public string blood_type { get; set; }
        public string tgl_lahir { get; set; }
        public string tgl_masuk { get; set; }
    }
    public class DataCabang
    {
        [JsonProperty("BranchID")]
        public string BranchID { get; set; }

        [JsonProperty("BranchName")]
        public string BranchName { get; set; }

  
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        err_msg.Visible = false;
    }

    protected void btn_login_Click(object sender, EventArgs e)
    {
        string user, pass, ret;
        string str_conn, sql;
        SqlConnection conn;

        str_conn = WebConfigurationManager.ConnectionStrings["conn_iss"].ConnectionString;
        conn = new SqlConnection(str_conn);

        user = username.Value;
        pass = password.Value;

        AuthUser auth = new AuthUser();

        try
        {
            ret = auth.IsValid(user, pass);

            if (ret == "error_data")
            {
                err_msg.InnerHtml = "Invalid NIK or Password";
                err_msg.Visible = true;
            }
            else
            {
                UserInfo userinfo = JsonConvert.DeserializeObject<UserInfo>(ret);

                //string cab = auth.GetDataCustomer("ISS");

                //int c = jObject.Count();
                //string j = jObject[0].BranchID;

                sql = "select top 1 iss_user_id,isnull(iss_user_cabang_id,'') as iss_user_cabang_id,isnull(iss_user_group_id,0) as iss_user_group_id,iss_user_email,iss_user_foto  ";
                sql += " from dbo.iss_user where iss_user_status=1 and iss_user_nik='" + user + "'";

                conn.Open();
                SqlCommand rs = new SqlCommand(sql, conn);
                SqlDataReader row = rs.ExecuteReader(CommandBehavior.CloseConnection);

                while (row.Read())
                {
                    Session["LoginUserID"] = row.GetInt32(0);
                    Session["LoginNIK"] = user;
                    Session["LoginCabangID"] = userinfo.branch;// row.GetString(1);
                    Session["LoginFullName"] = userinfo.Fullname;// row.GetString(1);
                    Session["LoginGroupID"] = row.GetInt32(2);
                    Session["LoginEmail"] = row.GetString(3);
                    Session["LoginImg"] = row["iss_user_foto"].ToString();

                    Response.Redirect("iss_user_level.aspx");

                }
                row.Close();
            }  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
             
    }
    
}