using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

namespace AT28C64B.IAOV
{
    #region Program

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = ConsoleDisplayMessages[(int)ConsoleDisplay.Title];

            /*
             * Initialize the WiringPi wrapper
             * Only in release mode
             */

#if !DEBUG
            Pi.Init<BootstrapWiringPi>();
#endif

            while (true)
            {
                Console.WriteLine(ConsoleDisplayMessages[(int)ConsoleDisplay.InfoMessage]);
                Console.WriteLine();

                Console.WriteLine(ConsoleDisplayMessages[(int)ConsoleDisplay.SelectTestSuit]);

                Console.WriteLine($"Enter {(int)SuitOption.InfrastructurePhisicalSanityCheck}. - To run {OptionName[(int)SuitOption.InfrastructurePhisicalSanityCheck]}");

                Console.WriteLine($"Enter {(int)SuitOption.OfflineVerification}. - To run {OptionName[(int)SuitOption.OfflineVerification]}");

                var opt = Console.ReadKey();

                switch (opt.Key)
                {
                    #region Signals Sanity (Smoke) Select

                    case ConsoleKey.NumPad1:
                        Console.WriteLine($"\n[INFO] You have selected {OptionName[(int)SuitOption.InfrastructurePhisicalSanityCheck]}\n\r");
                        SanitySignalChecks();
                        break;

                    case ConsoleKey.D1:
                        Console.WriteLine($"\n[INFO] You have selected {OptionName[(int)SuitOption.InfrastructurePhisicalSanityCheck]}\n\r");
                        SanitySignalChecks();
                        break;

                    #endregion Signals Sanity (Smoke) Select

                    #region Exhaustive Truth Table Transition Sweeps Select

                    case ConsoleKey.NumPad2:
                        Console.WriteLine($"\n[INFO] You have selected {OptionName[(int)SuitOption.OfflineVerification]}\n\r");
                        OfflineExhaustiveTruthTableTestRunner.Run();
                        Console.ReadLine();
                        break;

                    case ConsoleKey.D2:
                        Console.WriteLine($"\n[INFO] You have selected {OptionName[(int)SuitOption.OfflineVerification]}\n\r");
                        OfflineExhaustiveTruthTableTestRunner.Run();
                        Console.ReadLine();
                        break;

                    #endregion Exhaustive Truth Table Transition Sweeps Select

                    #region Option three selected

                    //case ConsoleKey.NumPad3:
                    //    Console.WriteLine($"\n[INFO] You have selected {OptionName[(int)SuitOption.OperationalScenarioSuite]}\n\r");

                    //    Console.ReadLine();
                    //    break;

                    //case ConsoleKey.D3:
                    //    Console.WriteLine($"\n[INFO] You have selected {OptionName[(int)SuitOption.OperationalScenarioSuite]}\n\r");

                    //    Console.ReadLine();
                    //    break;

                    #endregion Option three selected

                    #region Invalid selection

                    default:
                        Console.WriteLine($"[INFO] {OptionName[(int)SuitOption.InvalidOption]}");

                        Console.ReadKey();
                        break;

                        #endregion Invalid selection
                }

                Console.Clear();
            }
        }

        #region Infrastructure Sanity Check

        /*
         * Before evaluating state logic, 
         * Run an automated pin self-test to verify the physical V&V setup
         */

        #region Select Sanity Check Signal

        static void SanitySignalChecks()
        {
            Console.WriteLine($"{ConsoleDisplayMessages[(int)ConsoleDisplay.SelectSignalForSanityCheck]}");

            Console.WriteLine($"Enter {(int)SanityCheckSignal.PulseA0Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseA0Signal]}");
            Console.WriteLine($"Enter {(int)SanityCheckSignal.PulseA1Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseA1Signal]}");
            Console.WriteLine($"Enter {(int)SanityCheckSignal.PulseA2Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseA2Signal]}");
            Console.WriteLine($"Enter {(int)SanityCheckSignal.PulseA3Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseA3Signal]}");
            Console.WriteLine($"Enter {(int)SanityCheckSignal.PulseA4Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseA4Signal]}");
            Console.WriteLine($"Enter {(int)SanityCheckSignal.PulseA5Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseA5Signal]}");
            Console.WriteLine($"Enter {(int)SanityCheckSignal.PulseA6Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseA6Signal]}");
            Console.WriteLine($"Enter {(int)SanityCheckSignal.PulseA7Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseA7Signal]}");
            Console.WriteLine($"Enter {(int)SanityCheckSignal.PulseA8Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseA8Signal]}");

            Console.WriteLine($"Enter {(char)SanityCheckSignal.PulseIO0Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO0Signal]}");
            Console.WriteLine($"Enter {(char)SanityCheckSignal.PulseIO1Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO1Signal]}");
            Console.WriteLine($"Enter {(char)SanityCheckSignal.PulseIO2Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO2Signal]}");
            Console.WriteLine($"Enter {(char)SanityCheckSignal.PulseIO3Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO3Signal]}");
            Console.WriteLine($"Enter {(char)SanityCheckSignal.PulseIO4Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO4Signal]}");
            Console.WriteLine($"Enter {(char)SanityCheckSignal.PulseIO5Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO5Signal]}");
            Console.WriteLine($"Enter {(char)SanityCheckSignal.PulseIO6Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO6Signal]}");
            Console.WriteLine($"Enter {(char)SanityCheckSignal.PulseIO7Signal}. - To {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO7Signal]}");


            var opt = Console.ReadKey();

            switch (opt.Key)
            {
                #region Sanity Check Signal A0

                case ConsoleKey.NumPad1:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA0Signal]}\n\r");
                    PulseA0Signal();
                    Console.ReadLine();
                    break;
                case ConsoleKey.D1:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA0Signal]}\n\r");
                    PulseA0Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal A0

                #region Sanity Check Signal A1

                case ConsoleKey.NumPad2:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA1Signal]}\n\r");
                    PulseA1Signal();
                    Console.ReadLine();
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA1Signal]}\n\r");
                    PulseA1Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal A1

                #region Sanity Check Signal A2

                case ConsoleKey.NumPad3:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA2Signal]}\n\r");
                    PulseA2Signal();
                    Console.ReadLine();
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA2Signal]}\n\r");
                    PulseA2Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal A2

                #region Sanity Check Signal A3

                case ConsoleKey.NumPad4:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA3Signal]}\n\r");
                    PulseA3Signal();
                    Console.ReadLine();
                    break;
                case ConsoleKey.D4:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA3Signal]}\n\r");
                    PulseA3Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal A3

                #region Sanity Check Signal A4

                case ConsoleKey.NumPad5:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA4Signal]}\n\r");
                    PulseA4Signal();
                    Console.ReadLine();
                    break;
                case ConsoleKey.D5:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA4Signal]}\n\r");
                    PulseA4Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal A4

                #region Sanity Check Signal A5

                case ConsoleKey.NumPad6:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA5Signal]}\n\r");
                    PulseA5Signal();
                    Console.ReadLine();
                    break;
                case ConsoleKey.D6:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA5Signal]}\n\r");
                    PulseA5Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal A5

                #region Sanity Check Signal A6

                case ConsoleKey.NumPad7:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA6Signal]}\n\r");
                    PulseA6Signal();
                    Console.ReadLine();
                    break;
                case ConsoleKey.D7:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA6Signal]}\n\r");
                    PulseA6Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal A6

                #region Sanity Check Signal A7

                case ConsoleKey.NumPad8:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA7Signal]}\n\r");
                    PulseA7Signal();
                    Console.ReadLine();
                    break;
                case ConsoleKey.D8:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA7Signal]}\n\r");
                    PulseA7Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal A7

                #region Sanity Check Signal A8

                case ConsoleKey.NumPad9:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA8Signal]}\n\r");
                    PulseA8Signal();
                    Console.ReadLine();
                    break;
                case ConsoleKey.D9:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseA8Signal]}\n\r");
                    PulseA8Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal A8

                #region Sanity Check Signal I/O 0

                case ConsoleKey.A:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO0Signal]}\n\r");
                    PulseIO_0Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal I/O 0

                #region Sanity Check Signal I/O 1

                case ConsoleKey.B:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO1Signal]}\n\r");
                    PulseIO_1Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal I/O 1

                #region Sanity Check Signal I/O 2

                case ConsoleKey.C:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO2Signal]}\n\r");
                    PulseIO_2Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal I/O 2

                #region Sanity Check Signal I/O 3

                case ConsoleKey.D:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO3Signal]}\n\r");
                    PulseIO_3Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal I/O 3

                #region Sanity Check Signal I/O 4

                case ConsoleKey.E:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO4Signal]}\n\r");
                    PulseIO_4Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal I/O 4

                #region Sanity Check Signal I/O 5

                case ConsoleKey.F:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO5Signal]}\n\r");
                    PulseIO_5Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal I/O 5

                #region Sanity Check Signal I/O 6

                case ConsoleKey.G:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO6Signal]}\n\r");
                    PulseIO_6Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal I/O 6

                #region Sanity Check Signal I/O 7

                case ConsoleKey.H:
                    Console.WriteLine($"\n[INFO] You have selected {SanityCheckSignalName[(int)SanityCheckSignal.PulseIO7Signal]}\n\r");
                    PulseIO_7Signal();
                    Console.ReadLine();
                    break;

                #endregion Sanity Check Signal I/O 7


                default:
                    Console.WriteLine($"[INFO]  {SanityCheckSignalName[(int)SanityCheckSignal.InvalidSignalSelected]} ");
                    Console.ReadKey();
                    break;
            }
        }

        #endregion Select Sanity Check Signal

        #region Pulse Signal Static Methods

        static void PulseA0Signal()
        {
#if !DEBUG
            var a0Pin = Pi.Gpio[BcmPin.Gpio26];
            a0Pin.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 26 (Clock) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                a0Pin.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 26 set HIGH (3.3V)");
                Thread.Sleep(2000);
#if !DEBUG
                a0Pin.Write(false);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 26 set LOW (0V)");
                Thread.Sleep(2000);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop
             */
#if !DEBUG
            a0Pin.Write(false);
#endif
            Console.WriteLine("[INFO] A0 (Clock) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");
        }

        static void PulseA1Signal()
        {
#if !DEBUG
            var a1Pin = Pi.Gpio[BcmPin.Gpio19];
            a1Pin.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 19 (SOS) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                a1Pin.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 19 set HIGH (3.3V)");
                Thread.Sleep(2000);
#if !DEBUG
                a1Pin.Write(false);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 19 set LOW (0V)");
                Thread.Sleep(2000);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop
             */
#if !DEBUG
            a1Pin.Write(false);
#endif
            Console.WriteLine("[INFO] A1 (SOS) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");
        }

        static void PulseA2Signal()
        {
#if !DEBUG
            var a2Pin = Pi.Gpio[BcmPin.Gpio13];
            a2Pin.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 13 (Service) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                a2Pin.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 13 set HIGH (3.3V)");
                Thread.Sleep(2000);
#if !DEBUG
                a2Pin.Write(false);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 13 set LOW (0V)");
                Thread.Sleep(2000);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop
             */
#if !DEBUG
            a2Pin.Write(false);
#endif
            Console.WriteLine("[INFO] A2 (Service) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");
        }

        static void PulseA3Signal()
        {
#if !DEBUG
            var a3Pin = Pi.Gpio[BcmPin.Gpio06];
            a3Pin.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 06 (Unlock B) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                a3Pin.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 06 set HIGH (3.3V)");
                Thread.Sleep(2000);
#if !DEBUG
                a3Pin.Write(false);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 06 set LOW (0V)");
                Thread.Sleep(2000);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop
             */
#if !DEBUG
            a3Pin.Write(false);
#endif
            Console.WriteLine("[INFO] A3 (Unlock B) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");
        }

        static void PulseA4Signal()
        {
#if !DEBUG
            var a4Pin = Pi.Gpio[BcmPin.Gpio05];
            a4Pin.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 05 (Unlock A) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                a4Pin.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 05 set HIGH (3.3V)");
                Thread.Sleep(2000);
#if !DEBUG
                a4Pin.Write(false);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 05 set LOW (0V)");
                Thread.Sleep(2000);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop
             */
#if !DEBUG
            a4Pin.Write(false);
#endif
            Console.WriteLine("[INFO] A4 (Unlock A) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");
        }

        static void PulseA5Signal()
        {
#if !DEBUG
            var a5Pin = Pi.Gpio[BcmPin.Gpio22];
            a5Pin.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 22 (State Bit 1) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                a5Pin.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 22 set HIGH (3.3V)");
                Thread.Sleep(2000);
#if !DEBUG
                a5Pin.Write(false);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 22 set LOW (0V)");
                Thread.Sleep(2000);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop
             */
#if !DEBUG
            a5Pin.Write(false);
#endif
            Console.WriteLine("[INFO] A5 (State Bit 1) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");
        }

        static void PulseA6Signal()
        {
#if !DEBUG
            var a6Pin = Pi.Gpio[BcmPin.Gpio27];
            a6Pin.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 27 (State Bit 2) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                a6Pin.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 27 set HIGH (3.3V)");
                Thread.Sleep(2000);
#if !DEBUG
                a6Pin.Write(false);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 27 set LOW (0V)");
                Thread.Sleep(2000); 

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop
             */
#if !DEBUG
            a6Pin.Write(false);
#endif
            Console.WriteLine("[INFO] A6 (State Bit 2) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");
        }

        static void PulseA7Signal()
        {
#if !DEBUG
            var a7Pin = Pi.Gpio[BcmPin.Gpio17];
            a7Pin.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 17 (State Bit 3) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                a7Pin.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 17 set HIGH (3.3V)");
                Thread.Sleep(2000);
#if !DEBUG
                a7Pin.Write(false);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 17 set LOW (0V)");
                Thread.Sleep(2000); 

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop
             */
#if !DEBUG
            a7Pin.Write(false);
#endif
            Console.WriteLine("[INFO] A7 (State Bit 3) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");
        }

        static void PulseA8Signal()
        {
#if !DEBUG
            var a8Pin = Pi.Gpio[BcmPin.Gpio04];
            a8Pin.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 04 (State Bit 4) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                a8Pin.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 04 set HIGH (3.3V)");
                Thread.Sleep(2000); 
#if !DEBUG
                a8Pin.Write(false); 
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 04 set LOW (0V)");
                Thread.Sleep(2000); 

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop
             */
#if !DEBUG
            a8Pin.Write(false);
#endif
            Console.WriteLine("[INFO] A8 (State Bit 4) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");
        }

        static void PulseIO_0Signal()
        {

#if !DEBUG
            var pinIO_0 = Pi.Gpio[BcmPin.Gpio18];
            pinIO_0.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 18 (I/O 0) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                pinIO_0.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 18 set HIGH (3.3V)");
                Thread.Sleep(2000);
#if !DEBUG
                pinIO_0.Write(false); 
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 18 set LOW (0V)");
                Thread.Sleep(2000); 

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop
             */
#if !DEBUG
            pinIO_0.Write(false);
            pinIO_0.PinMode = GpioPinDriveMode.Input;
#endif
            Console.WriteLine("[INFO] GPIO 18 (I/O 0) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");
        }

        static void PulseIO_1Signal()
        {

#if !DEBUG
            var pinIO_1 = Pi.Gpio[BcmPin.Gpio23];
            pinIO_1.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 23 (I/O 1) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                pinIO_1.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 23 set HIGH (3.3V)");
                Thread.Sleep(2000); // Hold HIGH for 2 seconds
#if !DEBUG
                pinIO_1.Write(false); // LOW
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 23 set LOW (0V)");
                Thread.Sleep(2000); // Hold LOW for 2 seconds

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop. Set it to input just in case.
             */
#if !DEBUG
            pinIO_1.Write(false);
#endif
            Console.WriteLine("[INFO] GPIO 23 (I/O 1) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");

        }

        static void PulseIO_2Signal()
        {

#if !DEBUG
            var pinIO_2 = Pi.Gpio[BcmPin.Gpio24];
            pinIO_2.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 24 (I/O 2) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                pinIO_2.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 24 set HIGH (3.3V)");
                Thread.Sleep(2000); // Hold HIGH for 2 seconds
#if !DEBUG
                pinIO_2.Write(false); // LOW
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 24 set LOW (0V)");
                Thread.Sleep(2000); // Hold LOW for 2 seconds

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop. Set it to input just in case.
             */
#if !DEBUG
            pinIO_2.Write(false);
            pinIO_2.PinMode = GpioPinDriveMode.Input;
#endif
            Console.WriteLine("[INFO] GPIO 24 (I/O 2) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");

        }

        static void PulseIO_3Signal()
        {

#if !DEBUG
            var pinIO_3 = Pi.Gpio[BcmPin.Gpio25];
            pinIO_3.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 25 (I/O 3) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                pinIO_3.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 25 set HIGH (3.3V)");
                Thread.Sleep(2000); // Hold HIGH for 2 seconds
#if !DEBUG
                pinIO_3.Write(false); // LOW
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 25 set LOW (0V)");
                Thread.Sleep(2000); // Hold LOW for 2 seconds

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop. Set it to input just in case.
             */
#if !DEBUG
            pinIO_3.Write(false);
            pinIO_3.PinMode = GpioPinDriveMode.Input;
#endif
            Console.WriteLine("[INFO] GPIO 25 (I/O 3) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");

        }

        static void PulseIO_4Signal()
        {

#if !DEBUG
            var pinIO_4 = Pi.Gpio[BcmPin.Gpio12];
            pinIO_4.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 12 (I/O 4) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                pinIO_4.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 12 set HIGH (3.3V)");
                Thread.Sleep(2000); // Hold HIGH for 2 seconds
#if !DEBUG
                pinIO_4.Write(false); // LOW
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 12 set LOW (0V)");
                Thread.Sleep(2000); // Hold LOW for 2 seconds

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop. Set it to input just in case.
             */
#if !DEBUG
            pinIO_4.Write(false);
            pinIO_4.PinMode = GpioPinDriveMode.Input;
#endif
            Console.WriteLine("[INFO] GPIO 12 (I/O 4) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");

        }

        static void PulseIO_5Signal()
        {

#if !DEBUG
            var pinIO_5 = Pi.Gpio[BcmPin.Gpio16];
            pinIO_5.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 16 (I/O 5) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                pinIO_5.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 16 set HIGH (3.3V)");
                Thread.Sleep(2000); // Hold HIGH for 2 seconds
#if !DEBUG
                pinIO_5.Write(false); // LOW
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 16 set LOW (0V)");
                Thread.Sleep(2000); // Hold LOW for 2 seconds

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop. Set it to input just in case.
             */
#if !DEBUG
            pinIO_5.Write(false);
            pinIO_5.PinMode = GpioPinDriveMode.Input;
#endif
            Console.WriteLine("[INFO] GPIO 16 (I/O 5) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");

        }

        static void PulseIO_6Signal()
        {

#if !DEBUG
            var pinIO_6 = Pi.Gpio[BcmPin.Gpio20];
            pinIO_6.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 20 (I/O 6) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                pinIO_6.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 20 set HIGH (3.3V)");
                Thread.Sleep(2000); // Hold HIGH for 2 seconds
#if !DEBUG
                pinIO_6.Write(false); // LOW
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 20 set LOW (0V)");
                Thread.Sleep(2000); // Hold LOW for 2 seconds

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop. Set it to input just in case.
             */
#if !DEBUG
            pinIO_6.Write(false);
            pinIO_6.PinMode = GpioPinDriveMode.Input;
#endif
            Console.WriteLine("[INFO] GPIO 20 (I/O 6) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");
        }

        static void PulseIO_7Signal()
        {

#if !DEBUG
            var pinIO_7 = Pi.Gpio[BcmPin.Gpio21];
            pinIO_7.PinMode = GpioPinDriveMode.Output;
#endif
            Console.WriteLine("[INFO] Starting GPIO 21 (I/O 7) square wave test. Press Esc to stop.");

            bool escKey = false;

            while (!escKey)
            {
#if !DEBUG
                pinIO_7.Write(true);
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 21 set HIGH (3.3V)");
                Thread.Sleep(2000); // Hold HIGH for 2 seconds
#if !DEBUG
                pinIO_7.Write(false); // LOW
#endif
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] GPIO 21 set LOW (0V)");
                Thread.Sleep(2000); // Hold LOW for 2 seconds

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        escKey = true;
                    }
                }
            }

            /*
             * Ensure pin is safely set LOW when exiting loop. Set it to input just in case.
             */
