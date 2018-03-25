using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFTOSQLSERVER.codigo
{
    public  class procesoDbf
    {
        public string convertirDBFtoText(string rutaArchivo,string nombreTabla) {
            string aux = string.Empty;
            DbfDataReader.DbfTable tabla = new DbfDataReader.DbfTable(rutaArchivo);
            DbfDataReader.DbfRecord filas = new DbfDataReader.DbfRecord(tabla);
            string[] tipos = new string[tabla.Columns.Count];
            int contador = 0;
            foreach (var dbfColumn in tabla.Columns)
            {
                string tipo = Convert.ToString(dbfColumn.ColumnType);
                tipos[contador] = tipo;
                contador++;
            }
            while (tabla.Read(filas)) {
                contador = 0;
                aux += string.Format("INSERT INTO {0} VALUES(",nombreTabla);
                foreach (var dbfValue in filas.Values)
                {
                    var stringValue = dbfValue.ToString();
                    var obj = dbfValue.GetValue();
                    obj = (string.IsNullOrWhiteSpace(Convert.ToString(obj)) && tipos[contador].Equals("Number") ? "null" : obj);
                    aux += string.Format((tipos[contador].Equals("Number"))?"{0}":"'{0}'",obj);
                    aux += ",";
                    contador++;
                }
                aux = aux.Substring(0,aux.Length-1);
                aux += ");\n";
            }
            return aux;
        }
    }
}
