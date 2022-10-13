public class Program
{
    private static Users userLogged;
    private static String[] reserveList = { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
    public static void Main(string[] args)
    {
        Users[] users = new Users[10];
        Reserve[] reserve = new Reserve[10];
        bool[] seatA = { false, false, false, false, false, false, false, false, false, false };
        bool[] seatB = { false, false, false, false, false, false, false, false, false, false };

        int member = 0;
        int countReserve = 0;
        while (true)
        {
            int ch = indexMenu();
            switch (ch)
            {

                case 1:
                    users[member++] = Register(users);
                    Console.Clear();
                    break;
                case 2:

                    int status = Login(users);
                    if (status == 1)
                    {
                        bool stay = true;
                        do
                        {
                            Console.Clear();
                            int loggedMenu = indexLogged();
                            if (loggedMenu == 1)
                            {
                                int statusReserve = indexReserve(seatA, seatB);
                                if (statusReserve == 3)
                                {
                                    string seatList = "";
                                    int seq = 0;
                                    double price = 5235.25, totalAmount = 0;

                                    if (userLogged.getStudentId() != null)
                                    {
                                        price = 1200.5;
                                    }

                                    Array.ForEach(reserveList, info =>
                                            {
                                                if (info != null)
                                                {
                                                    if (seq != 0)
                                                    {
                                                        seatList += "," + info;
                                                    }
                                                    else
                                                    {
                                                        seatList = info;
                                                    }
                                                    seq++;
                                                    totalAmount += price;

                                                    string c = info.Substring(0, 1);
                                                    string n = info.Substring(1);
                                                    if (int.TryParse(n, out int seatNum))
                                                    {
                                                        if (c == "A")
                                                        {
                                                            seatA[seatNum] = true;
                                                        }
                                                        else
                                                        {
                                                            seatB[seatNum] = true;
                                                        }
                                                    }
                                                }
                                            });
                                    reserve[countReserve++] = new Reserve(userLogged, seatList, totalAmount);
                                    int selectedPayment = indexPayment(userLogged, reserve[countReserve - 1]);
                                    if (selectedPayment == 1)
                                    {
                                        indexBank(reserve[countReserve - 1]);
                                    }
                                    else if (selectedPayment == 2)
                                    {
                                        indexCredit(reserve[countReserve - 1]);
                                    }
                                    else if (selectedPayment == 3)
                                    {

                                        Array.ForEach(reserveList, info =>
                                        {
                                            if (info != null)
                                            {
                                                string c = info.Substring(0, 1);
                                                string n = info.Substring(1);
                                                if (int.TryParse(n, out int seatNum))
                                                {
                                                    if (c == "A")
                                                    {
                                                        seatA[seatNum] = false;
                                                    }
                                                    else
                                                    {
                                                        seatB[seatNum] = false;
                                                    }
                                                }
                                            }
                                        });
                                        reserve[countReserve - 1].setCancaled();
                                    }
                                }

                            }
                            else if (loggedMenu == 2)
                            {

                                reserveResult(reserve);
                                stay = true;
                            }
                            else if (loggedMenu == 3)
                            {
                                stay = false;
                            }
                        } while (stay);

                    }
                    break;
            }
        }
    }
    private static int indexMenu()
    {
        Console.WriteLine("*******************************");
        Console.WriteLine("1.Register");
        Console.WriteLine("2.Login");
        Console.Write("Enter choice: ");
        string? ch = Console.ReadLine();

        if (int.TryParse(ch, out int number))
        {
            return number;
        }
        throw new Exception("Please input decimal data.");
    }
    private static void reserveResult(Reserve[] reserve)
    {

        if (userLogged != null)
        {
            Reserve reserveResult = null;

            Array.ForEach(reserve, info =>
            {
                if (info != null)
                {
                    if (info.checkReserve(userLogged))
                    {
                        reserveResult = info;
                    }
                }

            }
            );
            if (reserveResult == null)
            {
                Console.WriteLine("Please book your seat first.");
            }
            else
            {
                string[] seat = reserveResult.getSeatList().Split(",");
                string typeA = "", typeB = "";
                int posTypeA = 0, posTypeB = 0;
                double price = 5235.25, totalAmount = 0, totalAmountA = 0, totalAmountB = 0;
                if (userLogged.getStudentId() != null)
                {
                    price = 1200.5;
                }

                Array.ForEach(seat, item =>
                {
                    if (item != null)
                    {

                        if (item[0] == 'A')
                        {
                            if (posTypeA == 0)
                            {
                                typeA = item;
                            }
                            else
                            {
                                typeA += "," + item;
                            }
                            totalAmountA += price;
                            posTypeA++;
                        }
                        else
                        {
                            if (posTypeB == 0)
                            {
                                typeB = item;
                            }
                            else
                            {
                                typeB += "," + item;
                            }
                            totalAmountB += price;
                            posTypeB++;
                        }
                    }
                }
            );
                totalAmount = totalAmountA + totalAmountB;
                if (userLogged.getStudentId() == null)
                {
                    Console.WriteLine("บุคคลทั่วไป");
                }
                else
                {
                    Console.WriteLine("นักเรียน");
                }
                Console.WriteLine("หมายเลข A1-A10 รวม {0}", totalAmountA);
                Console.WriteLine(typeA);
                Console.WriteLine("หมายเลข A1-A10 รวม {0}", totalAmountB);
                Console.WriteLine(typeB);
                Console.WriteLine("รวมทั้งหมด", totalAmount);
                Console.WriteLine("ช่องทางการชำระ");
                if (reserveResult.getPayment().isBank())
                {
                    Console.WriteLine("ชำระเงินผ่านธนาคาร");
                    Console.Write("ชื่อบัญชี: ");
                    Console.WriteLine(reserveResult.getPayment().getBankName());
                    Console.Write("หมายเลขบัญชี: ");
                    Console.WriteLine(reserveResult.getPayment().getBankNumber());
                }
                else
                {
                    Console.WriteLine("ชำระเงินผ่านบัตรเครดิต");
                    Console.Write("ชื่อผู้ถือบัตร: ");
                    Console.WriteLine(reserveResult.getPayment().getOwnerName());
                    Console.Write("หมายเลขบัญชี: ");
                    Console.WriteLine(reserveResult.getPayment().getCreditNumber());
                    Console.Write("วันที่หมดอายุ: ");
                    Console.WriteLine(reserveResult.getPayment().getDateCredit());
                    Console.Write("cvv: ");
                    Console.WriteLine(reserveResult.getPayment().getCVV());
                }

            }

        }
        Console.Write("Type any word to continue: ");
        Console.ReadLine();
    }
    private static int indexLogged()
    {
        Console.WriteLine("*******************************");
        Console.WriteLine("1.Reserve");
        Console.WriteLine("2.Reservation result");
        Console.WriteLine("3.Logout");
        Console.Write("Enter choice: ");
        string? ch = Console.ReadLine();

        if (int.TryParse(ch, out int number))
        {
            return number;
        }
        throw new Exception("Please input decimal data.");
    }
    private static void indexBank(Reserve reserve)
    {
        Console.WriteLine("*******************************");
        Console.Write("ชื่อบัญชี: ");
        string? nameBank = Console.ReadLine();
        Console.Write("เลขบัญชี: ");
        string? numberBank = Console.ReadLine();
        Payment payment = new Payment(nameBank, numberBank);
        reserve.setPayment(payment);
        reserve.setPayed();
    }

    private static void indexCredit(Reserve reserve)
    {
        Console.WriteLine("*******************************");
        Console.Write("ชื่อผู้ถือบัตร: ");
        string? ownerBank = Console.ReadLine();
        Console.Write("หมายเลขบัตร: ");
        string? numberCredit = Console.ReadLine();
        Console.Write("วันที่หมดอายุ: ");
        string? dateCredit = Console.ReadLine();
        Console.Write("หมายเลข cvv: ");
        string? cvv = Console.ReadLine();
        Payment payment = new Payment(ownerBank, numberCredit, dateCredit, cvv);
        reserve.setPayment(payment);
        reserve.setPayed();
    }

    private static int indexReserve(bool[] seatA, bool[] seatB)
    {
        int action = 0;
        int positionReserve = 0;
        do
        {
            Console.WriteLine("*******************************");
            Console.WriteLine("Type 'exit' to return to main page.");
            Console.WriteLine("Type 'checkout' to return to checkout page.");
            Console.WriteLine("Seating Plan");
            Console.WriteLine("A1-A10");
            Console.WriteLine("B1-B10");
            Console.Write("Enter seat code: ");
            string? seat = Console.ReadLine();

            if (seat == "exit")
            {
                action = 1;
                for (int i = 0; i < 20; i++)
                {
                    reserveList[i] = null;
                    if (i < 10)
                    {
                        seatA[i] = false;
                        seatB[i] = false;
                    }

                }
            }
            else if (seat == "checkout")
            {
                int countReserve = 0;
                for (int i = 0; i < 20; i++)
                {
                    if (reserveList[i] != null)
                    {
                        countReserve++;
                    }
                }
                if (countReserve == 0)
                {
                    action = 2;
                }
                else
                {
                    action = 3;
                }

            }
            else
            {

                if (seat.Length >= 2)
                {
                    string c = seat.Substring(0, 1);
                    string n = seat.Substring(1);
                    if (int.TryParse(n, out int seatNumber))
                    {
                        if (c == "A")
                        {
                            if (userLogged.getStudentId() == null)
                            {
                                if (seatA[seatNumber])
                                {
                                    Console.WriteLine("Already booked. Please try again.");
                                }
                                else
                                {
                                    reserveList[positionReserve++] = seat;
                                    seatA[seatNumber] = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Cannot book. Please try again.");
                            }
                        }
                        else
                        {
                            if (seatB[seatNumber])
                            {
                                Console.WriteLine("Already booked. Please try again.");
                            }
                            else
                            {
                                reserveList[positionReserve++] = seat;
                                seatB[seatNumber] = true;
                            }
                        }
                    }

                }
            }


        } while (action == 0);
        return action;
    }
    private static int Login(Users[] users)
    {
        int correct = 0;
        do
        {
            Console.WriteLine("*******************************");
            Console.WriteLine("Type 'exit' to return to main page.");
            Console.Write("Email: ");
            string? email = Console.ReadLine();

            if (email == "exit")
            {
                correct = 2;
                return correct;
            }

            Console.Write("Password: ");
            string? password = Console.ReadLine();
            Array.ForEach(users, user =>
            {
                if (user != null)
                {

                    if (email == user.getEmail() && password == user.getPassword())
                    {
                        correct = 1;
                        userLogged = user;
                    }
                }

            }
            );

            if (correct == 0)
            {
                Console.WriteLine("Incorrect email or password. Please try again.");
            }
        } while (correct == 0);
        return correct;
    }

    private static Users Register(Users[] users)
    {
        Console.WriteLine("*******************************");
        Console.WriteLine("ประเภทผู้ใช้งาน");
        Console.WriteLine("1.บุคคลทั่วไป");
        Console.WriteLine("2.นักเรียน");
        Console.Write("Enter Number: ");
        string? type = Console.ReadLine();


        int correct = 0;
        string? prefixName, firstName, lastName, password, email, studentId = null;
        int age = 0;
        do
        {
            bool passed = true;
            Console.WriteLine("*******************************");
            Console.WriteLine("Prefix Name");
            Console.WriteLine("1.นาย");
            Console.WriteLine("2.นาง");
            Console.WriteLine("3.นางสาว");
            prefixName = "นาย";
            int position = 0;
            do
            {
                Console.Write("Enter Number: ");
                string? n = Console.ReadLine();
                if (int.TryParse(n, out int num))
                {
                    position = num;
                }
                if (position == 1)
                {
                    prefixName = "นาย";
                }
                else if (position == 2)
                {
                    prefixName = "นาง";
                }
                else if (position == 3)
                {
                    prefixName = "นางสาว";
                }
                else
                {
                    passed = false;
                    Console.WriteLine("Invalid prefix. Please try again.");
                }

            } while (position == 0);


            Console.Write("First Name: ");
            firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            lastName = Console.ReadLine();
            Console.Write("Age: ");

            string? a = Console.ReadLine();
            if (int.TryParse(a, out int number))
            {
                age = number;
            }
            else
            {
                throw new Exception("Please input decimal data.");
            }


            Console.Write("Email: ");
            email = Console.ReadLine();

            if (type == "2")
            {
                Console.Write("Student Id: ");
                studentId = Console.ReadLine();
            }


            Console.Write("Password: ");
            password = Console.ReadLine();
            Console.Write("Confirm Password: ");
            string? confirmPassword = Console.ReadLine();


            if (password != confirmPassword)
            {
                passed = false;
                Console.WriteLine("Mismatched passwords. Please try again.");
            }

            Array.ForEach(users, user =>
            {
                if (user != null)
                {

                    if (prefixName == user.getPrefix() && firstName == user.getFirstName() && lastName == user.getLastName())
                    {
                        passed = false;
                        Console.WriteLine("User is already registered. Please try again.");
                    }
                }

            }
            );

            if (passed)
            {
                correct = 1;
            }

        } while (correct == 0);

        if (type == "1")
        {
            return new Users(prefixName, firstName, lastName, email, password, age);
        }
        else
        {
            return new Users(prefixName, firstName, lastName, email, password, age, studentId);
        }

    }

    private static int indexPayment(Users users, Reserve reserves)
    {
        int choice = 0;
        do
        {
            Console.WriteLine("*******************************");
            Console.WriteLine("Payment");

            if (reserves.checkUserReserve(users))
            {
                Console.WriteLine(reserves.getSeatList());
            }



            Console.WriteLine("รวมราคา {0}", reserves.getTotalAmount());
            Console.WriteLine("1.ชำระเงินผ่านธนาคาร");
            Console.WriteLine("2.ชำระเงินผ่านบัตรเครดิต");
            Console.WriteLine("3.ยกเลิกการจอง");
            Console.Write("Enter choice: ");
            string? ch = Console.ReadLine();

            if (int.TryParse(ch, out int number))
            {
                if (number >= 1 && number <= 3)
                {
                    choice = number;
                }
            }
        } while (choice == 0);
        return choice;
    }

}