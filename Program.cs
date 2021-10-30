using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{

    class route
    {
        public string name { get; set; }
        public Dictionary<int, stop> scheduleWithTimeAndStop { get; set; }
        public route(string name, Dictionary<int, stop> scheduleWithTimeAndStop)
        {
            this.name = name;
            this.scheduleWithTimeAndStop = scheduleWithTimeAndStop;
        }

    }
    class stop
    {
        public string code { get; set; }
        public List<bus> allBuses { get; set; }
        public stop(List<bus> buses, string code)
        {
            this.allBuses = buses;
            this.code = code;
        }
        public String ToString()
        {
            return code+" the bus number "+ allBuses[0].rego;
        }

    }
    class bus
    {
        public string rego { get; set; }
        //public List<string> Allschedule { get; set; }
        public Dictionary<int, stop> scheduleWithTimeAndStop;
        public stop CurrentStop { get; set; }

        public bus(Dictionary<int, stop> scheduleWithTimeAndStop, string rego, stop CurrentStop)
        {
            this.scheduleWithTimeAndStop = scheduleWithTimeAndStop;
            this.rego = rego;
            this.CurrentStop = CurrentStop;
        }



    }

    class line
    {

        public line(bus theBus, route theRoute) {
            runRoute(theBus, theRoute);
        }

        public void runRoute(bus theBus, route theRoute)
        {

            for (int ii = 0; ii < 3; ii++)
            {

                for (int i = 0; i < 24; i++)
                {
                    for (int index = 0; index < theRoute.scheduleWithTimeAndStop.Count; index++)
                    {
                        var item = theRoute.scheduleWithTimeAndStop.ElementAt(index);
                        var itemValue = item.Value;


                        if (theBus.scheduleWithTimeAndStop.ContainsKey(i) == true)
                        {

                            if (theBus.scheduleWithTimeAndStop[i] == itemValue)
                            {
                                //remove bus from last stop
                                for (int index2 = 0; index < theRoute.scheduleWithTimeAndStop.Count; index++)
                                {
                                    var item2 = theRoute.scheduleWithTimeAndStop.ElementAt(index2);
                                    var itemValue2 = item2.Value;
                                    if (itemValue2.allBuses.Contains(theBus))
                                    {
                                        itemValue2.allBuses.Remove(theBus);
                                    }
                                }
                                    itemValue.allBuses.Add(theBus);



                                if (theBus.CurrentStop != null)
                                {
                                    theBus.CurrentStop.allBuses.Remove(theBus);
                                }
                                theBus.CurrentStop = itemValue;
                            }
                        }

                    }
                    if (theBus.CurrentStop != null)
                    {
                        Console.WriteLine("theBus CurrentStop " + theBus.CurrentStop.ToString() + " currentTime" + i);
                    }
                }
                if (ii == 0)
                {
                    //remove a stop
                    theRoute.scheduleWithTimeAndStop.Remove(11);
                }
            }

        }



    }






    class program
    {
    static void Main(string[] args)
    {


            stop thestop1 = new stop(new List<bus>(), "stop1");
            stop thestop2 = new stop(new List<bus>(), "stop2");
            stop thestop3 = new stop(new List<bus>(), "stop3");


            Dictionary<int, stop> scheduleWithTimeAndStop = new Dictionary<int, stop> { { 10, thestop1 }, { 11, thestop2 }, { 12, thestop3 } };

            bus theBus = new bus(scheduleWithTimeAndStop, "rx123", null);


            route theRoute = new route("forward heaven", scheduleWithTimeAndStop);


            new line(theBus, theRoute);

            Console.WriteLine("-----------------------------------------------------------------------------------------");


            Dictionary<int, stop> scheduleWithTimeAndStop2 = new Dictionary<int, stop> { { 10, thestop3 }, { 11, thestop2 }, { 12, thestop1 } };

            bus theBus2 = new bus(scheduleWithTimeAndStop2, "rx321", null);


            route theRoute2 = new route("backward heaven", scheduleWithTimeAndStop2);

            new line(theBus2, theRoute2);



        }

    }


    }































