using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MyCustomAction
{
    public partial class CreateDatabase : Form
    {
        public CreateDatabase()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            this.TopMost = true;
            this.Focus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bool valid = false;
            if (!String.IsNullOrEmpty(txtServerName.Text))
                valid = VerifySqlServer(txtServerName.Text);

            if (!valid)
            {
                MessageBox.Show("Can't connect to Sql server instance. Please check your input and try again.", "Invalid info");
            }
            else
            {
                InitializeDatabase(txtServerName.Text);
                this.DialogResult = DialogResult.Yes;
            }
        }

        private bool VerifySqlServer(string serverName)
        {
            bool connectionState;
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = serverName,
                IntegratedSecurity = true,
                ConnectTimeout = 5
            };
            SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            try
            {
                connection.Open();
                connectionState = true;
                connection.Close();
            }
            catch (SqlException)
            {
                connectionState = false;
            }
            return connectionState;
        }

        private void InitializeDatabase(string serverName)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = serverName,
                IntegratedSecurity = true,
                ConnectTimeout = 5
            };
            string connection = connectionStringBuilder.ConnectionString;
            DatabaseInitializer.Initialize(connection);
        }
    }
}
