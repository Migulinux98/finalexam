using System;
using System.Linq;
using System.Collections.Generic;



public class Program
{
	public static void Main()
	{
        string key = "1";
        
        do{
                Console.WriteLine("\nPress enter to continue. ");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("\n--------------------------------------------- ");
                Console.WriteLine("Select option: ");
                Console.WriteLine("              1 -> display all data ");
                
                Console.WriteLine("              0 -> exit \n");
                Console.Write(">>>>> ");
                key = Console.ReadLine();

                if (key == "1")
                {
                    
                    List<Temperature> temperatures = new List<Temperature>() { 
		                new Temperature() { Date = "12/03/2020", Temp = 23} ,
				        new Temperature() { Date = "29/05/2022", Temp = 29} ,
				        new Temperature() { Date = "18/11/2021", Temp = 18 } ,
                        new Temperature() { Date = "11/12/2020", Temp = 11 } 
				    };
				    
                    var   result1 = from s in temperatures
							  where s.Temp > 0
							  select s;
		
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("(Ex 2.) Temperatures per day:");	
                    int count=0;
                    foreach(Temperature te1 in result1){			
                        Console.WriteLine(te1.Date + " - " + te1.Temp);
                        count +=1;
		            }
		
		
		            Console.WriteLine("----------------------------------------");
		            var avgTemp = temperatures.Sum(s => s.Temp);	
		            Console.WriteLine("(Ex 3.) Average for all Temperatures is {0}. ", avgTemp /count);

                    Console.WriteLine("----------------------------------------");
                    	
                    string dateLowestTemp = "";
                    Double lowestCelsious = 0;
                    foreach(Temperature te1 in result1){			
                        
                        
                        if(lowestCelsious > te1.Temp)
                        {
                            lowestCelsious = te1.Temp;
                            dateLowestTemp = te1.Date;
                        }
                        else
                        {
                            lowestCelsious = te1.Temp;
                            dateLowestTemp = te1.Date;
                        }
		            }
                    Console.WriteLine("(Ex 4.) Lowest Temperature is {0} on {1} .", lowestCelsious, dateLowestTemp);
                }
               
                else if (key == "0")
                {
                    Console.WriteLine("\nExiting ...");
                }

        }while(key != "0");


	}
}

public class Temperature{

	public int ID { get; set; }
	public string Date { get; set; }
	public double Temp { get; set; }
	
	
}
