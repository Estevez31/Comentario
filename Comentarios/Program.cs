using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Comentarios
{
    class Program
    {
        class Comentario
        {
            protected int id;
            protected string autor;
            protected DateTime fechaPublic;
            protected string comentarios;
            protected int direccionIp;
            protected string es_inapropiado;
            protected int likes;

            public Comentario(int direccionIp, int id, string comentarios, string autor, DateTime fechaPublic, int likes, string es_inapropiado)
            {
                DireccionIp = direccionIp;
                Id = id;
                Comentarios = comentarios;
                Autor = autor;
                FechaPublic = fechaPublic;
                Likes = likes;
                Es_inapropiado = es_inapropiado;
            }
            public int DireccionIp { get => direccionIp; set => direccionIp = value; }
            public int Id { get => id; set => id = value; }
            public string Autor { get => autor; set => autor = value; }
            public string Comentarios { get => comentarios; set => comentarios = value; }
            public DateTime FechaPublic { get => fechaPublic; set => fechaPublic = value; }
            public int Likes { get => likes; set => likes = value; }
            public string Es_inapropiado { get => es_inapropiado; set => es_inapropiado = value; }

            public override string ToString()
            {
                return String.Format($"Ip: {DireccionIp}, Id: {Id}, {Comentarios}, {Autor}, Fecha: {FechaPublic}, Likes: {Likes}, Es comentario inapropieado: {Es_inapropiado}");
            }

        }
    
        class Autor : Comentario
        {
            private string correo;
            private string usuario;

            public string Correo { get => correo; set => correo = value; }
            public string Usuario { get => usuario; set => usuario = value; }

            public Autor(int DireccionIP, int Id, string Comentario, string Autor, string correo, string usuario, DateTime FechaPublic, int Likes, string es_inapropiado) : base (DireccionIP,Id,Comentario, Autor,FechaPublic,Likes, es_inapropiado)
            {
                Correo = correo;
                Usuario = usuario;
            }
            public override string ToString()
            {
                return base.ToString() + string.Format($", Correo: {Correo}, Usuario: {Usuario}"); 
            }
        }
        class ComentarioDB
        {
            public static void SaveToFile(List<Comentario> comentarios, string path)
            {
                StreamWriter textOut = null;
                try
                {
                    textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                    foreach (var comentario in comentarios)
                    {
                        textOut.Write(comentario.DireccionIp + "|");
                        textOut.Write(comentario.Id + "|");
                        textOut.Write(comentario.Comentarios + "|");
                        textOut.Write(comentario.Autor + "|");
                        textOut.Write(comentario.FechaPublic + "|");
                        textOut.Write(comentario.Likes + "|");
                        textOut.WriteLine(comentario.Es_inapropiado);
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Ya existe el archivo");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                finally
                {
                    if (textOut != null)
                        textOut.Close();
                }
            }

            public static List<Comentario> ReadFromFile(string path)
            {
                List<Comentario> comentarios = new List<Comentario>();
                StreamReader textIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
                try
                {
                    while (textIn.Peek() != -1)
                    {
                        string row = textIn.ReadLine();
                        string[] columnas = row.Split('|');
                        Comentario c = new Autor(4781586, 012458, "Me gusta la pizza", "Ana Lee", "ana.lee@gmail.com", "Ana.Lee", new DateTime(2021, 05, 28), 10, "No");
                        c.DireccionIp = int.Parse(columnas[0]);
                        c.Id = int.Parse(columnas[1]);
                        c.Comentarios = columnas[2];
                        c.Autor = columnas[3];
                        c.FechaPublic = DateTime.Parse(columnas[4]);
                        c.Likes = int.Parse(columnas[5]);
                        c.Es_inapropiado = columnas[6];
                        comentarios.Add(c);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    textIn.Close();
                }
                return comentarios;
            }
        }

        static void Main(string[] args)
        {
            /*List<Comentario> comentarios = new List<Comentario>();
            comentarios.Add(new Autor(4781586, 012458, "Me gusta la pizza", "Ana Lee", "ana@gmail.com", "Ana.Lee",new DateTime(2021,05,31), 10, "No"));
            comentarios.Add(new Autor(3781784, 012478, "Hueles a huevo","Tom Lee", "tom@gmail.com", "Tom.Lee", new DateTime(2021,05,28), 5, "Si"));
            comentarios.Add(new Autor(4778582, 012487, "Hoy es un día hermoso", "Ana Todd","todd@gmail.com","Anna.Tood", new DateTime(2021,05,29), 15, "No"));
            comentarios.Add(new Autor(5787483, 012871, "La pelicula esta chafa", "Guss Ryder", "ryder@gmail.com", "Mr.Ryder", new DateTime(2021,05,26), 3, "Si"));
            comentarios.Add(new Autor(4781582, 017836, "Somos campeones", "Edgar Diaz", "eddiaz@gmail.com", "Ed.Diaz", new DateTime(2021,05,22), 13, "No"));
            comentarios.Add(new Autor(7781584, 012781, "Se que puedo lograrlo","Emma Shu", "emmas@gmail.com", "Emma.S", new DateTime(2021,05,30), 21, "No"));
            */
            //ComentarioDB.SaveToFile(comentarios, @"C:\Users\Dell\Prueba\comentarios.txt");   
            
           List<Comentario> comentarios = ComentarioDB.ReadFromFile(@"C:\Users\Dell\Prueba\comentarios.txt");
            foreach (var c in comentarios)
                Console.WriteLine(c);
            
            Console.WriteLine();

            Console.WriteLine("¿Deseas eliminar un comentario?");
            string Del = Console.ReadLine();

            if (Del == "Si" || Del == "SI" || Del == "si" || Del == "sI")
            {
                try
                {
                    Console.WriteLine("¿Introduce el Id?");
                    int borrar = Int32.Parse(Console.ReadLine());

                    Console.WriteLine();

                    
                    var id = from c in comentarios
                             where c.Id != borrar
                             select c;

                    foreach (var c in id)
                        Console.WriteLine(c);

                    Console.WriteLine();

                    foreach (var c in comentarios)
                      if (c.Id == borrar)
                      {

                            var fecha_s = (from a in comentarios
                                          orderby a.FechaPublic descending
                                          select a)
                                          .Skip(1).Take(5);
                            foreach (var a in fecha_s)
                                Console.WriteLine(a);

                            Console.WriteLine();

                            var likes = (from a in comentarios
                                        orderby a.Likes ascending
                                        select a)
                                        .Skip(1).Take(5);
                            foreach (var a in likes)
                                Console.WriteLine(a);

                            Console.WriteLine();

                            var es_inapropiados = (from a in comentarios
                                                  where a.Es_inapropiado == "No"
                                                  select a)
                                                  .Skip(1).Take(5);
                            foreach (var a in es_inapropiados)
                                Console.WriteLine(a);

                            Console.WriteLine();
                      }
                }

                catch (FormatException e)
                {
                    Console.WriteLine("Solo se aceptan digitos");
                    Console.WriteLine(e);
                }

                catch (OverflowException e)
                {
                    Console.WriteLine($"Los valores debe ser menor a {Int16.MaxValue}");
                    Console.WriteLine(e);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
            else
            {
                Console.WriteLine();

                var fecha_s = from a in comentarios
                              orderby a.FechaPublic descending
                              select a;
                foreach (var a in fecha_s)
                    Console.WriteLine(a);

                Console.WriteLine();

                var likes = from a in comentarios
                            orderby a.Likes ascending
                            select a;
                foreach (var a in likes)
                    Console.WriteLine(a);

                Console.WriteLine();

                var es_inapropiados = from a in comentarios
                                      where a.Es_inapropiado == "No"
                                      select a;
                foreach (var a in es_inapropiados)
                    Console.WriteLine(a);
            }

            Console.ReadKey();
        }
    }
}
