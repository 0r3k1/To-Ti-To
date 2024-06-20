using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace totito {
    class Program {

        struct cord {
            public int x;
            public int y;
        }

        static void Main(string[] args) {

            int[] tablero = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            cord[] coordenadas = new cord[9];
            cord posicion = new cord();
            
            int op = 0;

            Random random = new Random();
           

            

            for (int j = 0; j < 3; j++) {
                for (int i = 0; i < 3; i++) {
                    cord p = new cord {
                        x = (i * 10) + 4,
                        y = (j * 6) + 7
                    };
                    coordenadas[(j * 3) + i] = p;
                }
            }

            do {
                bool turnoP = false;
                bool gameOver = false;
                bool cpu = false;

                for(int i=0; i<tablero.Length; i++) {
                    tablero[i] = 0;
                }

                int numero = random.Next(1, 3);
                turnoP = numero == 1 ? true : false;

                Console.Clear();
                
                encavezado();

                op = mainMenu();
                if (op == 2) cpu = true;

                pintarTabla(coordenadas);

                //Game Loop
                while (!gameOver && op != 3) {

                    if (!cpu) posicion = turno(turnoP);
                    else posicion = turnoConCpu(tablero, turnoP);

                    if (turnoP) {
                        if (tablero[(posicion.y * 3) + posicion.x] == 0) tablero[(posicion.y * 3) + posicion.x] = 1;
                        else {
                            Console.Clear();
                            gotoxy(0, 12);
                            Console.Write(centeredString("Pocicion incorecta verifique y buelva a intentar"));
                            Console.ReadKey();
                            pintarTabla(coordenadas);
                            pintarFichas(tablero, coordenadas);
                            continue;
                        }
                    } else {
                        if (tablero[(posicion.y * 3) + posicion.x] == 0) tablero[(posicion.y * 3) + posicion.x] = 2;
                        else {
                            Console.Clear();
                            gotoxy(0, 12);
                            Console.Write(centeredString("Pocicion incorecta verifique y buelva a intentar"));
                            Console.ReadKey();
                            pintarTabla(coordenadas);
                            pintarFichas(tablero, coordenadas);
                            continue;
                        }
                    }


                    pintarTabla(coordenadas);
                    pintarFichas(tablero, coordenadas);

                    gameOver = ganador(tablero, cpu);
                    if (!gameOver) gameOver = empate(tablero);

                    turnoP = !turnoP;
                }
            } while (op != 3);

            //gotoxy(0, 23);

        }

        static void gotoxy(int x, int y) { Console.SetCursorPosition(x, y); }

        static void printLayout(ConsoleColor color, int w = 5, int h = 5) {
            Console.BackgroundColor = color;
            for (int i = 0; i < h; i++) {
                gotoxy(0, i);
                Console.Write(new string(' ', w));
            }
        }

        static void printLayoutInCoord(ConsoleColor color, int x = 0, int y = 0, int w = 5, int h = 5) {
            Console.BackgroundColor = color;
            for (int i = 0; i < h; i++) {
                gotoxy(x, y + i);
                Console.Write(new string(' ', w));
            }
            gotoxy(0, 0);
        }

        static void encavezado() {
            printLayout(ConsoleColor.Blue, 80, 6);
            gotoxy(0, 1);
            Console.Write(centeredString("Cristobal Rodas"));
            Console.Write(centeredString("To Ti To"));
            Console.Write(centeredString(".: Todos los derechos son de libre uso :."));
        }

        static string centeredString(string s, int width = 80) {

            if (s.Length >= width) return s;

            int leftPadding = (width - s.Length) / 2;
            int rightPadding = width - s.Length - leftPadding;

            return new string(' ', leftPadding) + s + new string(' ', rightPadding);
        }

        static int mainMenu() {
            string[] titulo =  { "████████╗ ██████╗    ████████╗██╗   ████████╗ ██████╗ ",
                                 "╚══██╔══╝██╔═══██╗   ╚══██╔══╝██║   ╚══██╔══╝██╔═══██╗",
                                 "   ██║   ██║   ██║█████╗██║   ██║█████╗██║   ██║   ██║",
                                 "   ██║   ██║   ██║╚════╝██║   ██║╚════╝██║   ██║   ██║",
                                 "   ██║   ╚██████╔╝      ██║   ██║      ██║   ╚██████╔╝",
                                 "   ╚═╝    ╚═════╝       ╚═╝   ╚═╝      ╚═╝    ╚═════╝ "};
            int op = 0;

            printLayoutInCoord(0, 7, 80, 18);
            gotoxy(0, 7);
            foreach (string linea in titulo) {
                Console.Write(centeredString(linea));
            }

            gotoxy(0, 16);
            Console.Write(centeredString("01) p1 vs p2 "));
            Console.Write(centeredString("02) p1 vs cpu"));
            Console.Write(centeredString("03) salir    "));
            gotoxy(33,19);
            Console.Write("op: ");
            op = int.Parse(Console.ReadLine());


            return op;
        }

        static void pintarTabla(cord[] coordenadas) {

            printLayoutInCoord(ConsoleColor.Black, 0, 7, 80, 20);
            gotoxy(0, 7);
            string[] tabla = { "          ║         ║          ",
                               "          ║         ║          ",
                               "          ║         ║          ",
                               "          ║         ║          ",
                               "          ║         ║          ",
                               "══════════╬═════════╬══════════",
                               "          ║         ║          ",
                               "          ║         ║          ",
                               "          ║         ║          ",
                               "          ║         ║          ",
                               "          ║         ║          ",
                               "══════════╬═════════╬══════════",
                               "          ║         ║          ",
                               "          ║         ║          ",
                               "          ║         ║          ",
                               "          ║         ║          ",
                               "          ║         ║          ",};

            foreach (string linea in tabla) {
                Console.WriteLine($"   {linea}");
            }

            for (int j = 0; j < 3; j++) {
                for (int i = 0; i < 3; i++) {
                    int p = (j * 3) + i;
                    gotoxy(coordenadas[p].x + 4, coordenadas[p].y + 2);
                    Console.Write(p);
                }
            }
        }

        static void pintarficha(cord point, cord[] coordenadas, bool p1 = true) {
            string[] circulo = { "    ▄    ",
                                 "  █   █  ",
                                 " █     █ ",
                                 "  █   █  ",
                                 "    ▀    ",
            };

            string[] equis = { "█       █",
                               "  █   █  ",
                               "    █    ",
                               "  █   █  ",
                               "█       █",
            };

            for (int i = 0; i < 5; i++) {
                int p = (point.y * 3) + point.x;
                gotoxy(coordenadas[p].x, i + coordenadas[p].y);
                if (p1) Console.Write(equis[i]);
                else Console.Write(circulo[i]);
            }
        }

        static void pintarFichas(int[] tabla, cord[] coordenadas) {
            for (int j = 0; j < 3; j++) {
                for (int i = 0; i < 3; i++) {
                    int p = (j * 3) + i;
                    cord punto = new cord() {
                        x = i,
                        y = j
                    };
                    if (tabla[p] == 0) continue;
                    else if (tabla[p] == 1) pintarficha(punto, coordenadas);
                    else if (tabla[p] == 2) pintarficha(punto, coordenadas, false);
                }
            }
        }

        static cord turno(bool turnoP) {
            cord p = new cord();
            int pos;

            gotoxy(43, 9);
            if (turnoP) Console.Write(centeredString("Turno P1", 23));
            else Console.Write(centeredString("Turno P2", 23));
            gotoxy(43, 10);
            Console.Write(centeredString("Introdusca una pocicion", 23));
            gotoxy(43, 11);
            Console.Write(centeredString("del tablero", 23));
            gotoxy(53, 12);
            pos = int.Parse(Console.ReadLine());

            p.x = pos % 3;
            p.y = pos / 3;

            return p;
        }

        static bool mov0(int[] tablero) {
            for (int i = 0; i < tablero.Length; i++) {
                if (tablero[i] == 0) return false;
            }

            return true;
        }

        static bool empate(int[] tablero) {

            if (!mov0(tablero)) return false;
            Thread.Sleep(1000);

            string[] empt = {
                "███████╗███╗   ███╗██████╗  █████╗ ████████╗███████╗",
                "██╔════╝████╗ ████║██╔══██╗██╔══██╗╚══██╔══╝██╔════╝",
                "█████╗  ██╔████╔██║██████╔╝███████║   ██║   █████╗  ",
                "██╔══╝  ██║╚██╔╝██║██╔═══╝ ██╔══██║   ██║   ██╔══╝  ",
                "███████╗██║ ╚═╝ ██║██║     ██║  ██║   ██║   ███████╗",
                "╚══════╝╚═╝     ╚═╝╚═╝     ╚═╝  ╚═╝   ╚═╝   ╚══════╝"
                };

            Console.Clear();
            gotoxy(0, 7);
            foreach (string linea in empt) {
                Console.Write(centeredString(linea));
            }

            Console.ReadKey();

            return true;
        }

        static bool evaluarTablero(int[] tablero, int pl) {

            if (((tablero[0] == pl) && (tablero[1] == pl) && (tablero[pl] == pl)) ||
                (tablero[3] == pl) && (tablero[4] == pl) && (tablero[5] == pl) ||
                (tablero[6] == pl) && (tablero[7] == pl) && (tablero[8] == pl) ||
                (tablero[0] == pl) && (tablero[4] == pl) && (tablero[8] == pl) ||
                (tablero[2] == pl) && (tablero[4] == pl) && (tablero[6] == pl) ||
                (tablero[0] == pl) && (tablero[3] == pl) && (tablero[6] == pl) ||
                (tablero[1] == pl) && (tablero[4] == pl) && (tablero[7] == pl) ||
                (tablero[2] == pl) && (tablero[5] == pl) && (tablero[8] == pl))
                return true;

            return false;
        }

        static bool ganador(int[] tablero, bool gcpu = false) {
            
            if (evaluarTablero(tablero, 1)) {
                Thread.Sleep(1000);
                string[] p1 = {
                    " ██████╗  █████╗ ███╗   ██╗ █████╗ ██████╗  ██████╗ ██████╗ ",
                    "██╔════╝ ██╔══██╗████╗  ██║██╔══██╗██╔══██╗██╔═══██╗██╔══██╗",
                    "██║  ███╗███████║██╔██╗ ██║███████║██║  ██║██║   ██║██████╔╝",
                    "██║   ██║██╔══██║██║╚██╗██║██╔══██║██║  ██║██║   ██║██╔══██╗",
                    "╚██████╔╝██║  ██║██║ ╚████║██║  ██║██████╔╝╚██████╔╝██║  ██║",
                     "╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚═════╝  ╚═════╝ ╚═╝  ╚═╝",
                    " ",
                    "██████╗  ██╗",
                    "██╔══██╗███║",
                    "██████╔╝╚██║",
                    "██╔═══╝  ██║",
                    "██║      ██║",
                    "╚═╝      ╚═╝"
                };

                Console.Clear();
                gotoxy(0, 5);
                foreach (string linea in p1) {
                    Console.Write(centeredString(linea));
                }
                Console.ReadKey();
                return true;
            }

            if (evaluarTablero(tablero, 2)) {
                Thread.Sleep(1000);
                string[] p2 = {
                    " ██████╗  █████╗ ███╗   ██╗ █████╗ ██████╗  ██████╗ ██████╗ ",
                    "██╔════╝ ██╔══██╗████╗  ██║██╔══██╗██╔══██╗██╔═══██╗██╔══██╗",
                    "██║  ███╗███████║██╔██╗ ██║███████║██║  ██║██║   ██║██████╔╝",
                    "██║   ██║██╔══██║██║╚██╗██║██╔══██║██║  ██║██║   ██║██╔══██╗",
                    "╚██████╔╝██║  ██║██║ ╚████║██║  ██║██████╔╝╚██████╔╝██║  ██║",
                    " ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚═════╝  ╚═════╝ ╚═╝  ╚═╝",
                    " "
                };

                string[] pl2 = { 
                    "██████╗ ██████╗ ",
                    "██╔══██╗╚════██╗",
                    "██████╔╝ █████╔╝",
                    "██╔═══╝ ██╔═══╝ ",
                    "██║     ███████╗",
                    "╚═╝     ╚══════╝"
                };

                string[] pcpu = {
                    " ██████╗██████╗ ██╗   ██╗",
                    "██╔════╝██╔══██╗██║   ██║",
                    "██║     ██████╔╝██║   ██║",
                    "██║     ██╔═══╝ ██║   ██║",
                    "╚██████╗██║     ╚██████╔╝",
                    " ╚═════╝╚═╝      ╚═════╝ "
                };

                Console.Clear();
                gotoxy(0, 5);
                foreach (string linea in p2) {
                    Console.Write(centeredString(linea));
                }

                if (!gcpu) {
                    foreach (string linea in pl2) {
                        Console.Write(centeredString(linea));
                    }
                } else {
                    foreach (string linea in pcpu) {
                        Console.Write(centeredString(linea));
                    }
                }
                Console.ReadKey();
                return true;
            }

            return false;
        }

        static int minMax(int[] tablero, bool isMaximizing) {
            if (evaluarTablero(tablero, 1) || evaluarTablero(tablero, 2)) {
                return isMaximizing ? -1 : 1;
            }

            if (mov0(tablero)) {
                return 0;
            }

            int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

            for (int i = 0; i < 9; i++) {
                if (tablero[i] == 0) {
                    tablero[i] = isMaximizing ? 2 : 1;
                    int score = minMax(tablero, !isMaximizing);
                    tablero[i] = 0;

                    bestScore = isMaximizing ? Math.Max(score, bestScore) : Math.Min(score, bestScore);
                }
            }

            return bestScore;
        }


        static cord turnoConCpu(int[] tablero, bool turnoP) {
            cord p = new cord();
            if (turnoP) {
                return turno(turnoP);
            } else {
                int bestScore = int.MinValue;
                int move = -1;

                for (int i = 0; i < 9; i++) {
                    if (tablero[i] == 0) {
                        tablero[i] = 2;
                        int score = minMax(tablero, false);
                        tablero[i] = 0;

                        if (score > bestScore) {
                            bestScore = score;
                            move = i;
                        }
                    }
                }

                p.x = move % 3;
                p.y = move / 3;
            }

            return p;
        }

    }
}