#if !DEBUG
            pinIO_7.Write(false);
            pinIO_7.PinMode = GpioPinDriveMode.Input;
#endif
            Console.WriteLine("[INFO] GPIO 21 (I/O 7) Signal Pulse Ended. Pin reset to LOW. Press Enter to return to Main Menu. ");

        }

        #endregion Pulse Signal Static Methods

        #endregion Infrastructure Sanity Check

        #region Display Dictionary

        static Dictionary<int, string> ConsoleDisplayMessages = new Dictionary<int, string>
        {
            [(int)ConsoleDisplay.Title] = "EEPROM AT28C64B",
            [(int)ConsoleDisplay.InfoMessage] = "Industrial Airlock Offline Verifier",
            [(int)ConsoleDisplay.SelectTestSuit] = "Select Test Suit:",
            [(int)ConsoleDisplay.SelectSignalForSanityCheck] = "Select Signal for Sanity Check"
        };

        static Dictionary<int, string> OptionName = new Dictionary<int, string>
        {
            [(int)SuitOption.InfrastructurePhisicalSanityCheck] = "Infrastructure Sanity Check (Pin I/O Verification)",
            [(int)SuitOption.OfflineVerification] = "Offline Verification",
            [(int)SuitOption.InvalidOption] = "\n\rYou have selected Invalid Option. \n\rPress any key to return to Main Menu ! "
        };

        static Dictionary<int, string> SanityCheckSignalName = new Dictionary<int, string>
        {
            [(int)SanityCheckSignal.PulseA0Signal] = "Pulse A0 Signal",
            [(int)SanityCheckSignal.PulseA1Signal] = "Pulse A1 Signal",
            [(int)SanityCheckSignal.PulseA2Signal] = "Pulse A2 Signal",
            [(int)SanityCheckSignal.PulseA3Signal] = "Pulse A3 Signal",
            [(int)SanityCheckSignal.PulseA4Signal] = "Pulse A4 Signal",
            [(int)SanityCheckSignal.PulseA5Signal] = "Pulse A5 Signal",
            [(int)SanityCheckSignal.PulseA6Signal] = "Pulse A6 Signal",
            [(int)SanityCheckSignal.PulseA7Signal] = "Pulse A7 Signal",
            [(int)SanityCheckSignal.PulseA8Signal] = "Pulse A8 Signal",

            [(int)SanityCheckSignal.PulseIO0Signal] = "Pulse I/O 0 Signal",
            [(int)SanityCheckSignal.PulseIO1Signal] = "Pulse I/O 1 Signal",
            [(int)SanityCheckSignal.PulseIO2Signal] = "Pulse I/O 2 Signal",
            [(int)SanityCheckSignal.PulseIO3Signal] = "Pulse I/O 3 Signal",
            [(int)SanityCheckSignal.PulseIO4Signal] = "Pulse I/O 4 Signal",
            [(int)SanityCheckSignal.PulseIO5Signal] = "Pulse I/O 5 Signal",
            [(int)SanityCheckSignal.PulseIO6Signal] = "Pulse I/O 6 Signal",
            [(int)SanityCheckSignal.PulseIO7Signal] = "Pulse I/O 7 Signal",


            [(int)SanityCheckSignal.InvalidSignalSelected] = "\n\rInvalid Signal Selected.  \n\rPress any key to return to Main Menu !"
        };

        #endregion Display Dictionary
    }

    #endregion Program

    #region Enums

    public enum ConsoleDisplay
    {
        Title = 1,
        InfoMessage = 2,
        SelectTestSuit = 3,
        SelectSignalForSanityCheck = 4
    }

    public enum SuitOption
    {
        InfrastructurePhisicalSanityCheck = 1,
        OfflineVerification = 2,
        InvalidOption = 3
    }

    public enum SanityCheckSignal
    {
        PulseA0Signal = 1,
        PulseA1Signal = 2,
        PulseA2Signal = 3,
        PulseA3Signal = 4,
        PulseA4Signal = 5,
        PulseA5Signal = 6,
        PulseA6Signal = 7,
        PulseA7Signal = 8,
        PulseA8Signal = 9,

        PulseIO0Signal = 'A',
        PulseIO1Signal = 'B',
        PulseIO2Signal = 'C',
        PulseIO3Signal = 'D',
        PulseIO4Signal = 'E',
        PulseIO5Signal = 'F',
        PulseIO6Signal = 'G',
        PulseIO7Signal = 'H',

        InvalidSignalSelected = 18
    }

    #endregion Enums

    #region Exhaustive Truth Table Transition Sweeps

    public class OfflineExhaustiveTruthTableTestRunner
    {
        private static readonly int _clockDelayMs;

        static OfflineExhaustiveTruthTableTestRunner()
        {
            _clockDelayMs = 20;
        }

        public static bool ExecuteStep(byte inputSignals, bool triggerClock, byte expectedOutputByte, string stepName)
        {
            HardwareController.SetInputs(inputSignals, triggerClock);
            Thread.Sleep(5);

            byte actualOutput = HardwareController.ReadOutputs();

            return ConsoleLogger.AssertState(stepName, expectedOutputByte, actualOutput);
        }

        public static void Run()
        {
            #region State Ready

            Console.WriteLine("State READY Begin.");
            Console.WriteLine();

            ExecuteStep(0x00, false, 0xC0, "[1]  READY READY                ");
            ExecuteStep(0x00, true, 0xC0, "[2]  READY READY                ");
            ExecuteStep(0x01, false, 0xC0, "[3]  READY READY                ");
            ExecuteStep(0x01, true, 0x0B, "[4]  READY EMERGENCY (SOS)      ");
            ExecuteStep(0x02, false, 0xC0, "[5]  READY READY                ");
            ExecuteStep(0x02, true, 0x0A, "[6]  READY SERVICE (MAINTENANCE)");
            ExecuteStep(0x03, false, 0xC0, "[7]  READY READY                ");
            ExecuteStep(0x03, true, 0x0B, "[8]  READY EMERGENCY (SOS)      ");
            ExecuteStep(0x04, false, 0xC0, "[9]  READY READY                ");
            ExecuteStep(0x04, true, 0xB6, "[10] READY PREPARE FOR EXIT     ");
            ExecuteStep(0x05, false, 0xC0, "[11] READY READY                ");
            ExecuteStep(0x05, true, 0x0B, "[12] READY EMERGENCY (SOS)      ");
            ExecuteStep(0x06, false, 0xC0, "[13] READY READY                ");
            ExecuteStep(0x06, true, 0x0A, "[14] READY SERVICE (MAINTENANCE)");
            ExecuteStep(0x07, false, 0xC0, "[15] READY READY                ");
            ExecuteStep(0x07, true, 0x0B, "[16] READY EMERGENCY (SOS)      ");
            ExecuteStep(0x08, false, 0xC0, "[17] READY READY                ");
            ExecuteStep(0x08, true, 0xB1, "[18] READY READY                ");
            ExecuteStep(0x09, false, 0xC0, "[19] READY PREPARE FOR ENTER    ");
            ExecuteStep(0x09, true, 0x0B, "[20] READY EMERGENCY (SOS)      ");
            ExecuteStep(0x0A, false, 0xC0, "[21] READY READY                ");
            ExecuteStep(0x0A, true, 0x0A, "[22] READY SERVICE (MAINTENANCE)");
            ExecuteStep(0x0B, false, 0xC0, "[23] READY READY                ");
            ExecuteStep(0x0B, true, 0x0B, "[24] READY EMERGENCY (SOS)      ");
            ExecuteStep(0x0C, false, 0xC0, "[25] READY READY                ");
            ExecuteStep(0x0C, true, 0xC0, "[26] READY READY                ");
            ExecuteStep(0x0D, false, 0xC0, "[27] READY READY                ");
            ExecuteStep(0x0D, true, 0x0B, "[28] READY EMERGENCY (SOS)      ");
            ExecuteStep(0x0E, false, 0xC0, "[29] READY READY                ");
            ExecuteStep(0x0E, true, 0x0A, "[30] READY SERVICE (MAINTENANCE)");
            ExecuteStep(0x0F, false, 0xC0, "[31] READY READY                ");
            ExecuteStep(0x0F, true, 0x0B, "[32] READY EMERGENCY (SOS)      ");

            Console.WriteLine();
            Console.WriteLine("State READY End.");
            Console.WriteLine();

            #endregion State Ready

            Thread.Sleep(500);

            #region State Prepare for Enter

            Console.WriteLine("State PREPARE FOR ENTER Begin.");
            Console.WriteLine();

            ExecuteStep(0x10, false, 0xD1, "[1]  PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x10, true, 0x42, "[2]  PREP_ENTER UNLOCK DOOR A           ");
            ExecuteStep(0x11, false, 0xD1, "[3]  PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x11, true, 0x0B, "[4]  PREP_ENTER EMERGENCY (SOS)         ");
            ExecuteStep(0x12, false, 0xD1, "[5]  PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x12, true, 0x0A, "[6]  PREP_ENTER SERVICE (MAINTENANCE)   ");
            ExecuteStep(0x13, false, 0xD1, "[7]  PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x13, true, 0x0B, "[8]  PREP_ENTER EMERGENCY (SOS)         ");
            ExecuteStep(0x14, false, 0xD1, "[9]  PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x14, true, 0x42, "[10] PREP_ENTER UNLOCK DOOR A           ");
            ExecuteStep(0x15, false, 0xD1, "[11] PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x15, true, 0x0B, "[12] PREP_ENTER EMERGENCY (SOS)         ");
            ExecuteStep(0x16, false, 0xD1, "[13] PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x16, true, 0x42, "[14] PREP_ENTER SERVICE (MAINTENANCE)   ");
            ExecuteStep(0x17, false, 0xD1, "[15] PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x17, true, 0x0B, "[16] PREP_ENTER EMERGENCY (SOS)         ");
            ExecuteStep(0x18, false, 0xD1, "[17] PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x18, true, 0x42, "[18] PREP_ENTER UNLOCK DOOR A           ");
            ExecuteStep(0x19, false, 0xD1, "[19] PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x19, true, 0x0B, "[20] PREP_ENTER EMERGENCY (SOS)         ");
            ExecuteStep(0x1A, false, 0xD1, "[21] PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x1A, true, 0x0A, "[22] PREP_ENTER SERVICE (MAINTENANCE)   ");
            ExecuteStep(0x1B, false, 0xD1, "[23] PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x1B, true, 0x0B, "[24] PREP_ENTER EMERGENCY (SOS)         ");
            ExecuteStep(0x1C, false, 0xD1, "[25] PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x1C, true, 0x42, "[26] PREP_ENTER UNLOCK DOOR A           ");
            ExecuteStep(0x1D, false, 0xD1, "[27] PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x1D, true, 0x0B, "[28] PREP_ENTER EMERGENCY (SOS)         ");
            ExecuteStep(0x1E, false, 0xD1, "[29] PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x1E, true, 0x0A, "[30] PREP_ENTER SERVICE (MAINTENANCE)   ");
            ExecuteStep(0x1F, false, 0xD1, "[31] PREP_ENTER PREP_ENTER              ");
            ExecuteStep(0x1F, true, 0x0B, "[32] PREP_ENTER EMERGENCY (SOS)         ");


            Console.WriteLine();
            Console.WriteLine("State PREPARE FOR ENTER End.");
            Console.WriteLine();

            #endregion State Prepare for Enter

            Thread.Sleep(500);

            #region State Unlock Door A

            Console.WriteLine("State UNLOCK A Begin.");
            Console.WriteLine();

            ExecuteStep(0x20, false, 0x42, "[1]  UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x20, true, 0xE3, "[2]  UNLOCK A DECONTAMINATION SPRAY ON ENTER");
            ExecuteStep(0x21, false, 0x42, "[3]  UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x21, true, 0x0B, "[4]  UNLOCK A EMERGENCY (SOS)               ");
            ExecuteStep(0x22, false, 0x42, "[5]  UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x22, true, 0x0A, "[6]  UNLOCK A SERVICE (MAINTENANCE)         ");
            ExecuteStep(0x23, false, 0x42, "[7]  UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x23, true, 0x0B, "[8]  UNLOCK A EMERGENCY (SOS)               ");
            ExecuteStep(0x24, false, 0x42, "[9]  UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x24, true, 0xE3, "[10] UNLOCK A DECONTAMINATION SPRAY ON ENTER");
            ExecuteStep(0x25, false, 0x42, "[11] UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x25, true, 0x0B, "[12] UNLOCK A EMERGENCY (SOS)               ");
            ExecuteStep(0x26, false, 0x42, "[13] UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x26, true, 0x0A, "[14] UNLOCK A SERVICE (MAINTENANCE)         ");
            ExecuteStep(0x27, false, 0x42, "[15] UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x27, true, 0x0B, "[16] UNLOCK A EMERGENCY (SOS)               ");
            ExecuteStep(0x28, false, 0x42, "[17] UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x28, true, 0xE3, "[18] UNLOCK A DECONTAMINATION SPRAY ON ENTER");
            ExecuteStep(0x29, false, 0x42, "[19] UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x29, true, 0x0B, "[20] UNLOCK A EMERGENCY (SOS)               ");
            ExecuteStep(0x2A, false, 0x42, "[21] UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x2A, true, 0x0A, "[22] UNLOCK A SERVICE (MAINTENANCE)         ");
            ExecuteStep(0x2B, false, 0x42, "[23] UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x2B, true, 0x0B, "[24] UNLOCK A EMERGENCY (SOS)               ");
            ExecuteStep(0x2C, false, 0x42, "[25] UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x2C, true, 0xE3, "[26] UNLOCK A DECONTAMINATION SPRAY ON ENTER");
            ExecuteStep(0x2D, false, 0x42, "[27] UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x2D, true, 0x0B, "[28] UNLOCK A EMERGENCY (SOS)               ");
            ExecuteStep(0x2E, false, 0x42, "[29] UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x2E, true, 0x0A, "[30] UNLOCK A SERVICE (MAINTENANCE)         ");
            ExecuteStep(0x2F, false, 0x42, "[31] UNLOCK A UNLOCK DOOR A                 ");
            ExecuteStep(0x2F, true, 0x0B, "[32] UNLOCK A EMERGENCY (SOS)               ");


            Console.WriteLine();
            Console.WriteLine("State UNLOCK A End.");
            Console.WriteLine();

            #endregion State Unlock Door A

            Thread.Sleep(500);

            #region State Decontamination Spray on Enter

            Console.WriteLine("State DECONTAMINATION SPRAY ON ENTER  Begin.");
            Console.WriteLine();

            ExecuteStep(0x30, false, 0xE3, "[1]  ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x30, true, 0xD4, "[2]  ENTER SPRAY DEPRESSURIZE                   ");
            ExecuteStep(0x31, false, 0xE3, "[3]  ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x31, true, 0x0B, "[4]  ENTER SPRAY EMERGENCY (SOS)                ");
            ExecuteStep(0x32, false, 0xE3, "[5]  ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x32, true, 0x0A, "[6]  ENTER SPRAY SERVICE (MAINTENANCE)          ");
            ExecuteStep(0x33, false, 0xE3, "[7]  ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x33, true, 0x0B, "[8]  ENTER SPRAY EMERGENCY (SOS)                ");
            ExecuteStep(0x34, false, 0xE3, "[9]  ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x34, true, 0xD4, "[10] ENTER SPRAY DEPRESSURIZE                   ");
            ExecuteStep(0x35, false, 0xE3, "[11] ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x35, true, 0x0B, "[12] ENTER SPRAY EMERGENCY (SOS)                ");
            ExecuteStep(0x36, false, 0xE3, "[13] ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x36, true, 0x0A, "[14] ENTER SPRAY SERVICE (MAINTENANCE)          ");
            ExecuteStep(0x37, false, 0xE3, "[15] ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x37, true, 0x0B, "[16] ENTER SPRAY EMERGENCY (SOS)                ");
            ExecuteStep(0x38, false, 0xE3, "[17] ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x38, true, 0xD4, "[18] ENTER SPRAY DEPRESSURIZE                   ");
            ExecuteStep(0x39, false, 0xE3, "[19] ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x39, true, 0x0B, "[20] ENTER SPRAY EMERGENCY (SOS)                ");
            ExecuteStep(0x3A, false, 0xE3, "[21] ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x3A, true, 0x0A, "[22] ENTER SPRAY SERVICE (MAINTENANCE)          ");
            ExecuteStep(0x3B, false, 0xE3, "[23] ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x3B, true, 0x0B, "[24] ENTER SPRAY EMERGENCY (SOS)                ");
            ExecuteStep(0x3C, false, 0xE3, "[25] ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x3C, true, 0xD4, "[26] ENTER SPRAY DEPRESSURIZE                   ");
            ExecuteStep(0x3D, false, 0xE3, "[27] ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x3D, true, 0x0B, "[28] ENTER SPRAY EMERGENCY (SOS)                ");
            ExecuteStep(0x3E, false, 0xE3, "[29] ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x3E, true, 0x0A, "[30] ENTER SPRAY SERVICE (MAINTENANCE)          ");
            ExecuteStep(0x3F, false, 0xE3, "[31] ENTER SPRAY DECONTAMINATION SPRAY ON ENTER ");
            ExecuteStep(0x3F, true, 0x0B, "[32] ENTER SPRAY EMERGENCY (SOS)                ");


            Console.WriteLine();
            Console.WriteLine("State DECONTAMINATION SPRAY ON ENTER End.");
            Console.WriteLine();

            #endregion State Decontamination Spray on Enter

            Thread.Sleep(500);

            #region State Depressurize 

            Console.WriteLine("State DEPRESSURIZE Begin.");
            Console.WriteLine();

            ExecuteStep(0x40, false, 0xD4, "[1]  DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x40, true, 0x85, "[2]  DEPRESSURIZE ENTER                 ");
            ExecuteStep(0x41, false, 0xD4, "[3]  DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x41, true, 0x0B, "[4]  DEPRESSURIZE EMERGENCY (SOS)       ");
            ExecuteStep(0x42, false, 0xD4, "[5]  DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x42, true, 0x0A, "[6]  DEPRESSURIZE SERVICE (MAINTENANCE) ");
            ExecuteStep(0x43, false, 0xD4, "[7]  DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x43, true, 0x0B, "[8]  DEPRESSURIZE EMERGENCY (SOS)       ");
            ExecuteStep(0x44, false, 0xD4, "[9]  DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x44, true, 0x85, "[10] DEPRESSURIZE ENTER                 ");
            ExecuteStep(0x45, false, 0xD4, "[11] DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x45, true, 0x0B, "[12] DEPRESSURIZE EMERGENCY (SOS)       ");
            ExecuteStep(0x46, false, 0xD4, "[13] DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x46, true, 0x0A, "[14] DEPRESSURIZE SERVICE (MAINTENANCE) ");
            ExecuteStep(0x47, false, 0xD4, "[15] DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x47, true, 0x0B, "[16] DEPRESSURIZE EMERGENCY (SOS)       ");
            ExecuteStep(0x48, false, 0xD4, "[17] DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x48, true, 0x85, "[18] DEPRESSURIZE ENTER                 ");
            ExecuteStep(0x49, false, 0xD4, "[19] DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x49, true, 0x0B, "[20] DEPRESSURIZE EMERGENCY (SOS)       ");
            ExecuteStep(0x4A, false, 0xD4, "[21] DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x4A, true, 0x0A, "[22] DEPRESSURIZE SERVICE (MAINTENANCE) ");
            ExecuteStep(0x4B, false, 0xD4, "[23] DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x4B, true, 0x0B, "[24] DEPRESSURIZE EMERGENCY (SOS)       ");
            ExecuteStep(0x4C, false, 0xD4, "[25] DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x4C, true, 0x85, "[26] DEPRESSURIZE ENTER                 ");
            ExecuteStep(0x4D, false, 0xD4, "[27] DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x4D, true, 0x0B, "[28] DEPRESSURIZE EMERGENCY (SOS)       ");
            ExecuteStep(0x4E, false, 0xD4, "[29] DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x4E, true, 0x0A, "[30] DEPRESSURIZE SERVICE (MAINTENANCE) ");
            ExecuteStep(0x4F, false, 0xD4, "[31] DEPRESSURIZE DEPRESSURIZE          ");
            ExecuteStep(0x4F, true, 0x0B, "[32] DEPRESSURIZE EMERGENCY (SOS)       ");


            Console.WriteLine();
            Console.WriteLine("State DEPRESSURIZE End.");
            Console.WriteLine();

            #endregion State Depressurize 

            Thread.Sleep(500);

            #region State Enter

            Console.WriteLine("State ENTER  Begin.");
            Console.WriteLine();

            ExecuteStep(0x50, false, 0x85, "[1]  ENTER ENTER                ");
            ExecuteStep(0x50, true, 0xC0, "[2]  ENTER READY                ");
            ExecuteStep(0x51, false, 0x85, "[3]  ENTER ENTER                ");
            ExecuteStep(0x51, true, 0x0B, "[4]  ENTER EMERGENCY (SOS)      ");
            ExecuteStep(0x52, false, 0x85, "[5]  ENTER ENTER                ");
            ExecuteStep(0x52, true, 0x0A, "[6]  ENTER SERVICE (MAINTENANCE)");
            ExecuteStep(0x53, false, 0x85, "[7]  ENTER ENTER                ");
            ExecuteStep(0x53, true, 0x0B, "[8]  ENTER EMERGENCY (SOS)      ");
            ExecuteStep(0x54, false, 0x85, "[9]  ENTER ENTER                ");
            ExecuteStep(0x54, true, 0xC0, "[10] ENTER READY                ");
            ExecuteStep(0x55, false, 0x85, "[11] ENTER ENTER                ");
            ExecuteStep(0x55, true, 0x0B, "[12] ENTER EMERGENCY (SOS)      ");
            ExecuteStep(0x56, false, 0x85, "[13] ENTER ENTER                ");
            ExecuteStep(0x56, true, 0x0A, "[14] ENTER SERVICE (MAINTENANCE)");
            ExecuteStep(0x57, false, 0x85, "[15] ENTER ENTER                ");
            ExecuteStep(0x57, true, 0x0B, "[16] ENTER EMERGENCY (SOS)      ");
            ExecuteStep(0x58, false, 0x85, "[17] ENTER ENTER                ");
            ExecuteStep(0x58, true, 0xC0, "[18] ENTER READY                ");
            ExecuteStep(0x59, false, 0x85, "[19] ENTER ENTER                ");
            ExecuteStep(0x59, true, 0x0B, "[20] ENTER EMERGENCY (SOS)      ");
            ExecuteStep(0x5A, false, 0x85, "[21] ENTER ENTER                ");
            ExecuteStep(0x5A, true, 0x0A, "[22] ENTER SERVICE (MAINTENANCE)");
            ExecuteStep(0x5B, false, 0x85, "[23] ENTER ENTER                ");
            ExecuteStep(0x5B, true, 0x0B, "[24] ENTER EMERGENCY (SOS)      ");
            ExecuteStep(0x5C, false, 0x85, "[25] ENTER ENTER                ");
            ExecuteStep(0x5C, true, 0xC0, "[26] ENTER READY                ");
            ExecuteStep(0x5D, false, 0x85, "[27] ENTER ENTER                ");
            ExecuteStep(0x5D, true, 0x0B, "[28] ENTER EMERGENCY (SOS)      ");
            ExecuteStep(0x5E, false, 0x85, "[29] ENTER ENTER                ");
            ExecuteStep(0x5E, true, 0x0A, "[30] ENTER SERVICE (MAINTENANCE)");
            ExecuteStep(0x5F, false, 0x85, "[31] ENTER ENTER                ");
            ExecuteStep(0x5F, true, 0x0B, "[32] ENTER EMERGENCY (SOS)      ");


            Console.WriteLine();
            Console.WriteLine("State ENTER End.");
            Console.WriteLine();

            #endregion State Enter

            Thread.Sleep(500);

            #region State Service (Maintenance)

            Console.WriteLine("State SERVICE/MAINTENANCE Begin.");
            Console.WriteLine();

            ExecuteStep(0xA0, false, 0x0A, "[1]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xA0, true, 0x0A, "[2]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xA1, false, 0x0A, "[3]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xA1, true, 0x0B, "[4]  SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xA2, false, 0x0A, "[5]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xA2, true, 0xC0, "[6]  SERVICE READY                  ");
            ExecuteStep(0xA3, false, 0x0A, "[7]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xA3, true, 0x0B, "[8]  SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xA4, false, 0x0A, "[9]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xA4, true, 0x0A, "[10] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xA5, false, 0x0A, "[11] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xA5, true, 0x0B, "[12] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xA6, false, 0x0A, "[13] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xA6, true, 0xC0, "[14] SERVICE READY                  ");
            ExecuteStep(0xA7, false, 0x0A, "[15] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xA7, true, 0x0B, "[16] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xA8, false, 0x0A, "[17] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xA8, true, 0x0A, "[18] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xA9, false, 0x0A, "[19] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xA9, true, 0x0B, "[20] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xAA, false, 0x0A, "[21] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xAA, true, 0xC0, "[22] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xAB, false, 0x0A, "[23] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xAB, true, 0x0B, "[24] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xAC, false, 0x0A, "[25] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xAC, true, 0x0A, "[26] SERVICE READY                  ");
            ExecuteStep(0xAD, false, 0x0A, "[27] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xAD, true, 0x0B, "[28] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xAE, false, 0x0A, "[29] SERVICE READY                  ");
            ExecuteStep(0xAE, true, 0xC0, "[30] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xAF, false, 0x0A, "[31] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xAF, true, 0x0B, "[32] SERVICE EMERGENCY (SOS)        ");


            Console.WriteLine();
            Console.WriteLine("State SERVICE/MAINTENANCE End.");
            Console.WriteLine();

            #endregion State Service (Maintenance)

            Thread.Sleep(500);

            #region State Prepare for Exit 

            Console.WriteLine("State PREPARE FOR EXIT Begin.");
            Console.WriteLine();

            ExecuteStep(0x60, false, 0xD6, "[1]  PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x60, true, 0x87, "[2]  PREP_EXIT UNLOCK DOOR B        ");
            ExecuteStep(0x61, false, 0xD6, "[3]  PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x61, true, 0x0B, "[4]  PREP_EXIT EMERGENCY (SOS)      ");
            ExecuteStep(0x62, false, 0xD6, "[5]  PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x62, true, 0x0A, "[6]  PREP_EXIT SERVICE (MAINTENANCE)");
            ExecuteStep(0x63, false, 0xD6, "[7]  PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x63, true, 0x0B, "[8]  PREP_EXIT EMERGENCY (SOS)      ");
            ExecuteStep(0x64, false, 0xD6, "[9]  PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x64, true, 0x87, "[10] PREP_EXIT UNLOCK DOOR B        ");
            ExecuteStep(0x65, false, 0xD6, "[11] PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x65, true, 0x0B, "[12] PREP_EXIT EMERGENCY (SOS)      ");
            ExecuteStep(0x66, false, 0xD6, "[13] PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x66, true, 0x0A, "[14] PREP_EXIT SERVICE (MAINTENANCE)");
            ExecuteStep(0x67, false, 0xD6, "[15] PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x67, true, 0x0B, "[16] PREP_EXIT EMERGENCY (SOS)      ");
            ExecuteStep(0x68, false, 0xD6, "[17] PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x68, true, 0x87, "[18] PREP_EXIT UNLOCK DOOR B        ");
            ExecuteStep(0x69, false, 0xD6, "[19] PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x69, true, 0x0B, "[20] PREP_EXIT EMERGENCY (SOS)      ");
            ExecuteStep(0x6A, false, 0xD6, "[21] PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x6A, true, 0x0A, "[22] PREP_EXIT SERVICE (MAINTENANCE)");
            ExecuteStep(0x6B, false, 0xD6, "[23] PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x6B, true, 0x0B, "[24] PREP_EXIT EMERGENCY (SOS)      ");
            ExecuteStep(0x6C, false, 0xD6, "[25] PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x6C, true, 0x87, "[26] PREP_EXIT UNLOCK DOOR B        ");
            ExecuteStep(0x6D, false, 0xD6, "[27] PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x6D, true, 0x0B, "[28] PREP_EXIT EMERGENCY (SOS)      ");
            ExecuteStep(0x6E, false, 0xD6, "[29] PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x6E, true, 0x0A, "[30] PREP_EXIT SERVICE (MAINTENANCE)");
            ExecuteStep(0x6F, false, 0xD6, "[31] PREP_EXIT PREP_EXIT            ");
            ExecuteStep(0x6F, true, 0x0B, "[32] PREP_EXIT EMERGENCY (SOS)      ");


            Console.WriteLine();
            Console.WriteLine("State PREPARE FOR EXIT End.");
            Console.WriteLine();

            #endregion State Prepare for Exit 

            Thread.Sleep(500);

            #region State Unlock Door B

            Console.WriteLine("State UNLOCK B Begin.");
            Console.WriteLine();

            ExecuteStep(0x70, false, 0x87, "[1]  UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x70, true, 0xD8, "[2]  UNLOCK B PRESSURIZE            ");
            ExecuteStep(0x71, false, 0x87, "[3]  UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x71, true, 0x0B, "[4]  UNLOCK B EMERGENCY (SOS)       ");
            ExecuteStep(0x72, false, 0x87, "[5]  UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x72, true, 0x0A, "[6]  UNLOCK B SERVICE (MAINTENANCE) ");
            ExecuteStep(0x73, false, 0x87, "[7]  UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x73, true, 0x0B, "[8]  UNLOCK B EMERGENCY (SOS)       ");
            ExecuteStep(0x74, false, 0x87, "[9]  UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x74, true, 0xD8, "[10] UNLOCK B PRESSURIZE            ");
            ExecuteStep(0x75, false, 0x87, "[11] UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x75, true, 0x0B, "[12] UNLOCK B EMERGENCY (SOS)       ");
            ExecuteStep(0x76, false, 0x87, "[13] UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x76, true, 0x0A, "[14] UNLOCK B SERVICE (MAINTENANCE) ");
            ExecuteStep(0x77, false, 0x87, "[15] UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x77, true, 0x0B, "[16] UNLOCK B EMERGENCY (SOS)       ");
            ExecuteStep(0x78, false, 0x87, "[17] UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x78, true, 0xD8, "[18] UNLOCK B PRESSURIZE            ");
            ExecuteStep(0x79, false, 0x87, "[19] UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x79, true, 0x0B, "[20] UNLOCK B EMERGENCY (SOS)       ");
            ExecuteStep(0x7A, false, 0x87, "[21] UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x7A, true, 0x0A, "[22] UNLOCK B SERVICE (MAINTENANCE) ");
            ExecuteStep(0x7B, false, 0x87, "[23] UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x7B, true, 0x0B, "[24] UNLOCK B EMERGENCY (SOS)       ");
            ExecuteStep(0x7C, false, 0x87, "[25] UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x7C, true, 0xD8, "[26] UNLOCK B PRESSURIZE            ");
            ExecuteStep(0x7D, false, 0x87, "[27] UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x7D, true, 0x0B, "[28] UNLOCK B EMERGENCY (SOS)       ");
            ExecuteStep(0x7E, false, 0x87, "[29] UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x7E, true, 0x0A, "[30] UNLOCK B SERVICE (MAINTENANCE) ");
            ExecuteStep(0x7F, false, 0x87, "[31] UNLOCK B UNLOCK DOOR B         ");
            ExecuteStep(0x7F, true, 0x0B, "[32] UNLOCK B EMERGENCY (SOS)       ");


            Console.WriteLine();
            Console.WriteLine("State UNLOCK B End.");
            Console.WriteLine();

            #endregion State Unlock Door B

            Thread.Sleep(500);

            #region State Pressurize

            Console.WriteLine("State PRESSURIZE  Begin.");
            Console.WriteLine();

            ExecuteStep(0x80, false, 0xD8, "[1]  PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x80, true, 0x49, "[2]  PRESSURIZE EXIT                    ");
            ExecuteStep(0x81, false, 0xD8, "[3]  PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x81, true, 0x0B, "[4]  PRESSURIZE EMERGENCY (SOS)         ");
            ExecuteStep(0x82, false, 0xD8, "[5]  PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x82, true, 0x0A, "[6]  PRESSURIZE SERVICE (MAINTENANCE)   ");
            ExecuteStep(0x83, false, 0xD8, "[7]  PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x83, true, 0x0B, "[8]  PRESSURIZE EMERGENCY (SOS)         ");
            ExecuteStep(0x84, false, 0xD8, "[9]  PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x84, true, 0x49, "[10] PRESSURIZE EXIT                    ");
            ExecuteStep(0x85, false, 0xD8, "[11] PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x85, true, 0x0B, "[12] PRESSURIZE EMERGENCY (SOS)         ");
            ExecuteStep(0x86, false, 0xD8, "[13] PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x86, true, 0x0A, "[14] PRESSURIZE SERVICE (MAINTENANCE)   ");
            ExecuteStep(0x87, false, 0xD8, "[15] PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x87, true, 0x0B, "[16] PRESSURIZE EMERGENCY (SOS)         ");
            ExecuteStep(0x88, false, 0xD8, "[17] PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x88, true, 0x49, "[18] PRESSURIZE EXIT                    ");
            ExecuteStep(0x89, false, 0xD8, "[19] PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x89, true, 0x0B, "[20] PRESSURIZE EMERGENCY (SOS)         ");
            ExecuteStep(0x8A, false, 0xD8, "[21] PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x8A, true, 0x0A, "[22] PRESSURIZE SERVICE (MAINTENANCE)   ");
            ExecuteStep(0x8B, false, 0xD8, "[23] PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x8B, true, 0x0B, "[24] PRESSURIZE EMERGENCY (SOS)         ");
            ExecuteStep(0x8C, false, 0xD8, "[25] PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x8C, true, 0x49, "[26] PRESSURIZE EXIT                    ");
            ExecuteStep(0x8D, false, 0xD8, "[27] PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x8D, true, 0x0B, "[28] PRESSURIZE EMERGENCY (SOS)         ");
            ExecuteStep(0x8E, false, 0xD8, "[29] PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x8E, true, 0x0A, "[30] PRESSURIZE SERVICE (MAINTENANCE)   ");
            ExecuteStep(0x8F, false, 0xD8, "[31] PRESSURIZE PRESSURIZE              ");
            ExecuteStep(0x8F, true, 0x0B, "[32] PRESSURIZE EMERGENCY (SOS)         ");


            Console.WriteLine();
            Console.WriteLine("State PRESSURIZE End.");
            Console.WriteLine();

            #endregion State Pressurize

            Thread.Sleep(500);

            #region State Exit

            Console.WriteLine("State EXIT Begin.");
            Console.WriteLine();

            ExecuteStep(0x90, false, 0x49, "[1]  EXIT EXIT                  ");
            ExecuteStep(0x90, true, 0xC0, "[2]  EXIT READY                 ");
            ExecuteStep(0x91, false, 0x49, "[3]  EXIT EXIT                  ");
            ExecuteStep(0x91, true, 0x0B, "[4]  EXIT EMERGENCY (SOS)       ");
            ExecuteStep(0x92, false, 0x49, "[5]  EXIT EXIT                  ");
            ExecuteStep(0x92, true, 0x0A, "[6]  EXIT SERVICE (MAINTENANCE) ");
            ExecuteStep(0x93, false, 0x49, "[7]  EXIT EXIT                  ");
            ExecuteStep(0x93, true, 0x0B, "[8]  EXIT EMERGENCY (SOS)       ");
            ExecuteStep(0x94, false, 0x49, "[9]  EXIT EXIT                  ");
            ExecuteStep(0x94, true, 0xC0, "[10] EXIT READY                 ");
            ExecuteStep(0x95, false, 0x49, "[11] EXIT EXIT                  ");
            ExecuteStep(0x95, true, 0x0B, "[12] EXIT EMERGENCY (SOS)       ");
            ExecuteStep(0x96, false, 0x49, "[13] EXIT EXIT                  ");
            ExecuteStep(0x96, true, 0x0A, "[14] EXIT SERVICE (MAINTENANCE) ");
            ExecuteStep(0x97, false, 0x49, "[15] EXIT EXIT                  ");
            ExecuteStep(0x97, true, 0x0B, "[16] EXIT EMERGENCY (SOS)       ");
            ExecuteStep(0x98, false, 0x49, "[17] EXIT EXIT                  ");
            ExecuteStep(0x98, true, 0xC0, "[18] EXIT READY                 ");
            ExecuteStep(0x99, false, 0x49, "[19] EXIT EXIT                  ");
            ExecuteStep(0x99, true, 0x0B, "[20] EXIT EMERGENCY (SOS)       ");
            ExecuteStep(0x9A, false, 0x49, "[21] EXIT EXIT                  ");
            ExecuteStep(0x9A, true, 0x0A, "[22] EXIT SERVICE (MAINTENANCE) ");
            ExecuteStep(0x9B, false, 0x49, "[23] EXIT EXIT                  ");
            ExecuteStep(0x9B, true, 0x0B, "[24] EXIT EMERGENCY (SOS)       ");
            ExecuteStep(0x9C, false, 0x49, "[25] EXIT EXIT                  ");
            ExecuteStep(0x9C, true, 0xC0, "[26] EXIT READY                 ");
            ExecuteStep(0x9D, false, 0x49, "[27] EXIT EXIT                  ");
            ExecuteStep(0x9D, true, 0x0B, "[28] EXIT EMERGENCY (SOS)       ");
            ExecuteStep(0x9E, false, 0x49, "[29] EXIT EXIT                  ");
            ExecuteStep(0x9E, true, 0x0A, "[30] EXIT SERVICE (MAINTENANCE) ");
            ExecuteStep(0x9F, false, 0x49, "[31] EXIT EXIT                  ");
            ExecuteStep(0x9F, true, 0x0B, "[32] EXIT EMERGENCY (SOS)       ");


            Console.WriteLine();
            Console.WriteLine("State EXIT End.");
            Console.WriteLine();

            #endregion State Exit

            Thread.Sleep(500);

            #region State Emergency (SOS)

            Console.WriteLine("State EMERGENCY/SOS  Begin.");
            Console.WriteLine();

            ExecuteStep(0xB0, false, 0x0B, "[1]  EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB0, true, 0x0B, "[2]  EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB1, false, 0x0B, "[3]  EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB1, true, 0x0A, "[4]  EMERGENCY SERVICE (MAINTENANCE)");
            ExecuteStep(0xB2, false, 0x0B, "[5]  EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB2, true, 0x0B, "[6]  EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB3, false, 0x0B, "[7]  EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB3, true, 0x0A, "[8]  EMERGENCY SERVICE (MAINTENANCE)");
            ExecuteStep(0xB4, false, 0x0B, "[9]  EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB4, true, 0x0B, "[10] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB5, false, 0x0B, "[11] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB5, true, 0x0A, "[12] EMERGENCY SERVICE (MAINTENANCE)");
            ExecuteStep(0xB6, false, 0x0B, "[13] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB6, true, 0x0B, "[14] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB7, false, 0x0B, "[15] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB7, true, 0x0A, "[16] EMERGENCY SERVICE (MAINTENANCE)");
            ExecuteStep(0xB8, false, 0x0B, "[17] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB8, true, 0x0B, "[18] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB9, false, 0x0B, "[19] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xB9, true, 0x0A, "[20] EMERGENCY SERVICE (MAINTENANCE)");
            ExecuteStep(0xBA, false, 0x0B, "[21] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xBA, true, 0x0B, "[22] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xBB, false, 0x0B, "[23] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xBB, true, 0x0A, "[24] EMERGENCY SERVICE (MAINTENANCE)");
            ExecuteStep(0xBC, false, 0x0B, "[25] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xBC, true, 0x0B, "[26] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xBD, false, 0x0B, "[27] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xBD, true, 0x0A, "[28] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xBE, false, 0x0B, "[29] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xBE, true, 0x0B, "[30] EMERGENCY SERVICE (MAINTENANCE)");
            ExecuteStep(0xBF, false, 0x0B, "[31] EMERGENCY EMERGENCY (SOS)      ");
            ExecuteStep(0xBF, true, 0x0A, "[32] EMERGENCY EMERGENCY (SOS)      ");


            Console.WriteLine();
            Console.WriteLine("State EMERGENCY/SOS End.");
            Console.WriteLine();

            #endregion State Emergency (SOS)

            Thread.Sleep(500);

            #region Failover Service (Maintenance State)

            Console.WriteLine("State SERVICE/MAINTENANCE Begin. Failover address 1100.");
            Console.WriteLine();

            #region 1100

            ExecuteStep(0xC0, false, 0x0A, "[1]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xC0, true, 0x0A, "[2]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xC1, false, 0x0A, "[3]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xC1, true, 0x0B, "[4]  SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xC2, false, 0x0A, "[5]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xC2, true, 0xC0, "[6]  SERVICE READY                  ");
            ExecuteStep(0xC3, false, 0x0A, "[7]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xC3, true, 0x0B, "[8]  SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xC4, false, 0x0A, "[9]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xC4, true, 0x0A, "[10] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xC5, false, 0x0A, "[11] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xC5, true, 0x0B, "[12] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xC6, false, 0x0A, "[13] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xC6, true, 0xC0, "[14] SERVICE READY                  ");
            ExecuteStep(0xC7, false, 0x0A, "[15] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xC7, true, 0x0B, "[16] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xC8, false, 0x0A, "[17] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xC8, true, 0x0A, "[18] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xC9, false, 0x0A, "[19] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xC9, true, 0x0B, "[20] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xCA, false, 0x0A, "[21] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xCA, true, 0xC0, "[22] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xCB, false, 0x0A, "[23] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xCB, true, 0x0B, "[24] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xCC, false, 0x0A, "[25] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xCC, true, 0x0A, "[26] SERVICE READY                  ");
            ExecuteStep(0xCD, false, 0x0A, "[27] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xCD, true, 0x0B, "[28] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xCE, false, 0x0A, "[29] SERVICE READY                  ");
            ExecuteStep(0xCE, true, 0xC0, "[30] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xCF, false, 0x0A, "[31] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xCF, true, 0x0B, "[32] SERVICE EMERGENCY (SOS)        ");

            #endregion 1100

            Console.WriteLine();
            Console.WriteLine("State SERVICE/MAINTENANCE End. Failover address 1100.");
            Console.WriteLine();

            Thread.Sleep(500);

            Console.WriteLine("State SERVICE/MAINTENANCE Begin. Failover address 1101.");
            Console.WriteLine();

            #region 1101

            ExecuteStep(0xD0, false, 0x0A, "[1]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xD0, true, 0x0A, "[2]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xD1, false, 0x0A, "[3]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xD1, true, 0x0B, "[4]  SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xD2, false, 0x0A, "[5]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xD2, true, 0xC0, "[6]  SERVICE READY                  ");
            ExecuteStep(0xD3, false, 0x0A, "[7]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xD3, true, 0x0B, "[8]  SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xD4, false, 0x0A, "[9]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xD4, true, 0x0A, "[10] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xD5, false, 0x0A, "[11] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xD5, true, 0x0B, "[12] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xD6, false, 0x0A, "[13] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xD6, true, 0xC0, "[14] SERVICE READY                  ");
            ExecuteStep(0xD7, false, 0x0A, "[15] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xD7, true, 0x0B, "[16] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xD8, false, 0x0A, "[17] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xD8, true, 0x0A, "[18] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xD9, false, 0x0A, "[19] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xD9, true, 0x0B, "[20] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xDA, false, 0x0A, "[21] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xDA, true, 0xC0, "[22] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xDB, false, 0x0A, "[23] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xDB, true, 0x0B, "[24] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xDC, false, 0x0A, "[25] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xDC, true, 0x0A, "[26] SERVICE READY                  ");
            ExecuteStep(0xDD, false, 0x0A, "[27] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xDD, true, 0x0B, "[28] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xDE, false, 0x0A, "[29] SERVICE READY                  ");
            ExecuteStep(0xDE, true, 0xC0, "[30] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xDF, false, 0x0A, "[31] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xDF, true, 0x0B, "[32] SERVICE EMERGENCY (SOS)        ");

            #endregion 1101

            Console.WriteLine();
            Console.WriteLine("State SERVICE/MAINTENANCE End. Failover address 1101.");
            Console.WriteLine();

            Thread.Sleep(500);

            Console.WriteLine("State SERVICE/MAINTENANCE Begin. Failover address 1110.");
            Console.WriteLine();

            #region 1110

            ExecuteStep(0xE0, false, 0x0A, "[1]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xE0, true, 0x0A, "[2]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xE1, false, 0x0A, "[3]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xE1, true, 0x0B, "[4]  SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xE2, false, 0x0A, "[5]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xE2, true, 0xC0, "[6]  SERVICE READY                  ");
            ExecuteStep(0xE3, false, 0x0A, "[7]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xE3, true, 0x0B, "[8]  SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xE4, false, 0x0A, "[9]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xE4, true, 0x0A, "[10] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xE5, false, 0x0A, "[11] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xE5, true, 0x0B, "[12] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xE6, false, 0x0A, "[13] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xE6, true, 0xC0, "[14] SERVICE READY                  ");
            ExecuteStep(0xE7, false, 0x0A, "[15] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xE7, true, 0x0B, "[16] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xE8, false, 0x0A, "[17] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xE8, true, 0x0A, "[18] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xE9, false, 0x0A, "[19] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xE9, true, 0x0B, "[20] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xEA, false, 0x0A, "[21] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xEA, true, 0xC0, "[22] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xEB, false, 0x0A, "[23] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xEB, true, 0x0B, "[24] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xEC, false, 0x0A, "[25] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xEC, true, 0x0A, "[26] SERVICE READY                  ");
            ExecuteStep(0xED, false, 0x0A, "[27] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xED, true, 0x0B, "[28] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xEE, false, 0x0A, "[29] SERVICE READY                  ");
            ExecuteStep(0xEE, true, 0xC0, "[30] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xEF, false, 0x0A, "[31] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xEF, true, 0x0B, "[32] SERVICE EMERGENCY (SOS)        ");

            #endregion 1110

            Console.WriteLine();
            Console.WriteLine("State SERVICE/MAINTENANCE End. Failover address 1110.");
            Console.WriteLine();

            Thread.Sleep(500);

            Console.WriteLine("State SERVICE/MAINTENANCE Begin. Failover address 1111.");
            Console.WriteLine();

            #region 1111

            ExecuteStep(0xF0, false, 0x0A, "[1]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xF0, true, 0x0A, "[2]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xF1, false, 0x0A, "[3]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xF1, true, 0x0B, "[4]  SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xF2, false, 0x0A, "[5]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xF2, true, 0xC0, "[6]  SERVICE READY                  ");
            ExecuteStep(0xF3, false, 0x0A, "[7]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xF3, true, 0x0B, "[8]  SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xF4, false, 0x0A, "[9]  SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xF4, true, 0x0A, "[10] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xF5, false, 0x0A, "[11] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xF5, true, 0x0B, "[12] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xF6, false, 0x0A, "[13] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xF6, true, 0xC0, "[14] SERVICE READY                  ");
            ExecuteStep(0xF7, false, 0x0A, "[15] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xF7, true, 0x0B, "[16] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xF8, false, 0x0A, "[17] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xF8, true, 0x0A, "[18] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xF9, false, 0x0A, "[19] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xF9, true, 0x0B, "[20] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xFA, false, 0x0A, "[21] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xFA, true, 0xC0, "[22] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xFB, false, 0x0A, "[23] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xFB, true, 0x0B, "[24] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xFC, false, 0x0A, "[25] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xFC, true, 0x0A, "[26] SERVICE READY                  ");
            ExecuteStep(0xFD, false, 0x0A, "[27] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xFD, true, 0x0B, "[28] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xFE, false, 0x0A, "[29] SERVICE READY                  ");
            ExecuteStep(0xFE, true, 0xC0, "[30] SERVICE EMERGENCY (SOS)        ");
            ExecuteStep(0xFF, false, 0x0A, "[31] SERVICE SERVICE (MAINTENANCE)  ");
            ExecuteStep(0xFF, true, 0x0B, "[32] SERVICE EMERGENCY (SOS)        ");

            #endregion 1111

            Console.WriteLine();
            Console.WriteLine("State SERVICE/MAINTENANCE End. Failover address 1111.");
            Console.WriteLine();

            #endregion Failover Service (Maintenance State)
        }
    }

    public class HardwareController
    {
#if !DEBUG
        #region GPIO Inputs

        private static readonly IGpioPin pinIO_0; // State Feed Bit 1 (A5)
        private static readonly IGpioPin pinIO_1; // State Feed Bit 1 (A6)
        private static readonly IGpioPin pinIO_2; // State Feed Bit 1 (A7)
        private static readonly IGpioPin pinIO_3; // State Feed Bit 1 (A8)

        private static readonly IGpioPin pinIO_4; // Pressure Vent
        private static readonly IGpioPin pinIO_5; // Decontamination Spray
        private static readonly IGpioPin pinIO_6; // Unlock Door B
        private static readonly IGpioPin pinIO_7; // Unlock Door A

        #endregion GPIO Inputs

        #region GPIO Outputs

        private static readonly IGpioPin pinA_0; // Clock 
        private static readonly IGpioPin pinA_1; // Emergency(SOS)
        private static readonly IGpioPin pinA_2; // Service (Maintenance)
        private static readonly IGpioPin pinA_3; // Unlock Door B
        private static readonly IGpioPin pinA_4; // Unlock Door A
        private static readonly IGpioPin pinA_5; // State Bit 1
        private static readonly IGpioPin pinA_6; // State Bit 2
        private static readonly IGpioPin pinA_7; // State Bit 3
        private static readonly IGpioPin pinA_8; // State Bit 4

        #endregion GPIO Outputs

#endif

        static HardwareController()
        {
            #region Address Pins - GPIO Outputs

#if !DEBUG

            pinA_0 = Pi.Gpio[BcmPin.Gpio26];
            pinA_0.PinMode = GpioPinDriveMode.Output;

            pinA_1 = Pi.Gpio[BcmPin.Gpio19];
            pinA_1.PinMode = GpioPinDriveMode.Output;

            pinA_2 = Pi.Gpio[BcmPin.Gpio13];
            pinA_2.PinMode = GpioPinDriveMode.Output;

            pinA_3 = Pi.Gpio[BcmPin.Gpio06];
            pinA_3.PinMode = GpioPinDriveMode.Output;

            pinA_4 = Pi.Gpio[BcmPin.Gpio05];
            pinA_4.PinMode = GpioPinDriveMode.Output;

            pinA_5 = Pi.Gpio[BcmPin.Gpio22];
            pinA_5.PinMode = GpioPinDriveMode.Output;

            pinA_6 = Pi.Gpio[BcmPin.Gpio27];
            pinA_6.PinMode = GpioPinDriveMode.Output;

            pinA_7 = Pi.Gpio[BcmPin.Gpio17];
            pinA_7.PinMode = GpioPinDriveMode.Output;

            pinA_8 = Pi.Gpio[BcmPin.Gpio04];
            pinA_8.PinMode = GpioPinDriveMode.Output;
#endif

            #endregion Address Pins - GPIO Outputs

            #region I/O Pins - GPIO Inputs

#if !DEBUG

            pinIO_0 = Pi.Gpio[BcmPin.Gpio18];
            pinIO_0.PinMode = GpioPinDriveMode.Input;

            pinIO_1 = Pi.Gpio[BcmPin.Gpio23];
            pinIO_1.PinMode = GpioPinDriveMode.Input;

            pinIO_2 = Pi.Gpio[BcmPin.Gpio24];
            pinIO_2.PinMode = GpioPinDriveMode.Input;

            pinIO_3 = Pi.Gpio[BcmPin.Gpio25];
            pinIO_3.PinMode = GpioPinDriveMode.Input;

            pinIO_4 = Pi.Gpio[BcmPin.Gpio12];
            pinIO_4.PinMode = GpioPinDriveMode.Input;

            pinIO_5 = Pi.Gpio[BcmPin.Gpio16];
            pinIO_5.PinMode = GpioPinDriveMode.Input;

            pinIO_6 = Pi.Gpio[BcmPin.Gpio20];
            pinIO_6.PinMode = GpioPinDriveMode.Input;

            pinIO_7 = Pi.Gpio[BcmPin.Gpio21];
            pinIO_7.PinMode = GpioPinDriveMode.Input;

#endif

            #endregion I/O Pins - GPIO Inputs

        }

        public static void PulseClock(int _clockDelayMs)
        {
#if !DEBUG
            pinA_0.Write(true);
#endif
            Thread.Sleep(_clockDelayMs > 0 ? _clockDelayMs : 1);
#if !DEBUG
            pinA_0.Write(false);
#endif
        }

        public static void SetInputs(byte _inputMask)
        {

#if !DEBUG

            #region Address Pins - GPIO Outputs

            ////var pinA_0 = Pi.Gpio[BcmPin.Gpio26];
            ////pinA_0.PinMode = GpioPinDriveMode.Output;
            ////pinA_0.Write(false);

            //var pinA_1 = Pi.Gpio[BcmPin.Gpio19];
            //pinA_1.PinMode = GpioPinDriveMode.Output;
            ////pinA_1.Write(false);

            //var pinA_2 = Pi.Gpio[BcmPin.Gpio13];
            //pinA_2.PinMode = GpioPinDriveMode.Output;
            ////pinA_2.Write(false);

            //var pinA_3 = Pi.Gpio[BcmPin.Gpio06];
            //pinA_3.PinMode = GpioPinDriveMode.Output;
            ////pinA_3.Write(false);

            //var pinA_4 = Pi.Gpio[BcmPin.Gpio05];
            //pinA_4.PinMode = GpioPinDriveMode.Output;
            ////pinA_4.Write(false);

            //var pinA_5 = Pi.Gpio[BcmPin.Gpio22];
            //pinA_5.PinMode = GpioPinDriveMode.Output;
            ////pinA_5.Write(false);

            //var pinA_6 = Pi.Gpio[BcmPin.Gpio27];
            //pinA_6.PinMode = GpioPinDriveMode.Output;
            ////pinA_6.Write(false);

            //var pinA_7 = Pi.Gpio[BcmPin.Gpio17];
            //pinA_7.PinMode = GpioPinDriveMode.Output;
            ////pinA_7.Write(false);

            //var pinA_8 = Pi.Gpio[BcmPin.Gpio04];
            //pinA_8.PinMode = GpioPinDriveMode.Output;
            ////pinA_8.Write(false);

            #endregion Address Pins - GPIO Outputs

            pinA_1.Write((_inputMask & (1 << 0)) != 0); // Emergency (SOS)
            pinA_2.Write((_inputMask & (1 << 1)) != 0); // Service (Maintenance)
            pinA_3.Write((_inputMask & (1 << 2)) != 0); // Unlock Door B
            pinA_4.Write((_inputMask & (1 << 3)) != 0); // Unlock Door A
            pinA_5.Write((_inputMask & (1 << 4)) != 0); // State Bit 1
            pinA_6.Write((_inputMask & (1 << 5)) != 0); // State Bit 2
            pinA_7.Write((_inputMask & (1 << 6)) != 0); // State Bit 3
            pinA_8.Write((_inputMask & (1 << 7)) != 0); // State Bit 4
#endif
        }

        public static void SetInputs(byte _inputMask, bool clockBit)
        {

#if !DEBUG
            pinA_1.Write((_inputMask & (1 << 0)) != 0); // Emergency (SOS)
            pinA_2.Write((_inputMask & (1 << 1)) != 0); // Service (Maintenance)
            pinA_3.Write((_inputMask & (1 << 2)) != 0); // Unlock Door B
            pinA_4.Write((_inputMask & (1 << 3)) != 0); // Unlock Door A
            pinA_5.Write((_inputMask & (1 << 4)) != 0); // State Bit 1
            pinA_6.Write((_inputMask & (1 << 5)) != 0); // State Bit 2
            pinA_7.Write((_inputMask & (1 << 6)) != 0); // State Bit 3
            pinA_8.Write((_inputMask & (1 << 7)) != 0); // State Bit 4

            if (clockBit)
            {
                pinA_0.Write(true);
            }
            else
            {
                pinA_0.Write(false);
            }
#endif
        }

        public static byte ReadOutputs()
        {

            byte result = 0;
#if !DEBUG

            if (pinIO_0.Read()) result |= (1 << 0); // State Bit 1 - Feed Address A5
            if (pinIO_1.Read()) result |= (1 << 1); // State Bit 2 - Feed Address A6
            if (pinIO_2.Read()) result |= (1 << 2); // State Bit 3 - Feed Address A7
            if (pinIO_3.Read()) result |= (1 << 3); // State Bit 4 - Feed Address A8

            if (pinIO_4.Read()) result |= (1 << 4); // Pressure Vent
            if (pinIO_5.Read()) result |= (1 << 5); // Decontamination Spray
            if (pinIO_6.Read()) result |= (1 << 6); // Unlock Door B
            if (pinIO_7.Read()) result |= (1 << 7); // Unlock Door A
#endif

            return result;
        }
    }

    #endregion Exhaustive Truth Table Transition Sweeps

    #region Logger 

    public static class ConsoleLogger
    {
        private static readonly string LogFilePath = $"TestRun_{DateTime.Now:yyyyMMdd_HHmmss}.log";

        public static void Info(string message)
        {
            string entry = $"[{DateTime.Now:HH:mm:ss}] [INFO] {message}";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(entry);
            Console.ResetColor();
            File.AppendAllText(LogFilePath, entry + Environment.NewLine);
        }

        public static bool AssertState(string testCase, int expectedByte, int actualByte)
        {
            bool passed = (expectedByte == actualByte);
            string status = passed ? "PASS" : "FAIL";
            string entry = $"[{DateTime.Now:HH:mm:ss}] [{status}] {testCase} | Expected: 0x{expectedByte:X2}, Actual: 0x{actualByte:X2}";

            Console.ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(entry);
            Console.ResetColor();

            File.AppendAllText(LogFilePath, entry + Environment.NewLine);
            return passed;
        }
    }

    #endregion Logger 
}
