using System;
using System.Collections.Generic;

namespace Q1
{
    public class transition
    {
        public string start ;
        public string end;
        public string edge;

        public transition(string s,string ed , string e)
        {
            start = s;
            edge = ed ;
            end = e;
        }
    }
    class Program
    {
        public static bool Solve(string[] states , string[] alphabets, string[] finalstates, List<transition> transitions , string text)
        {
            List<string> newList = new List<string>();
            for (int i = 0; i < text.Length; i++)
            {
                if(newList.Count!=0)
                {
                    List<string> newList2 = new List<string>();
                    foreach (var item in newList)
                    {
                        newList2.Add(item);
                    }
                    newList = new List<string>();
                    foreach (var item in newList2)
                    {
                        string state = item;
                        List<string> lambda = Lambdatransitions(state,transitions);
                        List<string> ends = new List<string>();
                        for (int j = 0; j < lambda.Count; j++)
                        {
                            for (int k = 0; k < transitions.Count; k++)
                            {
                                if(transitions[k].start == lambda[j] && transitions[k].edge == text[i].ToString())
                                    ends.Add(transitions[k].end);
                            }
                        }
                        int l = 0;
                        while(l < ends.Count)
                        {
                            List<string> elambda = new List<string>();
                            elambda = Lambdatransitions(ends[l],transitions);
                            for (int m = 0; m < elambda.Count; m++)
                            {
                                if(!ends.Contains(elambda[m]))
                                    ends.Add(elambda[m]);
                            }
                            l++;
                        }
                        for (int q = 0; q < ends.Count; q++)
                        {
                            newList.Add(ends[q]);
                        }
                        if(i==text.Length-1)
                        {
                            for (int o = 0; o < ends.Count; o++)
                            {
                                for (int p = 0; p < finalstates.Length; p++)
                                {
                                    if(ends[o]==finalstates[p])
                                        return true;
                                }
                            }
                        }
                        
                    }
                }
                else
                {
                    string state = states[i];
                    List<string> lambda = Lambdatransitions(state,transitions);
                    List<string> ends = new List<string>();
                    for (int j = 0; j < lambda.Count; j++)
                    {
                        for (int k = 0; k < transitions.Count; k++)
                        {
                            if(transitions[k].start == lambda[j] && transitions[k].edge == text[i].ToString())
                                ends.Add(transitions[k].end);
                        }
                    }
                    int l = 0;
                    while(l < ends.Count)
                    {
                        List<string> elambda = new List<string>();
                        elambda = Lambdatransitions(ends[l],transitions);
                        for (int m = 0; m < elambda.Count; m++)
                        {
                            if(!ends.Contains(elambda[m]))
                                ends.Add(elambda[m]);
                        }
                        l++;
                    }
                    for (int q = 0; q < ends.Count; q++)
                    {
                        newList.Add(ends[q]);
                    }
                    if(i==text.Length-1)
                    {
                        for (int o = 0; o < ends.Count; o++)
                        {
                            for (int p = 0; p < finalstates.Length; p++)
                            {
                                if(ends[o]==finalstates[p])
                                    return true;
                            }
                        }
                    }
                }
            }
            return false;

        }
        public static List<string> Lambdatransitions(string state,List<transition> transitions)
        {
            List<string> result = new List<string>();
            result.Add(state);
            int idx = 0;
            while(idx != result.Count)
            {
                for (int i = 0; i < transitions.Count; i++)
                {
                    if(transitions[i].start == state && transitions[i].edge == '$'.ToString() && !result.Contains(transitions[i].end))
                        result.Add(transitions[i].end);
                }
                idx++;
            }
            return result;
        }
        static void Main(string[] args)
        {
            string[] states = Console.ReadLine().Split(new char[]{'{','}',','},StringSplitOptions.RemoveEmptyEntries);
            string[] alphabets = Console.ReadLine().Split(new char[]{'{','}',','},StringSplitOptions.RemoveEmptyEntries);
            string[] finalstates = Console.ReadLine().Split(new char[]{'{','}',','},StringSplitOptions.RemoveEmptyEntries);
            long count = long.Parse(Console.ReadLine());
            List<transition> transitions = new List<transition>();
            string[] ss ;
            for (int i = 0; i < count; i++)
            {
                ss = Console.ReadLine().Split('{','}',',');
                transitions.Add(new transition(ss[0],ss[1],ss[2]));
            }
            string text = Console.ReadLine();
            bool answer = Solve(states,alphabets,finalstates,transitions,text);
            if(answer)
                System.Console.WriteLine("Accepted");
            else
                System.Console.WriteLine("Rejected");
        }
    }
}
