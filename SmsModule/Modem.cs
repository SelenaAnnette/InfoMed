namespace SmsModule
{
    using System;    
    using System.IO.Ports;    
    using System.Text;
    using System.Threading;

    public class Modem : IModem
    {
        private SerialPort serialPort;
        private string error_message = ""; //if error / catch
        private string log = "";
        private const int PortTimeOut = 100; //пока без таймаута вручную , устанавливается автоматический таймаут при инициализации.
        private const int SmsTimeOut = 100;

        private object lockObject;

        public Modem()
        {
            this.serialPort = new SerialPort();
            this.lockObject = new object();            
        }

        ~Modem()
        {
            this.serialPort.Dispose();
        }
        //public
        public bool Initialize() //если модем найден и инициализирован, то вернет true, иначе false
        {
            lock (this.lockObject)
            {

                serialPort = new SerialPort();


                foreach (string s in SerialPort.GetPortNames()) ///Get COMPort Name
                {
                    serialPort.PortName = s;
                    if (serialPort.IsOpen)
                    {
                        serialPort.Close();
                    }
                    serialPort.Open();
                    if (serialPort.IsOpen)
                    {
                        try
                        {
                            serialPort.WriteTimeout = PortTimeOut; //100 default
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
        }

        public bool CheckConnection()
        {
            if (serialPort.PortName != "")
            {
                lock (this.lockObject)
                {
                    SendCommandToModem("AT");
                    ReadRespondFromModem();
                    SendCommandToModem("AT");
                    string Port_readed_line = ReadRespondFromModem();
                    // MessageBox.Show(Port_readed_line);
                    if (Port_readed_line.IndexOf("OK") >= 0)
                    {
                        // MessageBox.Show("AT => OK");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        } //проверка соединения

        public bool SendSms(string phone_number, string message)
        {
            lock (this.lockObject)
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
                    System.Threading.Thread.Sleep(2000);

                    Final_Respond = ReadRespondFromModem();
                    log += Final_Respond;

                }

                if (Final_Respond.IndexOf("OK") != -1) //если в конце вернул ОК, значит прошло успешно и ждем смску
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        } //Отправка

        public Sms[] GetAllSms() //возвращает все смс как массив объектов смс со всем расшифрованными данными
        {
            lock (this.lockObject)
            {
                Sms[] My_Sms__Array = new Sms[100];
                string AllSms = "";

                if (CheckConnection())
                {
                    int index_sms = 0;
                    int index_text = 0;
                    int NewLineIndex = 0;
                    string Buffer = "";
                    SendCommandToModem("AT+CMGF=0"); //PDU Mode
                    SendCommandToModem("AT+CMGL=4"); //взять все сообщения
                    //SendCommandToModem("AT+CMGL=1");//взять все прочитанные сообщения
                    AllSms = ReadRespondFromModem();

                    while (AllSms.LastIndexOf("+CMGL") != -1)
                    {
                        //начинаем перебор с конца, в начале массива будут самые свежие смс
                        index_text = AllSms.LastIndexOf("+CMGL"); //индекс последней смс
                        Buffer = AllSms.Substring(index_text, AllSms.Length - index_text - 1); //копируем последнюю смс
                        if (Buffer[5] == '=') break; //если в буфере не смс а наш запрос то выходим нахер из цикла распарсивания
                        AllSms = AllSms.Substring(0, index_text); //оставляем все кроме последней
                        NewLineIndex = Buffer.IndexOf("\r\n"); //индекс конца заголовка
                        My_Sms__Array[index_sms] = parse_sms(Buffer); //объект смс заносим в массив

                        // MessageBox.Show("i= "+ index_sms.ToString() +" b "+Buffer);//показать что мы разобрали
                        index_sms += 1; //переход к следующему
                    }
                }

                return My_Sms__Array;
            }
        }

        public bool DeleteByDate(DateTime dt)//дата время не включительно, относительно параметра
        {
            lock (this.lockObject)
            {
                Sms[] AllSms = new Sms[100];
                int counter = 0; //счетчик удаленных СМС
                AllSms = GetAllSms(); //все смс
                foreach (Sms CurrentSms in AllSms)
                {
                    if (CurrentSms != null)
                    {
                        if (CurrentSms.DTime < dt)
                        {
                            SendCommandToModem("AT+CMGD=" + CurrentSms.Index);
                            counter += 1;
                            if (ReadRespondFromModem().IndexOf("OK") < 0) return false; //если ошибка и не вурнул ОК
                        }
                    }
                }

                return true;
            }
        }

        //private:
        private void SendCommandToModem(string comm)
        {
            System.Threading.Thread.Sleep(PortTimeOut);
            serialPort.Write(comm + "\r\n");

        } //только команда без спец знаков в параметр

        private string ReadRespondFromModem()
        {
            System.Threading.Thread.Sleep(PortTimeOut);
            string DataFromPort = "";
            DataFromPort = serialPort.ReadExisting();
            return DataFromPort;

        }

        private string ConvertToUCS2(string txt) //нужно переделать на автомат.кодирование встроенными функциями
        {
            byte[] byte_array = Encoding.BigEndianUnicode.GetBytes(txt);
            return BitConverter.ToString(byte_array).Replace("-", string.Empty);
        }

        private Sms parse_sms(string code)
        {
            Sms NewSms = new Sms();
            int Length = 0; //вся длина
            int index_index = 0; //индекс в строке  индекса смски
            int index_comma = 0; //индекс первой запятой (после индекса)
            int NewLineIndex = 0; //конец заголовка смс
            bool TP_UDH = false;//user data header, есть ли заголовок, показывающий номер этой смс в серии

            Length = code.Length;
            NewLineIndex = code.IndexOf("\r\n") + 2;
            index_index = code.IndexOf(" ") + 1;
            index_comma = code.IndexOf(",");
            NewSms.Index = Convert.ToInt32(code.Substring(index_index, index_comma - index_index)); //Set Index
            code = code.Substring(NewLineIndex, Length - NewLineIndex); //Obrezat' zagolovok

            //заполнить служебные шифрованные поля
            int i_s = 0;
            int i_e = 0;
            i_s = 0;
            i_s += 2 + (Convert.ToInt32(code.Substring(0, 2)) * 2);

            NewSms.TP_SCA = code.Substring(0, i_s);
            NewSms.TP_MTICO = code.Substring(i_s, 2);
            i_s += 2;
            i_e = HexToInt(code.Substring(i_s, 2)) + 4; //длина номера + 2 символа
            if (i_e % 2 != 0) //учитываем четный символ F
            {
                i_e += 1;
            }
            NewSms.TP_OA = code.Substring(i_s, i_e);
            i_s += i_e;
            NewSms.TP_PID = "00";
            i_s += 2;
            NewSms.TP_DCS = code.Substring(i_s, 2);
            i_s += 2;
            NewSms.TP_SCTS = code.Substring(i_s, 14);
            i_s += 14;
            NewSms.TP_UDL = code.Substring(i_s, 2);
            i_s += 2;
            ///определить есть ли заголовок в начале текста. всего частей/эта смс
            string temp = HexToBinary(NewSms.TP_MTICO);
            TP_UDH = temp[1].ToString() == "1";//BOOL!
            if (TP_UDH)
            {
                int UDHsize = HexToInt(code.Substring(i_s, 2));
                NewSms.Part = code.Substring(i_s + (UDHsize * 2) - 2, 4);
                i_s += (UDHsize * 2) + 2;
                NewSms.TP_UD = code.Substring(i_s, (HexToInt(NewSms.TP_UDL) - UDHsize - 1) * 2);
            }
            else
            {
                NewSms.TP_UD = code.Substring(i_s, HexToInt(NewSms.TP_UDL) * 2);   //code.Length - i_s);
            }
            //заполнить понятные поля из служебных

            NewSms.From = get_sender(NewSms.TP_OA);
            NewSms.Text = get_text_sms(NewSms.TP_UD, NewSms.TP_DCS);
            NewSms.DTime = Convert.ToDateTime("20" +
                          NewSms.TP_SCTS[1].ToString() + NewSms.TP_SCTS[0].ToString() + "-" +
                          NewSms.TP_SCTS[3].ToString() + NewSms.TP_SCTS[2].ToString() + "-" +
                          NewSms.TP_SCTS[5].ToString() + NewSms.TP_SCTS[4].ToString() + "T" +
                          NewSms.TP_SCTS[7].ToString() + NewSms.TP_SCTS[6].ToString() + ":" +
                          NewSms.TP_SCTS[9].ToString() + NewSms.TP_SCTS[8].ToString() + ":" +
                          NewSms.TP_SCTS[11].ToString() + NewSms.TP_SCTS[10].ToString()); // дата и время до секунд, когда СЦ была получена смс от пациента

            return NewSms;

        }

        private int HexToInt(string hex)
        {
            int ret = (Int16)Convert.ToInt16(hex, 16);
            return ret;
        }

        private string HexToBinary(string hex)
        {
            string binaryval = Convert.ToString(Convert.ToInt32("8" + hex, 16), 2);//8ка дает старший бит=1, чтобы не обрезал старшие нули. Нужно обрезать первые 4 символа в бинаре
            binaryval = binaryval.Substring(4);
            return binaryval;
        }

        private string rotate(string st) ///меняет местами символы
        {
            char f = st[0];
            char buf = st[1];
            return buf.ToString() + f.ToString();

        }

        private string get_sender(string str)
        {
            string number = "";
            int len = HexToInt(str.Substring(0, 2)); //длина номера
            /*  if (len % 2 != 0) //четный символ F
              {
                  len += 1;
              }*/
            if (str.Substring(3, 1) == "1")//любой но не буквенно-цифровой // !!в т.ч. тип *000*
            {
                for (int i = 4; i < len + 4; i++)
                {
                    number += rotate(str.Substring(i, 2));
                    i++;
                }
                number = number.Substring(0, len); //на случай если в конце символ четности F, надо обрезать

            }
            else if (str.Substring(3, 1) == "0")//буквенно цифровой
            {
                number = ConvertFrom7bit(str.Substring(4, len));
            }
            else
            {
                return "unreachable_format";
            }
            return number;
        }

        private string get_text_sms(string TP_UD, string TP_DCS)
        {

            if (TP_DCS == "00")//7bit
            {
                return ConvertFrom7bit(TP_UD);
            }
            else
            {
                if ((TP_DCS == "08") || (TP_DCS == "18") || (TP_DCS == "08"))//UCS-2
                {
                    return ConvertFromUCS2(TP_UD);
                }

            }

            return "ok";
        }

        private string ConvertFromUCS2(string TP_UD)
        {

            StringBuilder UCS = new StringBuilder(TP_UD.Length);
            byte[] b = new byte[280];
            int b_counter = 0;
            int i = 0;
            for (i = 0; i < TP_UD.Length; i++)
            {
                string ss = "0x" + TP_UD.Substring(i, 2);
                b[b_counter] = Convert.ToByte(ss, 16);
                b_counter += 1;
                i += 1;

            }
            UCS.Append(Encoding.BigEndianUnicode.GetChars(b));
            return UCS.ToString();
        }

        private string ConvertFrom7bit(string txt)
        {
            byte[] b = new byte[180];//asi.GetBytes(txt);//
            int k, b_counter = 0;
            string buf = "";
            for (k = 0; k < txt.Length; k++)
            {
                string ss = HexToBinary(txt.Substring(k, 2));//байт в бинарном виде
                ss += buf; //прибавить справа 
                buf = ss.Substring(0, b_counter + 1); //скопировать первые i+1 бит
                ss = ss.Substring(b_counter + 1);//обрезать на i+1
                ss = "0" + ss;
                b[b_counter] = Convert.ToByte(ss, 2);
                b_counter += 1;
                if (buf.Length == 7)
                {
                    ss = "0" + buf;
                    b[b_counter] = Convert.ToByte(ss, 2);
                    b_counter += 1;
                    buf = "";
                }
                k += 1;//step=2
            }

            return Encoding.ASCII.GetString(b);


        }
        //
        }
   
    public class Sms
    {
        public int Index = 0;//индекс в памяти модема. после удаления любой смс произойдет сдвиг (? надо проверить)
        public string Text = "";//распознанный текст
        public string From = "";//отправитель, номер
        public DateTime DTime;//время и дата отправки
        public string Part = "0000";//00 = всего частей, вторые 00 - эта смс. если нули значит частей нет вообще. например 0201 значит первая из двух
        /// служебные данные, из которых можно получить все остальное если расшифровать
        public string TP_SCA;
        public string TP_MTICO;
        public string TP_OA;
        public string TP_PID;
        public string TP_DCS;
        public string TP_SCTS;
        public string TP_UDL;
        public string TP_UD;

    }
}