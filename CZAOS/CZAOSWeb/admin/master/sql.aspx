<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.Sql" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data.Common" %>
<%@ Register Src="~/admin/master/masterlinks.ascx" TagPrefix="uc1" TagName="masterlinks" %>


<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="C#" runat="server">                           
         
        public DataSet ExecuteDataSet(string query, CommandType commandtype)
        {
            
            DbProviderFactory objFactory = SqlClientFactory.Instance;
            DbConnection objConnection = objFactory.CreateConnection();
            DbDataAdapter adapter = objFactory.CreateDataAdapter();
            DbCommand objCommand = objFactory.CreateCommand();

            objConnection.ConnectionString = "Password=lj81h9z;Persist Security Info=True;User ID=ronfoth;Initial Catalog=ronfoth;Data Source=(local);";
            objCommand.Connection = objConnection;
            
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            adapter.SelectCommand = objCommand;
            
            DataSet ds = new DataSet();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                 
            }
            finally
            {
                objCommand.Parameters.Clear();
                if (objConnection.State == System.Data.ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return ds;
        }

        public int ExecuteNonQuery(string query, CommandType commandtype)
        {
            DbProviderFactory objFactory = SqlClientFactory.Instance;
            DbConnection objConnection = objFactory.CreateConnection();
            DbDataAdapter adapter = objFactory.CreateDataAdapter();
            DbCommand objCommand = objFactory.CreateCommand();

            objConnection.ConnectionString = "Password=lj81h9z;Persist Security Info=True;User ID=ronfoth;Initial Catalog=ronfoth;Data Source=(local);";
            objCommand.Connection = objConnection;
            
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            
            int i = -1;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = objCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                 
            }
            finally
            {
                objCommand.Parameters.Clear();
                objConnection.Close();
            }

            return i;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Roles.IsUserInRole("MasterAdmin"))
                {
                    throw new HttpException(403, "Access is denied");
                }
                //string sql = "SELECT * FROM foth_PortfolioAsset WHERE Filename LIKE '%?%'"; // information_schema.tables";
                //string update = "UPDATE foth_PortfolioAsset SET Filename = 'Wendys_Chevy Artwork 9_27_2010.zip' WHERE ID = 2108"; // information_schema.tables";

                //ExecuteNonQuery(update, CommandType.Text);
                
                //gvResultSet.DataSource = ExecuteDataSet(sql, CommandType.Text);
                //gvResultSet.DataBind();
            }
        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            gvResultSet.DataSource = ExecuteDataSet(txtSQL.Text, CommandType.Text);
            gvResultSet.DataBind();

            lblResults.Text = "Showing " + gvResultSet.Rows.Count + " rows";
            
        }

        protected void btnNonQuery_Click(object sender, EventArgs e)
        {
            int i = ExecuteNonQuery(txtSQL.Text, CommandType.Text);
            lblResults.Text = "Updated " + i + " rows";
        }
        
</script>
    
</head>
<body>
    <form id="form1" runat="server">
        <uc1:masterlinks runat="server" id="masterlinks" />
        <asp:TextBox runat="server" ID="txtSQL" TextMode="MultiLine" Height="200px" Width="100%"></asp:TextBox>
        <br />
        <asp:Button runat="server" ID="btnQuery" Text="Go" Width="130px" CausesValidation="false" OnClick="btnQuery_Click" />
        <asp:Button runat="server" ID="btnNonQuery" Text="Non Q Go" Width="130px" CausesValidation="false" OnClick="btnNonQuery_Click" />
        <br />
        <asp:GridView runat="server" ID="gvResultSet" Width="720px" AutoGenerateColumns="true">
            <EmptyDataTemplate>
                <div class="static_message">No Results</div>
            </EmptyDataTemplate>
        </asp:GridView>
        <br />
        <asp:Label runat="server" ID="lblResults"></asp:Label>

    </form>

</body>
</html>
