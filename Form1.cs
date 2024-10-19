using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace LagerKapazität
{
    public partial class Form1 : Form
    {
        // Speichere den Benutzernamen und das Passwort
        private string richtigerUsername = "13";
        private string richtigesPasswort = "13"; // Aktuelles Passwort

        // Public Property, um das Passwort von außen lesen und ändern zu können
        public string RichtigesPasswort
        {
            get { return richtigesPasswort; }
            set { richtigesPasswort = value; }
        }

        private string emailAdresse = "EmailAdresse"; // Deine E-Mail-Adresse

        public Form1()
        {
            InitializeComponent();

            // Passwort zu Beginn unsichtbar machen
            textBox2Passwort.PasswordChar = '*';

            // Platzhaltertexte zu Beginn setzen
            SetPlaceholderText();

            // TabIndex einstellen, um zwischen Textboxen zu wechseln
            textBox1Username.TabIndex = 0;
            textBox2Passwort.TabIndex = 1;
            button1Anmelden.TabIndex = 2;

            // KeyPress Event für Textboxen hinzufügen
            this.textBox1Username.KeyDown += new KeyEventHandler(OnKeyDownHandler);
            this.textBox2Passwort.KeyDown += new KeyEventHandler(OnKeyDownHandler);
        }

        private void SetPlaceholderText()
        {
            if (string.IsNullOrWhiteSpace(textBox1Username.Text))
            {
                textBox1Username.Text = "Benutzername eingeben...";
                textBox1Username.ForeColor = System.Drawing.Color.Gray; // Platzhalterfarbe
            }
            if (string.IsNullOrWhiteSpace(textBox2Passwort.Text))
            {
                textBox2Passwort.Text = "Passwort eingeben...";
                textBox2Passwort.ForeColor = System.Drawing.Color.Gray; // Platzhalterfarbe
                textBox2Passwort.UseSystemPasswordChar = false; // Sichtbar für Platzhalter
            }
        }

        private void textBox1Username_Enter(object sender, EventArgs e)
        {
            if (textBox1Username.Text == "Benutzername eingeben...")
            {
                textBox1Username.Text = "";
                textBox1Username.ForeColor = System.Drawing.Color.Black; // Textfarbe zurücksetzen
            }
        }

        private void textBox1Username_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1Username.Text))
            {
                SetPlaceholderText();
            }
        }

        private void textBox2Passwort_Enter(object sender, EventArgs e)
        {
            if (textBox2Passwort.Text == "Passwort eingeben...")
            {
                textBox2Passwort.Text = "";
                textBox2Passwort.ForeColor = System.Drawing.Color.Black; // Textfarbe zurücksetzen
                textBox2Passwort.UseSystemPasswordChar = true; // Passwort unsichtbar machen
            }
        }

        private void textBox2Passwort_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2Passwort.Text))
            {
                SetPlaceholderText();
            }
        }

        // Passwort anzeigen/verbergen
        private void checkBox1PasswortAnzeigen_CheckedChanged(object sender, EventArgs e)
        {
            textBox2Passwort.PasswordChar = checkBox1PasswortAnzeigen.Checked ? '\0' : '*'; // Passwort anzeigen/verbergen
        }

        private void OpenForm2()
        {
            Form2 form2 = new Form2(this); // Übergib die aktuelle Instanz von Form1
            form2.Show();
        }

        // Anmelden-Button Klick
        private void button1Anmelden_Click(object sender, EventArgs e)
        {
            string eingegebenerUsername = textBox1Username.Text.Trim();
            string eingegebenesPasswort = textBox2Passwort.Text.Trim();

            if (checkBox2PasswortVergessen.Checked)
            {
                // Passwort vergessen wurde angeklickt
                SendeNeuesPasswort();
                return;
            }

            // Anmeldeüberprüfung
            if (eingegebenerUsername.Equals(richtigerUsername, StringComparison.OrdinalIgnoreCase) &&
                eingegebenesPasswort == richtigesPasswort)
            {
                MessageBox.Show("Anmeldung erfolgreich!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Neues Fenster öffnen (z.B., Hauptanwendung)
                OpenForm2();
                this.Hide(); // Verstecke das Anmeldeformular
            }
            else
            {
                MessageBox.Show("Ungültiger Benutzername oder Passwort.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Methode zum Senden eines neuen Passworts
        private void SendeNeuesPasswort()
        {
            // Neues Passwort generieren (hier einfach als Beispiel)
            string neuesPasswort = GenerateRandomPassword(); // Methode zum Generieren eines zufälligen Passworts

            try
            {
                // E-Mail Nachricht erstellen
                MailMessage mail = new MailMessage(emailAdresse, emailAdresse); // Absender und Empfänger sind gleich
                SmtpClient client = new SmtpClient("smtp.gmail.com");

                // SMTP-Client Einstellungen
                client.Port = 587; // TLS Port
                client.Credentials = new NetworkCredential("EmialAdresse", "2.Faktore Passwort"); // App-Passwort
                client.EnableSsl = true; // SSL/TLS aktivieren

                // Betreff und Nachrichtentext
                mail.Subject = "Neues Passwort";
                mail.Body = $"Ihr neues Passwort lautet: {neuesPasswort}";

                // E-Mail senden
                client.Send(mail);
                MessageBox.Show("Eine E-Mail mit einem neuen Passwort wurde an " + emailAdresse + " gesendet.");

                // Aktualisiere das Passwort in der Anwendung
                richtigesPasswort = neuesPasswort; // Altes Passwort mit neuem Passwort ersetzen
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                MessageBox.Show("Fehler beim Senden der E-Mail: " + ex.Message);
            }
        }

        // Methode zur Generierung eines zufälligen Passworts
        private string GenerateRandomPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 13); // Einfaches Beispiel, ein zufälliger GUID-Teil
        }

        // Methode zum Handhaben von Tastatureingaben (Tab und Enter)
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            // Bei Enter: Anmeldung ausführen
            if (e.KeyCode == Keys.Enter)
            {
                button1Anmelden.PerformClick(); // Anmelde-Button Klick auslösen
            }
            // Bei Tab: Zum nächsten Steuerelement wechseln
            else if (e.KeyCode == Keys.Tab)
            {
                this.SelectNextControl(ActiveControl, true, true, true, true); // Zum nächsten Steuerelement wechseln
            }
        }

        
    }
}
