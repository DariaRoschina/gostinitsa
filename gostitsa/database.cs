using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

/*
                    string dTime = DateTime.Now.ToString();
                    t_querry = "Insert into Table1(Column1,Column2,Column3) values (" + i + "," + rnd.Next(1, 4) + ",'" + dTime + "')";
                    t_command = new OracleCommand(t_querry);
                    t_command.Connection = t_connection;

 */
namespace gostitsa
{

       public class database
    {
        public enum DBType { VARCHAR2 = 0, DATE = 1, NUMBER = 2 };

        public struct TabColumn
        {
            public String Name;
            public DBType Type;
        }

        public struct TabValue
        {
            public String Name;
            public Object Value;
        }
        private readonly string[] DBTypes = { "VARCHAR2(255)", "DATE", "NUMBER(9,0)" };

       

       

        private String conStr =
            "Data Source=127.0.0.1:1521;User ID=GOSTINITSA;Password=123;";
        private OracleConnection con = null;

        public database()
        {
            con = new OracleConnection(conStr);
            con.Open();
        }

        ~database()
        {
            con.Close();
        }
        //hostel
        public List<String> getListGostinits(bool addGost)
        {
            List<String> result = new List<string>();
            String querry = "Select NAZVANIE from GOSTINITSA";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Add(data.GetValue(0).ToString());
            }
            if (addGost)
            {
                result.Add("Добавить гостиницу");
            }
            data.Close();
            return result;
        }
        public DataTable getTableGost()
        {
            DataTable result = new DataTable();

            //result.Columns.Add("Код гостиницы");
            result.Columns.Add("Адрес");
            result.Columns.Add("Телефон");
            result.Columns.Add("Название");
            String querry = "Select GOSTINITSA.ID_GOST, GOSTINITSA.ADRESS, " +
                "GOSTINITSA.TELEPHONE, GOSTINITSA.NAZVANIE" +
                " from GOSTINITSA";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Rows.Add(
                    (data.IsDBNull(1)) ? "" : data.GetValue(1).ToString(),
                    (data.IsDBNull(2)) ? "" : data.GetValue(2).ToString(),
                    (data.IsDBNull(3)) ? "" : data.GetValue(3).ToString());
                   // (data.IsDBNull(4)) ? "" : data.GetValue(4).ToString());
            }
            data.Close();
            return result;
        }
        public void insGost(string address, long tel, string name)
        {
            string querry = "Insert into GOSTINITSA (ID_GOST,ADRESS,"+
                "TELEPHONE,NAZVANIE) values (null, '" + address + "', '" +
                tel.ToString() + "','" + name + "')";
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public void delGost(int ID_GOST)
        {
            string querry = "Delete from GOSTINITSA where ID_GOST =" +
                ID_GOST.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public void updateGost(int id_gost, string adress, long telephone,
           string nazvanie)
        {
            string querry = "Update GOSTINITSA set "
                + "ADRESS = " + adress
                + "TELEPHONE = " + telephone
                + "NAZVANIE = " + nazvanie
                + "where ID_KLIENT =" + id_gost.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public int getIdGost(string nazvanie)
        {
            int result = 0;
            string querry = "Select ID_GOST from GOSTINITSA where " +
                "NAZVANIE ='" + nazvanie;
            OracleCommand comm = new OracleCommand(querry, con);
            result = (int)comm.ExecuteScalar();
            return result;
        }

        //klients
        public List<String> getListKlient()
        {
            List<String> result = new List<string>();
            String querry = "Select FIO from KLIENTS";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Add(data.GetValue(0).ToString());
            }
            data.Close();
            return result;
        }
        public DataTable getTableKlient()
        {
            DataTable result = new DataTable();
            result.Columns.Add("ФИО");
            result.Columns.Add("Паспорт");
            result.Columns.Add("Комментарий");
            result.Columns.Add("Вид клиента");
            String querry = "Select KLIENTS.ID_KLIENT, KLIENTS.FIO, " +
                "KLIENTS.PASPORT, KLIENTS.KOMMENT, VIDI_KLIENTOV.NAZVANIE" +
                " from KLIENTS join VIDI_KLIENTOV on  "+
                "VIDI_KLIENTOV.id_vida = KLIENTS.id_vida";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Rows.Add(
                    (data.IsDBNull(1)) ? "" : data.GetValue(1).ToString(),
                    (data.IsDBNull(2)) ? "" : data.GetValue(2).ToString(),
                    (data.IsDBNull(3)) ? "" : data.GetValue(3).ToString(),
                    (data.IsDBNull(4)) ? "" : data.GetValue(4).ToString());
            }
            data.Close();
            return result;
        }
        public void insKlient(string fio, long pasport, string comment, 
            int id_vida)
        {
            string querry = "Insert into KLIENTS (ID_KLIENT , FIO, PASPORT, "+
                "KOMMENT, ID_VIDA) values (null, '" + fio + "', " +
                pasport.ToString() + ",'" + comment +  "', " +
                id_vida.ToString() + ")";
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public int getIdKlient(string fio, long pasport)
        {
            int result = 0;
            string querry = "Select ID_KLIENT from KLIENTS where " +
                "FIO ='" + fio + "' and PASPORT = '" + pasport.ToString()+"'";
            OracleCommand comm = new OracleCommand(querry, con);
            result = (int)comm.ExecuteScalar();
            return result;
        }
        public void delKlient(int id_kient)
        {
            string querry = "Delete from KLIENTS where ID_KLIENT =" +
                id_kient.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public void updateKlient(int id_klient,string fio, long pasport, 
            string comment, int id_vida)
        {
            string querry = "Update KLIENTS set "
                + "FIO = " + fio
                + "PASPORT = " + pasport
                + "KOMMENT = " + comment
                + "ID_VIDA = " + id_vida
                + "where ID_KLIENT =" + id_klient.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }

        //vid klienta
        public List<String> getListVid(bool addVid)
        {
            List<String> result = new List<string>();
            String querry = "Select NAZVANIE from VIDI_KLIENTOV";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Add(data.GetValue(0).ToString());
            }
            if (addVid)
            {
                result.Add("Добавить вид");
            }
            data.Close();
            return result;
        }
        public int getIdVida(string tip)
        {
            int result = 0;
            string querry = "Select ID_VIDA from VIDI_KLIENTOV where " +
                "NAZVANIE ='" + tip + "'";
            OracleCommand comm = new OracleCommand(querry, con);
            result = (int)comm.ExecuteScalar();
            return result;
        }
        public int getIdVida(int id_klient)
        {
            int result = 0;
            string querry = "Select ID_VIDA from KLIENTS where " +
                "id_klient ='" + id_klient.ToString() + "'";
            OracleCommand comm = new OracleCommand(querry, con);
            result = (int)comm.ExecuteScalar();
            return result;
        }
        public void insVid(string name)
        {
            string querry = "Insert into VIDI_KLIENTOV (ID_VIDA , NAZVANIE)" +
                "values (null, '" + name + "')";
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public void delVidK(int id_vida)
        {
            string querry = "Delete from VIDI_KLIENTOV where ID_VIDA =" +
                id_vida.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public DataTable getTableVidK()
        {
            DataTable result = new DataTable();
            result.Columns.Add("Название");
            String querry = "Select VIDI_KLIENTOV.ID_VIDA, VIDI_KLIENTOV.NAZVANIE  " +
              " from VIDI_KLIENTOV";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Rows.Add(
                   (data.IsDBNull(1)) ? "" : data.GetValue(1).ToString());
            }
            data.Close();
            return result;
        }
        //public void createVidTable(int id_vida, params TabColumn[] columns)
        //{
        //    String querry = "CREATE TABLE  \"VID_" + id_vida.ToString() + "\" (" +
        //       " \"ID_KLIENT\" NUMBER(3,0) NOT NULL ENABLE ";
        //    foreach (TabColumn col in columns)
        //    {
        //        querry = querry + ", \"" + col.Name.ToUpper() + "\" " + DBTypes[(int)col.Type];
        //    }
        //    querry = querry + ")";
        //    OracleCommand comm = new OracleCommand(querry, con);
        //    comm.ExecuteNonQuery();
        //    querry = "ALTER TABLE  \"VID_" + id_vida.ToString() + "\" " +
        //         "ADD CONSTRAINT \"VID_" + id_vida.ToString() +
        //         "_FK\" FOREIGN KEY (\"ID_KLIENT\")  REFERENCES  " +
        //         "\"KLIENTS\" (\"ID_KLIENT\") ON DELETE CASCADE ENABLE ";
        //    comm = new OracleCommand(querry, con);
        //    comm.ExecuteNonQuery();
        //}
        //public void dropVidTable(int id_vida)
        //{
        //    String querry = "drop TABLE  \"VID_" + id_vida.ToString();
        //    OracleCommand comm = new OracleCommand(querry, con);
        //    comm.ExecuteNonQuery();
        //}


        
        public int getIdNumb(int id_komf, int kol_mest)
        {
            int result = 0;
            string querry = "Select ID_NUM from NUMBERS_GOSTINITS where " +
                "KOMFORTABLE.ID_KOMF ='" + id_komf.ToString() + "' and KOL_MEST = '" + kol_mest.ToString() + "'";
            OracleCommand comm = new OracleCommand(querry, con);
            result = (int)comm.ExecuteScalar();
            return result;
        }
        public DataTable getTableNumb()
        {
            DataTable result = new DataTable();

            result.Columns.Add("Количесво мест");
            result.Columns.Add("Комфортность");
            result.Columns.Add("Цена");
            result.Columns.Add("Гостиница");
            String querry = "Select NUMBERS_GOSTINITS.ID_NUM, NUMBERS_GOSTINITS.PRICE, " +
                "NUMBERS_GOSTINITS.KOL_MEST, KOMFORTABLE.ID_KOMF, GOSTINITSA.ID_GOST " +
                " from NUMBERS_GOSTINITS join KOMFORTABLE on KOMFORTABLE.id_komf = NUMBERS_GOSTINITS.id_KOMF " +
                " join GOSTINITSA on GOSTINITSA.id_gost = NUMBERS_GOSTINITS.id_gost";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Rows.Add(
                    (data.IsDBNull(1)) ? "" : data.GetValue(1).ToString(),
                    (data.IsDBNull(2)) ? "" : data.GetValue(2).ToString(),
                    (data.IsDBNull(3)) ? "" : data.GetValue(3).ToString(),
                    (data.IsDBNull(4)) ? "" : data.GetValue(4).ToString());
            }
            data.Close();
            return result;
        }
        public void insNumb(int kol_mest, long price,
            int id_komf, int id_gost)
        {
            string querry = "Insert into NUMBERS_GOSTINITS (ID_NUM , KOL_MEST, PRICE, " +
                "ID_GOST, ID_KOMF) values (null, '" + kol_mest.ToString() + "', " +
                price.ToString() + ",'" + id_komf.ToString() + "', " +
                id_gost.ToString() + ")";
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public void delNumb(int id_num)
        {
            string querry = "Delete from NUMBERS_GOSTINITS where ID_NUM =" +
                id_num.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }

        //komfortnost
        public int getIdKomf(string type)
        {
            int result = 0;
            string querry = "Select ID_KOMF from KOMFORTABLE where " +
                "TYPE ='" + type + "'";
            OracleCommand comm = new OracleCommand(querry, con);
            result = (int)comm.ExecuteScalar();
            return result;
        }
        public void insKomf(int type)
        {
            string querry = "Insert into KOMFORTABLE (ID_KOMF , TYPE) " +
                " values (null, '" +type + ")";
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public void delKomf(int id_komf)
        {
            string querry = "Delete from KOMFORTABLE where ID_KOMF =" +
                id_komf.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public List<String> getListKomf()
        {
            List<String> result = new List<string>();
            String querry = "Select TYPE from KOMFORTABLE";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Add(data.GetValue(0).ToString());
            }
            data.Close();
            return result;
        }

        // arhiv klientov
        public DataTable getTableArhivK()
        {
            DataTable result = new DataTable();

            result.Columns.Add("ФИО");
            result.Columns.Add("Паспорт");
            result.Columns.Add("Дата изменения");
            result.Columns.Add("Комментарий");
            String querry = "Select KLIENTS.ID_KLIENT, KLIENTS_ARHIV.FIO, " +
                "KLIENTS_ARHIV.PASPORT, KLIENTS_ARHIV.KOMMENT, KLIENTS_ARHIV.DATE_IZMENENIA" +
                " from KLIENTS join KLIENTS_ARHIV on  " +
                "KLIENTS.id_klient = KLIENTS_ARHIV.id_klient";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Rows.Add(
                    (data.IsDBNull(1)) ? "" : data.GetValue(1).ToString(),
                    (data.IsDBNull(2)) ? "" : data.GetValue(2).ToString(),
                    (data.IsDBNull(3)) ? "" : data.GetValue(3).ToString(),
                    (data.IsDBNull(4)) ? "" : data.GetValue(4).ToString());
            }
            data.Close();
            return result;
        }

        //poselenie 
        public int getIdPoselen(DateTime date_poselenia, long id_num)
        {
            int result = 0;
            string querry = "Select ID_POSELENIA from POSELENIE where " +
                "DATE_POSELENIA ='" + date_poselenia + "' and NUMBERS_GOSTINITS.ID_NUM = '" + id_num.ToString() + "'";
            OracleCommand comm = new OracleCommand(querry, con);
            result = (int)comm.ExecuteScalar();
            return result;
        }
        public void insPoselen(DateTime date_poselenia, DateTime date_osv, long id_num, 
            int id_klient,int id_broni,int kol_people)
        {
            string querry = "Insert into POSELENIE (ID_POSELENIA , DATE_POSELENIA, DATE_OSV, " +
                "ID_NUM, ID_KLIENT,ID_BRONI,KOL_PEOPLE) values (null, '" + date_poselenia + "', " +
                date_osv.ToString() + ",'" + id_num + "', " +
                id_klient.ToString() + ",'" + id_broni + "','" + kol_people + "')";
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public DataTable getTablePoselen()
        {
            DataTable result = new DataTable();
            result.Columns.Add("Дата поселения");
            result.Columns.Add("Дата освобождения");
            result.Columns.Add("номер");
            result.Columns.Add("Клиент");
            result.Columns.Add("Количество проживающих");
            result.Columns.Add("Номер брони");

            String querry = "Select POSELENIE.ID_POSELENIA, POSELENIE.DATE_POSELENIA, " +
                "POSELENIE.DATE_OSV,NUMBERS_GOSTINITS.ID_NUM, KLIENTS.ID_KLIENT, " +
                "POSELENIE.KOL_PEOPLE,BRONIROVANIE.ID_BRONI " +
                "from POSELENIE join NUMBERS_GOSTINITS on NUMBERS_GOSTINITS.id_num = POSELENIE.id_num " +
                "join KLIENTS  on KLIENTS.id_klient=POSELENIE.id_klient join BRONIROVANIE on  " +
                "BRONIROVANIE.id_broni=POSELENIE.id_broni";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                    result.Rows.Add(
                    (data.IsDBNull(1)) ? "" : data.GetValue(1).ToString(),
                    (data.IsDBNull(2)) ? "" : data.GetValue(2).ToString(),
                    (data.IsDBNull(3)) ? "" : data.GetValue(3).ToString(),
                    (data.IsDBNull(4)) ? "" : data.GetValue(4).ToString(),
                    (data.IsDBNull(5)) ? "" : data.GetValue(5).ToString(),
                    (data.IsDBNull(6)) ? "" : data.GetValue(6).ToString());
            }
            data.Close();
            return result;
        }
        public void delPoselen(int id_poselenia)
        {
            string querry = "Delete from POSELENIE where ID_POSELENIA =" +
                id_poselenia.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }

        //bronirovanie
        public int getIdBronir(DateTime date_poselenia, long id_num)
        {
            int result = 0;
            string querry = "Select ID_BRONI from BRONIROVANIE where " +
                "DATE_POSELENIA ='" + date_poselenia + "' and NUMBERS_GOSTINITS.ID_NUM = '" + id_num.ToString() + "'";
            OracleCommand comm = new OracleCommand(querry, con);
            result = (int)comm.ExecuteScalar();
            return result;
        }
        public void insBronir(DateTime date_poselenia, DateTime date_osv, long id_num,
            int id_klient, int id_broni)
        {
            string querry = "Insert into BRONIROVANIE (ID_BRONI , DATE_POSELENIA, DATE_OSV, " +
                "ID_NUM, ID_KLIENT,ID_BRONI) values (null, '" + date_poselenia + "', " +
                date_osv+ ",'" + id_num.ToString() + "', " +
                id_klient.ToString() + ",'" + id_broni.ToString() + "')";
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public DataTable getTableBronir()
        {
            DataTable result = new DataTable();

            result.Columns.Add("Дата поселения");
            result.Columns.Add("Дата освобождения");
            result.Columns.Add("номер");
            result.Columns.Add("Клиент");
            String querry = "Select BRONIROVANIE.ID_BRONI, BRONIROVANIE.DATE_POSELENIA, " +
                "BRONIROVANIE.DATE_OSV,NUMBERS_GOSTINITS.ID_NUM, KLIENTS.ID_KLIENT " +
               " from BRONIROVANIE  join NUMBERS_GOSTINITS on(NUMBERS_GOSTINITS.id_num = BRONIROVANIE.id_num) " +
                "join KLIENTS on (KLIENTS.id_klient=BRONIROVANIE.id_klient) ";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Rows.Add(
                    (data.IsDBNull(1)) ? "" : data.GetValue(1).ToString(),
                    (data.IsDBNull(2)) ? "" : data.GetValue(2).ToString(),
                    (data.IsDBNull(3)) ? "" : data.GetValue(3).ToString(),
                    (data.IsDBNull(4)) ? "" : data.GetValue(4).ToString());
            }
            data.Close();
            return result;
        }
        public void delBronir(int id_broni)
        {
            string querry = "Delete from BRONIROVANIE where ID_BRONI =" +
                id_broni.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        
        //skidki
        public int getIdSkidki(string nazvanie)
        {
            int result = 0;
            string querry = "Select ID_SKIDKI from SKIDKI where " +
                "NAZVANIE ='" + nazvanie;
            OracleCommand comm = new OracleCommand(querry, con);
            result = (int)comm.ExecuteScalar();
            return result;
        }
        public void insSkidki(int id_skidki, int prozent, string nazvanie)
        {
            string querry = "Insert into SKIDKI (ID_SKIDKI ,PROZENT,NAZVANIE) " +
                " values (null, '" + prozent + "', '" + nazvanie + "')";
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public DataTable getTableSkidki()
        {
            DataTable result = new DataTable();
            result.Columns.Add("Название");
            result.Columns.Add("Процент");
            String querry = "Select SKIDKI.ID_SKIDKI, SKIDKI.PROZENT, " +
                "SKIDKI.NAZVANIE from SKIDKI";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Rows.Add(
                    (data.IsDBNull(1)) ? "" : data.GetValue(1).ToString(),
                    (data.IsDBNull(2)) ? "" : data.GetValue(2).ToString());
            }
            data.Close();
            return result;
        }
        public void delSkidki(int id_skidki)
        {
            string querry = "Delete from SKIDKI where ID_SKIDKI =" +
                id_skidki.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public void updateSkidki(int id_skidki,int prozent, string nazvanie)
        {
            string querry = "Update SKIDKI set "
                + "PROZENT = " + prozent
                + "NAZVANIE = " + nazvanie
                + "where ID_SKIDKI =" + id_skidki.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }

        //skidki vidy
        public DataTable getTableSkV()
        {
            DataTable result = new DataTable();
            result.Columns.Add("Вид");
            result.Columns.Add("Скидка");
            String querry = "Select SKIDKI_VIDAM.ID_SKIDKI, SKIDKI_VIDAM.ID_VIDA  " +
                " from SKIDKI_VIDAM  "; 
                 //" SKIDKI.ID_SKIDKI=SKIDKI_VIDAM.ID_SKIDKI,  VIDI_KLIENTOV.ID_VIDA=SKIDKI_VIDAM=ID_VIDA";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Rows.Add(
                    (data.IsDBNull(1)) ? "" : data.GetValue(1).ToString(),
                    (data.IsDBNull(2)) ? "" : data.GetValue(2).ToString());
            }
            data.Close();
            return result;
        }
        public void insSkV(int id_skidki, int id_vida)
        {
            string querry = "Insert into SKIDKI_VIDAM (ID_SKIDKI, ID_VIDA) " +
                " values ('" + id_skidki + "', '" + id_vida + "')";
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public void delSkV(int id_skidki)
        {
            string querry = "Delete from SKIDKI_VIDAM where ID_SKIDKI =" +
                id_skidki.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        
        //identification
        public DataTable getTableId()
        {
            DataTable result = new DataTable();
            result.Columns.Add("Гостиница");
            result.Columns.Add("Нач.зн.Клиенты");
            result.Columns.Add("Кон.зн.Клиенты");
            result.Columns.Add("Нач.зн.Номер");
            result.Columns.Add("Кон.зн.Номер");
            result.Columns.Add("Нач.зн.Скидки");
            result.Columns.Add("Кон.зн.Скидки");
            result.Columns.Add("Нач.зн.Вида");
            result.Columns.Add("Кон.зн.Вида");
            result.Columns.Add("Нач.зн.Поселения");
            result.Columns.Add("Кон.зн.Поселения");
            result.Columns.Add("Нач.зн.Брони");
            result.Columns.Add("Кон.зн.Брони");
            result.Columns.Add("Нач.зн.Комфорт.");
            result.Columns.Add("Кон.зн.Комфорт.");
            String querry = "Select KLIENTS.ID_KLIENT, NUMBERS_GOSTINITS.ID_NUM, " +
                "SKIDKI.ID_SKIDKI, VIDI_KLIENTOV.ID_VIDA, POSELENIE.ID_POSELENIA," +
                "BRONIROVANIE.ID_BRONI, KOMFORTABLE.ID_KOMF" +
                " from KLIENTS join NUMBERS_GOSTINITS join SKIDKI join VIDI_KLIENTOV join POSELENIE join BRONIROVANIE join KOMFORTABLE on  " +
                "VIDI_KLIENTOV.id_vida = KLIENTS.id_vida";
            OracleCommand comm = new OracleCommand(querry, con);
            OracleDataReader data = comm.ExecuteReader();
            while (data.Read())
            {
                result.Rows.Add(
                    (data.IsDBNull(1)) ? "" : data.GetValue(1).ToString(),
                    (data.IsDBNull(2)) ? "" : data.GetValue(2).ToString(),
                    (data.IsDBNull(3)) ? "" : data.GetValue(3).ToString(),
                    (data.IsDBNull(4)) ? "" : data.GetValue(4).ToString(),
                    (data.IsDBNull(5)) ? "" : data.GetValue(5).ToString(),
                    (data.IsDBNull(6)) ? "" : data.GetValue(6).ToString(),
                    (data.IsDBNull(7)) ? "" : data.GetValue(7).ToString(),
                    (data.IsDBNull(8)) ? "" : data.GetValue(8).ToString(),
                    (data.IsDBNull(9)) ? "" : data.GetValue(9).ToString(),
                    (data.IsDBNull(10)) ? "" : data.GetValue(10).ToString(),
                    (data.IsDBNull(11)) ? "" : data.GetValue(11).ToString(),
                    (data.IsDBNull(12)) ? "" : data.GetValue(12).ToString(),
                    (data.IsDBNull(13)) ? "" : data.GetValue(13).ToString(),
                    (data.IsDBNull(14)) ? "" : data.GetValue(14).ToString(),
                    (data.IsDBNull(15)) ? "" : data.GetValue(15).ToString());
            }
            data.Close();
            return result;
        }
        public void insId(string fio, long pasport, string comment,
            int id_vida)
        {
            string querry = "Insert into KLIENTS (ID_KLIENT , FIO, PASPORT, " +
                "KOMMENT, ID_VIDA) values (null, '" + fio + "', " +
                pasport.ToString() + ",'" + comment + "', " +
                id_vida.ToString() + ")";
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public void delId(int id_gost)
        {
            string querry = "Delete from IDENT where ID_GOST =" +
                id_gost.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }
        public void updateId(int id_klient, string fio, long pasport,
            string comment, int id_vida)
        {
            string querry = "Update KLIENTS set "
                + "FIO = " + fio
                + "PASPORT = " + pasport
                + "KOMMENT = " + comment
                + "ID_VIDA = " + id_vida
                + "where ID_KLIENT =" + id_klient.ToString();
            OracleCommand comm = new OracleCommand(querry, con);
            comm.ExecuteNonQuery();
        }

    } 
  
}
