using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace DataBaseManagerApplication
{
    public partial class MainForm : Form
    {
        private List<Label> labelsList;
        private List<TextBox> textBoxesList;
        private List<string> tablesNames;
        private List<string> columnsTitle;
        private List<string> logData = new List<string>();
        private int columnCnt;
        private string userSelectedTableName;


        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tablesNames = getDataBaseTables();
            foreach (var tableName in tablesNames)
            {
                dataBaseTablesListBox.Items.Add(tableName);
            }

        }
        // Getting all table names from BuildingMaterials_store database
        private List<string> getDataBaseTables()
        {
            List<string> tablesNames = new List<string>();

            try
            {
                // create connection
                MySqlConnection connection = new MySqlConnection(getConnectionString());
                connection.Open();

                // get tables from database
                string query = "show tables from BuildingMaterials_store;";
                MySqlCommand command = new MySqlCommand(query, connection);
                logData.Add($"*[{DateTime.Now}] New connection;\n\n");

                // read tables
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(0) == "Store_Schedule_open" || reader.GetString(0) == "__efmigrationshistory" || reader.GetString(0) == "__EFMigrationsHistory")
                            continue;
                        tablesNames.Add(reader.GetString(0));
                    }
                }
                logData.Add($"**[{DateTime.Now}] sql query '{query}' - executed (DONE);\n\n");
                connection.Close();
                logData.Add($"*[{DateTime.Now}] Connection closed;\n\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while adding table names from DataBase.\n{ex.Message}");
                logData.Add($"ERROR[{DateTime.Now}] error occurred while adding table names.\n'{ex.Message}';\n\n");
            }

            return tablesNames;
        }

        // Getting "connection string" to Database from appsettings.json
        private string getConnectionString()
        {
            string connectionString;
            try
            {
                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile($"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/appsettings.json");
                IConfigurationRoot config = builder.Build();
                connectionString = config.GetConnectionString("DefaultConnection");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while getting connection string from appsettings.json.\n{ex.Message}");
                return "";
                logData.Add($"ERROR[{DateTime.Now}] error occurred while getting connection string from appsettings.json.\n'{ex.Message}';\n\n");
            }
            logData.Add($"*[{DateTime.Now}] Getting connection string from 'appsettings.json' (DONE);\n\n");
            return connectionString;
        }

        // Adding new data by "INSERT" to database table, which user selected before in "dataBaseTablesListBox" tool
        private void create_button_Click(object sender, EventArgs e)
        {
            if (dataBaseTablesListBox.SelectedItems.Count == 1)
            {
                // Building insert sql statement
                
                if (dataBaseTablesListBox.SelectedItem.ToString() != "authorization" && dataBaseTablesListBox.SelectedItem.ToString() != "store_schedule_open")
                {
                    columnsTitle.RemoveAt(0);
                }

                string query = $"INSERT INTO {userSelectedTableName} (";
                for (int i = 0; i < columnsTitle.Count; i++)
                {
                    query += $" {columnsTitle[i]}";
                    if (i < columnsTitle.Count - 1)
                        query += ",";
                    else
                        query += ") VALUES (";
                }
                for (int i = 0; i < columnsTitle.Count; i++)
                {
                    query += textBoxesList[i].Text.Trim() == "" ? " NULL" : $" '{textBoxesList[i].Text.Split(" ")[0]}'";
                    if (i < columnsTitle.Count - 1)
                        query += ",";
                    else
                        query += ")";
                }

                MessageBox.Show(query);
                runSqlQuery(query);
                updateView();
            }
        }

        // Updating new data by "UPDATE" to database table
        private void update_button_Click(object sender, EventArgs e)
        {
            if (dataBaseTablesListBox.SelectedItems.Count == 1 && tableItemsDataGrid.Rows.Count > 0)
            {
                var selectedRow = tableItemsDataGrid.SelectedCells[0].RowIndex;
                var idValue = tableItemsDataGrid.Rows[selectedRow].Cells[0].Value;

                var id = columnsTitle[0];

                if (dataBaseTablesListBox.SelectedItem.ToString() != "authorization" && dataBaseTablesListBox.SelectedItem.ToString() != "store_schedule_open")
                {
                    columnsTitle.RemoveAt(0);
                }

                // Building update sql statement
                string query = $"UPDATE {userSelectedTableName} SET";
                for (int i = 0; i < columnsTitle.Count; i++)
                {
                    query += textBoxesList[i].Text.Trim() == "" ? $" {columnsTitle[i]}=NULL" : $" {columnsTitle[i]}='{textBoxesList[i].Text.Split(" ")[0]}'";
                    if (i < columnsTitle.Count - 1)
                        query += ",";
                    else
                        query += $" WHERE {id}={idValue}";
                }

                MessageBox.Show(query);
                runSqlQuery(query);
                updateView();
            }

        }

        // Deletion by "DELETE" data that user selected by clicking on "tableItemsDataGrid" tool cell
        private void delete_button_Click(object sender, EventArgs e)
        {
            if (dataBaseTablesListBox.SelectedItems.Count == 1 && tableItemsDataGrid.Rows.Count > 0)
            {
                var selectedRow = tableItemsDataGrid.SelectedCells[0].RowIndex;
                var idValue = tableItemsDataGrid.Rows[selectedRow].Cells[0].Value;

                // Building update sql statement
                string query = $"DELETE FROM {userSelectedTableName} WHERE {columnsTitle[0]}='{idValue}'";

                if (runSqlQuery(query))
                {
                    updateView();
                }
            }
        }

        // Running custom sql query
        private bool runSqlQuery(string query)
        {
            bool isStatus = false;
            MySqlConnection connection = new MySqlConnection(getConnectionString());

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteReader();
                isStatus = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SQL statement run error.\n\n{ex.Message}", "DataBase request error");
                logData.Add($"**[{DateTime.Now}] sql query '{query}' - SQL statement run error (BAD)\n{ex.Message};\n\n");
            }
            finally
            {
                connection.Close();
            }

            if (isStatus)
                logData.Add($"**[{DateTime.Now}] sql query '{query}' - executed (DONE);\n\n");

            return isStatus;
        }

        // Clear values in input user TextBoxes tools
        private void clear_button_Click(object sender, EventArgs e)
        {
            if (dataBaseTablesListBox.SelectedItems.Count == 1)
            {
                for (int i = 0; i < columnsTitle.Count; i++)
                {
                    textBoxesList[i].Text = "";
                }
            }
        }

        // Selecting new table from "dataBaseTablesListBox" tool, for adding/updating/deleting data
        private void dataBaseTablesListBox_Click(object sender, EventArgs e)
        {
            updateView();
            logData.Add($"[{DateTime.Now}] Table changed;\n\n");
        }

        // Getting new data from the database and updating values in "tableItemsDataGrid" tool
        private void updateView()
        {
            // Clear dataGridView items
            tableItemsDataGrid.Rows.Clear();
            tableItemsDataGrid.Columns.Clear();

            // Get clicked table from ListBox
            userSelectedTableName = dataBaseTablesListBox.SelectedItem.ToString();

            // Create new connection to DB
            MySqlConnection connection = new MySqlConnection(getConnectionString());
            connection.Open();
            logData.Add($"*[{DateTime.Now}] New connection;\n\n");

            // Get count of title column names from the table 
            columnCnt = getColumnsTitleCount(userSelectedTableName, connection);

            // Set dataGridView titles
            int _cnt = 0;
            columnsTitle = getColumnsTitle(userSelectedTableName, connection);
            foreach (var title in columnsTitle)
            {
                tableItemsDataGrid.Columns[_cnt].Name = title;
                _cnt++;
            }

            // Add rows data to dataGridView
            foreach (var dataRow in getRowsData(userSelectedTableName, connection))
            {
                tableItemsDataGrid.Rows.Add(dataRow);
            }

            // Clear TextBoxes and labels
            labelsList = new List<Label> { label1, label2, label3, label4, label5, label6, label7, label8, label9 };
            textBoxesList = new List<TextBox> { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9 };
            for (int k = 0; k < labelsList.Count; k++)
            {
                labelsList[k].Visible = false;
                textBoxesList[k].Visible = false;
                textBoxesList[k].Text = null;

            }

            // Set TextBoxes and labels
            if (dataBaseTablesListBox.SelectedItem.ToString() != "authorization" && dataBaseTablesListBox.SelectedItem.ToString() != "store_schedule_open")
            {
                for (int i = 0; i < columnsTitle.Count-1; i++)
                {
                    labelsList[i].Visible = true;
                    textBoxesList[i].Visible = true;
                    labelsList[i].Text = columnsTitle[i + 1];
                    labelTableName.Text = userSelectedTableName;
                }
            }
            else
            {
                for (int i = 0; i < columnsTitle.Count; i++)
                {
                    labelsList[i].Visible = true;
                    textBoxesList[i].Visible = true;
                    labelsList[i].Text = columnsTitle[i];
                    labelTableName.Text = userSelectedTableName;
                }
            }
            
            connection.Close();
            logData.Add($"*[{DateTime.Now}] Connection closed;\n\n");

        }

        // Getting count of the titles in table from the DataBase
        private int getColumnsTitleCount(string tableName, MySqlConnection connection)
        {
            int columnCnt = 0;
            string query = $"SELECT COUNT(*) AS anyName FROM INFORMATION_SCHEMA.COLUMNS WHERE table_schema = 'buildingmaterials_store' AND table_name = '{tableName}';";
            try
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columnCnt = Convert.ToInt32(reader.GetValue(0));
                    }
                }
                logData.Add($"**[{DateTime.Now}] sql query '{query}' - executed (DONE);\n\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"'getColumnsTitleCount' Error occurred while reading data from ExecuteReader '{query}'.\n{ex.Message}");
                logData.Add($"ERROR[{DateTime.Now}] 'getColumnsTitleCount' Error occurred while reading data from ExecuteReader '{query}'.\n'{ex.Message}';\n\n");
            }

            return columnCnt;
        }

        // Getting table title names from the DataBase (id, store, person ...)
        private List<string> getColumnsTitle(string tableName, MySqlConnection connection)
        {
            List<string> columnsTitle = new List<string>();
            string query = $"SHOW COLUMNS FROM {tableName} ;";
            try
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    tableItemsDataGrid.ColumnCount = columnCnt;

                    while (reader.Read())
                    {
                        columnsTitle.Add(reader.GetString(0));
                    }
                }
                logData.Add($"**[{DateTime.Now}] sql query '{query}' - executed (DONE);\n\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"'getColumnsTitle' Error occurred while reading data from ExecuteReader '{query}'.\n{ex.Message}");
                logData.Add($"ERROR[{DateTime.Now}] 'getColumnsTitle' Error occurred while reading data from ExecuteReader '{query}'.\n'{ex.Message}';\n\n");
            }

            return columnsTitle;
        }

        // Read new data from tha database 
        private List<string[]> getRowsData(string tableName, MySqlConnection connection)
        {
            List<string[]> rowsData = new List<string[]>();
            string query = $"SELECT * FROM {tableName};";
            try
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] items = new string[columnsTitle.Count];
                        for (int i = 0; i < columnsTitle.Count; i++)
                        {
                            items[i] = reader.GetValue(i).ToString();
                        }
                        rowsData.Add(items);
                    }
                }
                logData.Add($"**[{DateTime.Now}] sql query '{query}' - executed (DONE);\n\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"'getRowsData' Error occurred while reading data from ExecuteReader '{query}'.\n{ex.Message}");
                logData.Add($"ERROR[{DateTime.Now}] 'getRowsData' Error occurred while reading data from ExecuteReader '{query}'.\n'{ex.Message}';\n\n");
            }

            return rowsData;
        }

        // Setting new values to "TextBoxes" tools, when clicking on "tableItemsDataGrid" tool cell with data
        private void tableItemsDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataBaseTablesListBox.SelectedItems.Count == 1 && tableItemsDataGrid.Rows.Count > 0)
            {
                var selectedRow = tableItemsDataGrid.SelectedCells[0].RowIndex;

                for (int i = 0; i < columnsTitle.Count - 1; i++)
                {
                    textBoxesList[i].Text = tableItemsDataGrid.Rows[selectedRow].Cells[i + 1].Value.ToString();
                }
            }
        }

        // Exit by clicking on "Exit" menu strip tool button
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        // Open new window with logs about current working session
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            foreach (var item in logData)
            {
                f2.richTextBoxLogs.Text += item;
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If have any questions, please contact me:\n\nMail: ncxr.official@gmail.com", "Info");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by ncx (Vitalii Nasikovskyi)\n\nVasyl\' Stus Donetsk National University", "Info");
        }

        // Saving current working session logs to "Logs/logs.txt"
        private void saveLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/Logs/logs.txt";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var column in logData)
                {
                    writer.WriteLine(column);
                }
            }
        }
    }
}