using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace LagerKapazität
{
    public partial class Form2 : Form
    {
        private Form1 form1Instance;
        private DataTable dataTable;

        // SQL-Verbindungszeichenfolge
        private string connectionString = "Server=ServerName;Database=LagerKapazität;Trusted_Connection=True;";

        public Form2(Form1 form1)
        {
            InitializeComponent();
            form1Instance = form1; // Referenz auf Form1 setzen

            // DataTable erstellen und als Datenquelle für die DataGridView festlegen
            dataTable = new DataTable();
            dataTable.Columns.Add("Ware", typeof(string));
            dataTable.Columns.Add("Menge", typeof(int));

            dataGridView1.DataSource = dataTable; // DataGridView mit der DataTable verknüpfen
            LoadDataFromDatabase(); // Daten aus der Datenbank laden
        }

        // Passwort-Wunsch-Button
        private void button1WünchPasswort_Click(object sender, EventArgs e)
        {
            Form3 wunschPasswortForm = new Form3(form1Instance);
            wunschPasswortForm.Show();
            this.Hide(); // Verstecke das Anmeldeformular
        }

        // Programm beenden
        private void button2ProgrammBeenden_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Beim Laden des Formulars
        private void Form2_Load(object sender, EventArgs e)
        {
            // Initiale Konfigurationen können hier vorgenommen werden, falls nötig
            // Lade Daten aus der Datenbank in die DataGridView
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.Lager"; // Tabellennamen anpassen
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);

                // Daten zur DataTable hinzufügen, nur wenn sie noch nicht vorhanden sind
                foreach (DataRow row in dt.Rows)
                {
                    string ware = row["Ware"].ToString();
                    int menge = Convert.ToInt32(row["Menge"]);

                    // Überprüfen, ob die Ware bereits in der dataTable vorhanden ist
                    bool exists = dataTable.AsEnumerable().Any(r => r.Field<string>("Ware") == ware);

                    if (!exists)
                    {
                        dataTable.Rows.Add(ware, menge);
                    }
                }
            }
        }



        //private void LoadDataFromDatabase()
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = "SELECT * FROM Lager"; // Tabellennamen anpassen
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
        //        DataTable dt = new DataTable();
        //        dataAdapter.Fill(dt);

        //        // Daten zur DataTable hinzufügen
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            dataTable.Rows.Add(row["Ware"], row["Menge"]);
        //        }
        //    }
        //}

        // Speichern-Button
        private void button1Speichern_Click(object sender, EventArgs e)
        {
            ClearSuchenFields();

            string ware = textBox1WareSpeichen.Text;
            int menge;

            if (string.IsNullOrWhiteSpace(ware) || !int.TryParse(textBox2MengeSpeichen.Text, out menge))
            {
                MessageBox.Show("Bitte geben Sie eine gültige Ware und Menge ein.");
                return;
            }

            // Überprüfen auf Duplikate
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["Ware"].ToString() == ware)
                {
                    MessageBox.Show("Diese Ware ist bereits im Lager.");
                    return;
                }
            }

            // Neue Zeile zur DataTable hinzufügen
            dataTable.Rows.Add(ware, menge);
            SaveDataToDatabase(ware, menge); // Daten in die Datenbank speichern

            // Warnung bei geringer Menge
            if (menge < 5)
            {
                MessageBox.Show($"Die Ware {ware} hat nur {menge} Stück im Lager.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ClearSpeichernFields();
        }


        private void SaveDataToDatabase(string ware, int menge)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Überprüfen, ob die Ware bereits in der Datenbank existiert
                string checkQuery = "SELECT COUNT(*) FROM dbo.Lager WHERE Ware = @Ware";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Ware", ware);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        // Wenn die Ware bereits existiert, aktualisiere die Menge
                        string updateQuery = "UPDATE dbo.Lager SET Menge = Menge + @Menge WHERE Ware = @Ware";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@Menge", menge);
                            updateCommand.Parameters.AddWithValue("@Ware", ware);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Wenn die Ware nicht existiert, füge sie hinzu
                        string insertQuery = "INSERT INTO dbo.Lager (Ware, Menge) VALUES (@Ware, @Menge)";
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Ware", ware);
                            insertCommand.Parameters.AddWithValue("@Menge", menge);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }



        //private void SaveDataToDatabase(string ware, int menge)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = "INSERT INTO Lager (Ware, Menge) VALUES (@Ware, @Menge)";
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@Ware", ware);
        //            command.Parameters.AddWithValue("@Menge", menge);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        // Verkaufen-Button
        private void button2Verkaufen_Click(object sender, EventArgs e)
        {
            ClearSuchenFields();

            string ware = textBox3WareVarkaufen.Text;
            int mengeVerkaufen;

            if (string.IsNullOrWhiteSpace(ware) || !int.TryParse(textBox4MengeVerkaufen.Text, out mengeVerkaufen))
            {
                MessageBox.Show("Bitte geben Sie eine gültige Ware und Menge zum Verkaufen ein.");
                return;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                if (row["Ware"].ToString() == ware)
                {
                    int vorhandeneMenge = Convert.ToInt32(row["Menge"]);

                    if (vorhandeneMenge >= mengeVerkaufen)
                    {
                        row["Menge"] = vorhandeneMenge - mengeVerkaufen;
                        UpdateDataInDatabase(ware, vorhandeneMenge - mengeVerkaufen); // Datenbank aktualisieren
                        MessageBox.Show("Verkauf erfolgreich.");

                        if (vorhandeneMenge - mengeVerkaufen < 5)
                        {
                            MessageBox.Show($"Die Ware {ware} hat nur noch {vorhandeneMenge - mengeVerkaufen} Stück im Lager.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nicht genug Ware auf Lager.");
                    }

                    ClearVerkaufenFields();
                    return;
                }
            }

            MessageBox.Show("Ware nicht gefunden.");
        }


        // Suchen-Button
        private void button3Suchen_Click(object sender, EventArgs e)
        {
            string ware = textBox5WareSuchen.Text;

            if (string.IsNullOrWhiteSpace(ware))
            {
                MessageBox.Show("Bitte geben Sie den Namen der zu suchenden Ware ein.");
                return;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                if (row["Ware"].ToString() == ware)
                {
                    textBox6MengeSuchen.Text = row["Menge"].ToString();
                    textBox6MengeSuchen.ReadOnly = true; // Menge nicht änderbar machen
                    MessageBox.Show($"Ware: {row["Ware"]}, Menge: {row["Menge"]}");
                    return;
                }
            }

            MessageBox.Show("Ware nicht gefunden.");
        }

        // Ändern-Button
        private void button4Ändern_Click_1(object sender, EventArgs e)
        {
            ClearSuchenFields();

            string ware = textBox7WareÄndern.Text;
            int neueMenge;


            // Überprüfe, ob die neue Menge eine gültige Zahl ist
            if (string.IsNullOrWhiteSpace(ware) || !int.TryParse(textBox8MengeÄndern.Text, out neueMenge))
            {

                MessageBox.Show("Bitte geben Sie eine gültige Ware und neue Menge ein.");
                return;
            }

            // Suche nach der Ware in der DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["Ware"].ToString() == ware)
                {
                    // Aktuelle Menge in der DataTable abrufen
                    int vorhandeneMenge = Convert.ToInt32(row["Menge"]);

                    // Neue Menge zur vorhandenen Menge addieren
                    int aktualisierteMenge = vorhandeneMenge + neueMenge;

                    // Aktualisiere die DataTable mit der neuen Menge
                    row["Menge"] = aktualisierteMenge;

                    // Datenbank aktualisieren
                    UpdateDataInDatabase(ware, aktualisierteMenge);

                    MessageBox.Show($"Die Menge wurde erfolgreich aktualisiert. Neue Menge: {aktualisierteMenge}");

                    // Warnung bei geringer Menge
                    if (aktualisierteMenge < 5)
                    {
                        MessageBox.Show($"Die Ware {ware} hat nur noch {aktualisierteMenge} Stück im Lager.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    ClearÄndernFields();
                    return;
                }
            }

            // Wenn die Ware nicht gefunden wurde
            MessageBox.Show("Ware nicht gefunden.");
        }
        




        // Löschen-Button
        private void button5Löschen_Click(object sender, EventArgs e)
        {
            string ware = textBox9WareLöschen.Text;

            if (string.IsNullOrWhiteSpace(ware))
            {
                MessageBox.Show("Bitte geben Sie den Namen der zu löschenden Ware ein.");
                return;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                if (row["Ware"].ToString() == ware)
                {
                    dataTable.Rows.Remove(row);
                    DeleteDataFromDatabase(ware); // Daten aus der Datenbank löschen
                    MessageBox.Show("Ware erfolgreich gelöscht.");
                    ClearLoeschenFields();
                    return;
                }
            }

            MessageBox.Show("Ware nicht gefunden.");
        }


        private void UpdateDataInDatabase(string ware, int neueMenge)
        {
            string connectionString = "Server=ServerName;Database=LagerKapazität;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Überprüfe, ob die Ware existiert
                string checkQuery = "SELECT COUNT(*) FROM dbo.Lager WHERE Ware = @Ware";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Ware", ware);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        // Wenn die Ware existiert, aktualisiere die Menge
                        string updateQuery = "UPDATE dbo.Lager SET Menge = @Menge WHERE Ware = @Ware";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@Menge", neueMenge);
                            updateCommand.Parameters.AddWithValue("@Ware", ware);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Wenn die Ware nicht existiert, füge sie hinzu
                        string insertQuery = "INSERT INTO dbo.Lager (Ware, Menge) VALUES (@Ware, @Menge)";
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Ware", ware);
                            insertCommand.Parameters.AddWithValue("@Menge", neueMenge);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }





        //private void UpdateDataInDatabase(string ware, int neueMenge)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = "UPDATE Lager SET Menge = @Menge WHERE Ware = @Ware";
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@Menge", neueMenge);
        //            command.Parameters.AddWithValue("@Ware", ware);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        private void DeleteDataFromDatabase(string ware)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM dbo.Lager WHERE Ware = @Ware";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Ware", ware);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Unbenutzte TextBox-Methoden
        private void textBox1WareSpeichern_TextChanged(object sender, EventArgs e) { }
        private void textBox2MengeSpeichern_TextChanged(object sender, EventArgs e) { }
        private void textBox3WareVerkaufen_TextChanged(object sender, EventArgs e) { }
        private void textBox4MengeVerkaufen_TextChanged(object sender, EventArgs e) { }
        private void textBox5WareSuchen_TextChanged(object sender, EventArgs e) { }
        private void textBox6MengeSuchen_TextChanged(object sender, EventArgs e) { }
        private void textBox7WareÄndern_TextChanged(object sender, EventArgs e) { }
        private void textBox8MengeÄndern_TextChanged(object sender, EventArgs e) { }
        private void textBox9WareLoeschen_TextChanged(object sender, EventArgs e) { }

        // DataGridView CellContentClick Event (derzeit unbenutzt)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // Tab-Index für die Reihenfolge der Steuerungselemente festlegen
        private void Form2_Activated(object sender, EventArgs e)
        {
            // Tab-Index für eine klare Reihenfolge festlegen
            textBox1WareSpeichen.TabIndex = 0; // Speichern Ware
            textBox2MengeSpeichen.TabIndex = 1; // Speichern Menge
            button1Speichen.TabIndex = 2; // Speichern Button
            textBox3WareVarkaufen.TabIndex = 3; // Verkaufen Ware
            textBox4MengeVerkaufen.TabIndex = 4; // Verkaufen Menge
            button2Verkaufen.TabIndex = 5; // Verkaufen Button
            textBox5WareSuchen.TabIndex = 6; // Suchen Ware
            textBox6MengeSuchen.TabIndex = 7; // Suchen Menge
            button3Suchen.TabIndex = 8; // Suchen Button
            
            button4Ändern.TabIndex = 11; // Ändern Button
            
        }

        private void ClearSpeichernFields()
        {
            textBox1WareSpeichen.Clear();
            textBox2MengeSpeichen.Clear();
        }

        private void ClearVerkaufenFields()
        {
            textBox3WareVarkaufen.Clear();
            textBox4MengeVerkaufen.Clear();
        }

        private void ClearSuchenFields()
        {
            textBox5WareSuchen.Clear();
            textBox6MengeSuchen.Clear();
            
        }

        private void ClearÄndernFields()
        {
            textBox8MengeÄndern.Clear();
            textBox7WareÄndern.Clear();
        }

        private void ClearLoeschenFields()
        {
            textBox9WareLöschen.Clear();
        }

        private void textBox9WareLöschen_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7WareÄndern_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox8MengeÄndern_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
