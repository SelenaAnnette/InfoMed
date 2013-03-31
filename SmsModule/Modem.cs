namespace SmsModule
{
    using System;    
    using System.IO.Ports;    
    using System.Text;    

    public class Modem : IModem
    {
        private SerialPort serialPort;
        private string error_message = ""; //if error / catch
        private string log = "";
        private const int PortTimeOut = 100; //пока без таймаута вручную , устанавливается автоматический таймаут при инициализации.
        private const int SmsTimeOut = 100;        

        public Modem()
        {
            this.serialPort = new SerialPort();   
        }

        ~Modem()
        {
            this.serialPort.Dispose();
        }


        public bool Initialize() //если модем найден и инициализирован, то вернет true, иначе false
        {            
            //выбираем первый компорт, который смог ответить на команду АТ (может быть и несколько портов которые ответят)
            foreach (string s in SerialPort.GetPortNames())///Get COMPort Name
            {
                serialPort.PortName = s;
                serialPort.Open();
                if (serialPort.IsOpen)
                {
                    try
                    {
                        serialPort.WriteTimeout = PortTimeOut;//100 default
                        serialPort.WriteLine("AT \r\n");
                    }
                    catch (TimeoutException ee)
                    {
                        error_message = ee.Message;
                    }
                    System.Threading.Thread.Sleep(100);
                    string Port_readed_line = serialPort.ReadExisting();
                    if (Port_readed_line.Length > 0)
                    {
                        //автоматический таймаут
                        serialPort.WriteTimeout = PortTimeOut;
                        serialPort.ReadTimeout = PortTimeOut;
                        break;
                    }
                    serialPort.Close();
                }
            }
            if (serialPort.PortName != "")
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public bool CheckConnection()
        {
            if (serialPort.PortName != "")
            {
                //2 раза шлю АТ, на второй раз должен ответить ОК, на первый возможно и error, если старая команда не завершена. 
                //True возвращается только если ответит ОК и ничто иное
                SendCommandToModem("AT");
                ReadRespondFromModem();
                SendCommandToModem("AT");
                string Port_readed_line = ReadRespondFromModem();
                if (Port_readed_line.IndexOf("OK") >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        } ///если модем ответил как положено, то вернет true, иначе false


        public bool SendSms(string phone_number, string message)
        {
            string Final_Respond = "";
            if (serialPort.IsOpen)
            {

                SendCommandToModem("AT");
                log += ReadRespondFromModem();

                //проверяем возможность отправки SMS при помощи данного устройства
                SendCommandToModem("AT+CSMS=0");
                log += ReadRespondFromModem();

                //выбираем режим работы модема - text mode
                SendCommandToModem("AT+CMGF=1");
                log += ReadRespondFromModem();

                // параметры модема для отправки SMS на русском языке
                SendCommandToModem("AT+CSMP=17,167,0,25");
                log += ReadRespondFromModem();

                //определяем кодировку сообщений в UCS2/Unicode
                SendCommandToModem("AT+CSCS=\"UCS2\"");
                log += ReadRespondFromModem();

                phone_number = ConvertToUCS2(phone_number);

                //отправили номер телефона-адресата
                SendCommandToModem("AT+CMGS=\"" + phone_number + "\"");
                log += ReadRespondFromModem();


                message = ConvertToUCS2(message); //txtInUCS2;

                //+ Ctrl+Z (^Z) или 0x1Ah 
                SendCommandToModem(message + "\x1A");
                //дополнительная пауза нужна после отправки сообщения перед получением ответа от модема
                System.Threading.Thread.Sleep(SmsTimeOut);//2000

                Final_Respond = ReadRespondFromModem();
                log += Final_Respond;
            }

            if (Final_Respond.IndexOf("OK") != -1)//если в конце вернул ОК, значит прошло успешно и ждем смску
            {
                return true;
            }
            else
            {
                return false;
            }
        }  //если последняя вернется ОК, то вернет true, иначе false



        private void SendCommandToModem(string comm)
        {
          //  System.Threading.Thread.Sleep(PortTimeOut);
            serialPort.Write(comm+"\r\n");
            
        }  //отправить команду



        private string ReadRespondFromModem()
        {
          //  System.Threading.Thread.Sleep(PortTimeOut);
            string DataFromPort = "";
            DataFromPort = serialPort.ReadExisting();
            return DataFromPort;

        }

        private string ConvertToUCS2(string txtInRus) //надо переделать на автомат
        {
            //строка с алфавитом
            String strAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюяABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789'-* :;)(.,!=_";

            String[] ArrayUCSCode = new String[141]{            
            "0410","0411","0412","0413","0414","0415","00A8","0416","0417",
            "0418","0419","041A","041B","041C","041D","041E","041F","0420",
            "0421","0422","0423","0424","0425","0426","0427","0428","0429",
            "042A","042B","042C","042D","042E","042F","0430","0431","0432","0433",
            "0434","0435","00B8","0436","0437","0438","0439","043A","043B",
            "043C","043D","043E","043F","0440","0441","0442","0443","0444",
            "0445","0446","0447","0448","0449","044A","044B", "044C","044D","044E","044F","0041",
            "0042","0043","0044","0045","0046","0047","0048","0049","004A",
            "004B","004C","004D","004E","004F","0050","0051","0052","0053",
            "0054","0055","0056","0057","0058","0059","005A","0061","0062",
            "0063","0064","0065","0066","0067","0068","0069","006A","006B",
            "006C","006D","006E","006F","0070","0071","0072","0073","0074",
            "0075","0076","0077","0078","0079","007A","0030","0031","0032",
            "0033","0034","0035","0036","0037","0038","0039","0027","002D",
            "002A","0020","003A","003B","0029","0028","002E","002C","0021",
            "003D","005F"};
            StringBuilder UCS = new StringBuilder(txtInRus.Length);
            Int32 intLetterIndex = 0;
            for (int i = 0; i < txtInRus.Length; i++)
            {
                intLetterIndex = strAlphabet.IndexOf(txtInRus[i]);
                if (intLetterIndex != -1)
                {
                    UCS.Append(ArrayUCSCode[intLetterIndex]);
                }
            }

            return UCS.ToString();
        }

       
    }
}

    



