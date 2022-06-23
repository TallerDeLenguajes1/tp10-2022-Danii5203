using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace civilizaciones
{
    class Program
    {
        public static void Main(string[] args)
        {
            getCivilizaciones();
        }
        public static void getCivilizaciones()
        {
            var url = $"https://age-of-empires-2-api.herokuapp.com/api/v1/civilizations";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if(strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string strCivilizaciones = objReader.ReadToEnd();
                            civilizaciones? civilizacionesAOF = JsonSerializer.Deserialize<civilizaciones>(strCivilizaciones);

                            Console.WriteLine("===============================================");
                            Console.WriteLine("===== CIVILIZACIONES DISPONIBLES");
                            Console.WriteLine("===============================================");
                            foreach (var infoCiv in civilizacionesAOF.Civilizations)
                            {
                                Console.WriteLine($"ID: {infoCiv.Id} | NOMBRE: {infoCiv.Name}");
                            }

                            Console.WriteLine("\n- Elija una civilizacion para mostrar sus caracteristicas mediante su ID:");
                            int respuestaId = Convert.ToInt32(Console.ReadLine());

                            
                            foreach (var infoCiv in civilizacionesAOF.Civilizations)
                            {
                                if (infoCiv.Id == respuestaId)
                                {
                                    Console.WriteLine("\n===============================================");
                                    Console.WriteLine("===== INFORMACION DE LA CIVILIZACION");
                                    Console.WriteLine("===============================================");
                                    Console.WriteLine($"ID: {infoCiv.Id}");
                                    Console.WriteLine($"NOMBRE: {infoCiv.Name}");
                                    Console.WriteLine($"EXPANCION: {infoCiv.Expansion}");
                                    Console.WriteLine($"TIPO DE EJERCITO: {infoCiv.ArmyType}");

                                    Console.WriteLine($"UNIDAD UNICA:"); //api de la informacion sobre la unidad unica
                                    foreach (var unidadUnica in infoCiv.UniqueUnit)
                                    {
                                        Console.WriteLine($"- {unidadUnica}");
                                    }

                                    Console.WriteLine($"TECNOLOGIA UNICA:"); //api de la informacion sobre la unidad unica
                                    foreach (var tecUnica in infoCiv.UniqueTech)
                                    {
                                        Console.WriteLine($"- {tecUnica}");
                                    }

                                    Console.WriteLine($"BONUS DE EQUIPO: {infoCiv.TeamBonus}");
                                    Console.WriteLine($"BONUS DE CIVILIZACION:"); 
                                    foreach (var civBonus in infoCiv.CivilizationBonus)
                                    {
                                        Console.WriteLine($"- {civBonus}");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                
                throw;
            }
        }
    }
}