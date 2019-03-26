using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private Account[] ac = new Account[3];
        private Account activeAccount = null;
        private int userInput;
        private int pin;
       
        public Form1(Account[] ac)
        {
            InitializeComponent();
            button1.Click += new EventHandler(MyButtonClick);
            button2.Click += new EventHandler(MyButtonClick);
            button3.Click += new EventHandler(MyButtonClick);
            button4.Click += new EventHandler(MyButtonClick);
            button5.Click += new EventHandler(MyButtonClick);
            button6.Click += new EventHandler(MyButtonClick);
            button7.Click += new EventHandler(MyButtonClick);
            button8.Click += new EventHandler(MyButtonClick);
            button9.Click += new EventHandler(MyButtonClick);
            button0.Click += new EventHandler(MyButtonClick);
            btnEnter.Click += new EventHandler(inputSelection);
            this.ac = ac;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
          

            promptAccountNum();

        }

        void promptAccountNum()
        {
            while (lstThing.Items.Count > 0)
            {
                lstThing.Items.RemoveAt(0);
            }
            lstThing.Items.Add("Please Enter Your Account Number");
        }

        void sideInput(Object sender,EventArgs e)
        {
            Button button = sender as Button;
            String switchNum = Convert.ToString(button.Name[(button.Name).Length - 1 ]);
            Console.WriteLine(switchNum);
            switch (switchNum)
            {
                case "1":

                    withdawCash();
                    Console.WriteLine("Case 1");
                    break;
                case "2":
                    displayBalance();
                    Console.WriteLine("Case 2");
                    break;
                case "3":
                    activeAccount = null;
                    promptAccountNum();                   
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

        }
        void MyButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            txtInput.Text += button.Name[(button.Name).Length - 1];
        }

        void inputSelection(object sender, EventArgs e)
        {
            Button button = sender as Button;
            try
            {
                userInput = Convert.ToInt32(txtInput.Text);
            }
            catch
            {
                displayOptions();
            }
            txtInput.Text = "";
            if (activeAccount == null)
            {
                checkSequence();
            }
            else
            {

                if (activeAccount.checkPin(userInput))
                {
                    lstThing.Items.Add("Pin Correct");
                    pin = userInput;
                    displayOptions();
                }
                else
                {
                    lstThing.Items.Add("Pin Incorrect");
                }
            }
        }
        void withdawCash()
        {
           

            while (lstThing.Items.Count > 0)
            {
                lstThing.Items.RemoveAt(0);
            }
            lstThing.Items.Add("");
            lstThing.Items.Add("1>10 ");
            lstThing.Items.Add("");
            lstThing.Items.Add("");
            lstThing.Items.Add("2>20 ");
            lstThing.Items.Add("");
            lstThing.Items.Add("");
            lstThing.Items.Add("3>40 ");
            lstThing.Items.Add("");
            lstThing.Items.Add("");
            lstThing.Items.Add("4>100 ");
            lstThing.Items.Add("");
            lstThing.Items.Add("");
            lstThing.Items.Add("5> 500");
            lstThing.Items.Add("");

            lstThing.Items.Add("3> exit");
            resetButtons();
            btnOption1.Click += new EventHandler(selectMoney);
            btnOption2.Click += new EventHandler(selectMoney);
            btnOption3.Click += new EventHandler(selectMoney);
            btnOption4.Click += new EventHandler(selectMoney);
            btnOption5.Click += new EventHandler(selectMoney);
        }

        void selectMoney(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String switchNum = Convert.ToString(button.Name[(button.Name).Length - 1]);
            Console.WriteLine(switchNum);
            switch (switchNum)
            {
                case "1":
                    decBalance(10);
                    break;
                case "2":
                    decBalance(20);
                    break;
                case "3":
                    decBalance(40);
                    break;
                case "4":
                    decBalance(100);
                    break;
                case "5":
                    decBalance(500);
                    break;
                default:
                    Console.WriteLine("Default case(2nd)");
                    break;
            }

        }
        void decBalance(int value)
        {
            while (lstThing.Items.Count > 0)
            {
                lstThing.Items.RemoveAt(0);
            }

            if (activeAccount.decrementBalance(value)){
                lstThing.Items.Add("new balance " + activeAccount.getBalance());
                lstThing.Items.Add("Press enter button to continue:");
            }
            else
            {
                lstThing.Items.Add("There is not enough funds in this account");
                lstThing.Items.Add("Press enter button to continue:");
               
            }
        }
        void displayOptions()
        {
            Console.WriteLine("DISPLAY");
                while (lstThing.Items.Count > 0)
                {
                 lstThing.Items.RemoveAt(0);
                 }
                lstThing.Items.Add("1> take out cash");
                lstThing.Items.Add("");
                lstThing.Items.Add("2> balance");
                lstThing.Items.Add("");
                lstThing.Items.Add("3> exit");

            resetButtons();

            btnOption1.Click += new EventHandler(sideInput);
            btnOption2.Click += new EventHandler(sideInput);
            btnOption3.Click += new EventHandler(sideInput);


        }
        void resetButtons()
        {
            RemoveClickEvent(btnOption1);
            RemoveClickEvent(btnOption2);
            RemoveClickEvent(btnOption3);
            RemoveClickEvent(btnOption4);
            RemoveClickEvent(btnOption5);

        }
        
        private void displayBalance()
        {
            if (this.activeAccount != null)
            {
                while (lstThing.Items.Count > 0)
                {
                    lstThing.Items.RemoveAt(0);
                }
                lstThing.Items.Add(" your current balance is : " + activeAccount.getBalance());
                lstThing.Items.Add("Press Enter Button to Continue");
            }
        }
        


        private void lstThing_SelectedIndexChanged(object sender, EventArgs e)
        {
    
        }

        void checkSequence()
        {

            activeAccount = findAccount();

            if (activeAccount != null)
            {
                Console.WriteLine("AccountFound");
                promptForPin();
            }
            else
            {   //if the account number entered is not found let the user know!
                Console.WriteLine("no matching account found.");
                
            }
            
        }
        private Account findAccount()
        {

            for (int i = 0; i < this.ac.Length; i++)
            {
                if (ac[i].getAccountNum() == userInput)
                {
                    return ac[i];
                }
            }
           return null;

        }
            private bool getAccountNumber()
        {
            //lstThing.Items.Clear;
            lstThing.Items.Add("Enter your account number..");

            return true;
        }
        

        private void promptForPin()
        {
            lstThing.Items.RemoveAt(0);
            lstThing.Items.Add("Enter Pin");      
        }
        /**
         * Code From
         * https://stackoverflow.com/questions/91778/how-to-remove-all-event-handlers-from-an-event
         * **/

        private void RemoveClickEvent(Button b)
        {
            FieldInfo f1 = typeof(Control).GetField("EventClick",
                BindingFlags.Static | BindingFlags.NonPublic);
            object obj = f1.GetValue(b);
            PropertyInfo pi = b.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList list = (EventHandlerList)pi.GetValue(b, null);
            list.RemoveHandler(obj, list[obj]);
        }



        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button0_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {

        }
        private void btnEnter_Click(object sender, EventArgs e)
        {

        }

        private void btnOption1_Click(object sender, EventArgs e)
        {

        }

        private void btnOption2_Click(object sender, EventArgs e)
        {

        }
    }


    /*
     *   The Account class encapusulates all features of a simple bank account
     */
    public class Account
    {
        //the attributes for the account
        private int balance;
        private int pin;
        private int accountNum;

        // a constructor that takes initial values for each of the attributes (balance, pin, accountNumber)
        public Account(int balance, int pin, int accountNum)
        {
            this.balance = balance;
            this.pin = pin;
            this.accountNum = accountNum;
        }

        //getter and setter functions for balance
        public int getBalance()
        {
            return balance;
        }
        public void setBalance(int newBalance)
        {
            this.balance = newBalance;
        }

        /*
         *   This funciton allows us to decrement the balance of an account
         *   it perfomes a simple check to ensure the balance is greater tha
         *   the amount being debeted
         *   
         *   reurns:
         *   true if the transactions if possible
         *   false if there are insufficent funds in the account
         */
        public Boolean decrementBalance(int amount)
        {
            if (this.balance > amount)
            {
                balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * This funciton check the account pin against the argument passed to it
         *
         * returns:
         * true if they match
         * false if they do not
         */
        public Boolean checkPin(int pinEntered)
        {
            if (pinEntered == pin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int getAccountNum()
        {
            return accountNum;
        }

    }
    /* 
     *      This is out main ATM class that preforms the actions outlined in the assigment hand out
     *      
     *      the constutor contains the main funcitonality.
     */

    class ATM
    {
        //local referance to the array of accounts
        private Account[] ac;

        //this is a referance to the account that is being used
        private Account activeAccount = null;

        // the atm constructor takes an array of account objects as a referance
        public ATM(Account[] ac)
        {
            this.ac = ac;

            // an infinite loop to keep the flow of controll going on and on
            while (true)
            {

                //ask for account number and store result in acctiveAccount (null if no match found)
              

                if (activeAccount != null)
                {
                    //if the account is found check the pin 
                    if (activeAccount.checkPin(this.promptForPin()))
                    {
                        //if the pin is a match give the options to do stuff to the account (take money out, view balance, exit)
                        dispOptions();
                    }
                }
                else
                {   //if the account number entered is not found let the user know!
                    Console.WriteLine("no matching account found.");
                }

                //wipes all text from the console
                Console.Clear();
            }


        }
      

        /*
         *    this method promts for the input of an account number
         *    the string input is then converted to an int
         *    a for loop is used to check the enterd account number
         *    against those held in the account array
         *    if a match is found a referance to the match is returned
         *    if the for loop completest with no match we return null
         * 
         */

        /*
         * 
         *  this jsut promt the use to enter a pin number
         *  
         * returns the string entered converted to an int
         * 
         */
        private int promptForPin()
        {
            Console.WriteLine("enter pin:");
            String str = Console.ReadLine();
            int pinNumEntered = Convert.ToInt32(str);
            return pinNumEntered;
        }

        /*
         * 
         *  give the use the options to do with the accoutn
         *  
         *  promt for input
         *  and defer to appropriate method based on input
         *  
         */
        private void dispOptions()
        {
            Console.WriteLine("1> take out cash");
            Console.WriteLine("2> balance");
            Console.WriteLine("3> exit");

            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1)
            {
                dispWithdraw();
            }
            else if (input == 2)
            {
                dispBalance();
            }
            else if (input == 3)
            {


            }
            else
            {

            }

        }

        /*
         * 
         * offer withdrawable amounts
         * 
         * based on input attempt to withraw the corosponding amount of money
         * 
         */
        private void dispWithdraw()
        {
            Console.WriteLine("1> 10");
            Console.WriteLine("2> 50");
            Console.WriteLine("3> 500");

            int input = Convert.ToInt32(Console.ReadLine());

            if (input > 0 && input < 4)
            {

                //opiton one is entered by the user
                if (input == 1)
                {

                    //attempt to decrement account by 10 punds
                    if (activeAccount.decrementBalance(10))
                    {
                        //if this is possible display new balance and await key press
                        Console.WriteLine("new balance " + activeAccount.getBalance());
                        Console.WriteLine(" (prese enter to continue)");
                        Console.ReadLine();
                    }
                    else
                    {
                        //if this is not possible inform user and await key press
                        Console.WriteLine("insufficent funds");
                        Console.WriteLine(" (prese enter to continue)");
                        Console.ReadLine();
                    }
                }
                else if (input == 2)
                {
                    if (activeAccount.decrementBalance(50))
                    {
                        Console.WriteLine("new balance " + activeAccount.getBalance());
                        Console.WriteLine(" (prese enter to continue)");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("insufficent funds");
                        Console.WriteLine(" (prese enter to continue)");
                        Console.ReadLine();
                    }
                }
                else if (input == 3)
                {
                    if (activeAccount.decrementBalance(500))
                    {
                        Console.WriteLine("new balance " + activeAccount.getBalance());
                        Console.WriteLine(" (prese enter to continue)");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("insufficent funds");
                        Console.WriteLine(" (prese enter to continue)");
                        Console.ReadLine();
                    }
                }
            }
        }
        /*
         *  display balance of activeAccount and await keypress
         *  
         */
        private void dispBalance()
        {
            if (this.activeAccount != null)
            {
                Console.WriteLine(" your current balance is : " + activeAccount.getBalance());
                Console.WriteLine(" (prese enter to continue)");
                Console.ReadLine();
            }
        }
    }

    public class Bank
    {
        private Account[] ac = new Account[3];
        private Form1 atm;


        public Bank()
        {
            ac[0] = new Account(300, 1111, 111111);
            ac[1] = new Account(750, 2222, 222222);
            ac[2] = new Account(3000, 3333, 333333);


            // Application.Run(new Form1());
            Thread ATM2 = new Thread(new ThreadStart(ThreadProc));
            ATM2.Start();
            Thread ATM1 = new Thread(new ThreadStart(ThreadProc));
            ATM1.Start();

        }

        private static void Main(string[] args)
        {
            Bank test = new Bank();
            
        }

        private void ThreadProc()
        {
            var frm = new Form1(ac);
            frm.ShowDialog();
        }
    }
}








