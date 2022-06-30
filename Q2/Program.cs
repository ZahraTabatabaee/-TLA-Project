using System;
using System.Linq;
using System.Collections.Generic;

namespace Q2
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
        public static int Solve(string[] states , string[] alphabets, string[] finalstates, List<transition> transitions)
        {
            List<string> state = new List<string>{states[0]};
            List<string> ans = Lambdatransitions(state.ToArray(),transitions);
            List<List<string>> MyList = new List<List<string>>{ans};
            List<string> strlist = new List<string>(){listtostr(ans)};
            int i = 0;
            int count = 1 ;
            while(i < MyList.Count)
            {
                for (int j = 0; j < alphabets.Length; j++)
                {
                    List<string> trans = Transitions(MyList[i].ToArray(),transitions,alphabets[j]);
                    List<string> lambdatrans = Lambdatransitions(trans.ToArray(),transitions);
                    List<bool> b = new List<bool>();
                    if(!compare(MyList,lambdatrans))
                    {
                        MyList.Add(lambdatrans);
                        count++;
                    }
                }
                i++;
            }
            return count;
        }
        public static string listtostr(List<string> s)
        {
            string ss = "";
            ss = string.Join(",",s.ToArray());
            return ss;
        }
        public static bool compare(List<List<string>> LL , List<string> L)
        {
            for (int i = 0; i < LL.Count; i++)
            {
                if(LL[i].Count == L.Count)
                {
                LL[i] = LL[i].OrderBy(q => q).ToList();
                L = L.OrderBy(q => q).ToList();
                if(LL[i].SequenceEqual(L)) return true;
                }
            }
            return false;
        }
        public static List<string> Lambdatransitions(string[] state,List<transition> transitions)
        {
            List<string> nextstate = new List<string>();
            for (int j = 0; j < state.Length; j++)
            {
                List<string> MyList = new List<string>();
                MyList.Add(state[j]);
                int idx = 0;
                while(idx < MyList.Count)
                {
                    for (int i = 0; i < transitions.Count; i++)
                    {
                        if(transitions[i].start == MyList[idx] && transitions[i].edge == '$'.ToString() && !MyList.Contains(transitions[i].end))
                            MyList.Add(transitions[i].end);
                    }
                    idx++;
                }
                for (int k = 0; k < MyList.Count; k++)
                {
                    if(!nextstate.Contains(MyList[k]))
                        nextstate.Add(MyList[k]);
                }
            }
            return nextstate;
        }
        public static List<string> Transitions(string[] state,List<transition> t , string s)
        {
            List<string> nextstate = new List<string>();
            int idx = 0;
            while(idx != state.Length)
            {
                for (int i = 0; i < t.Count; i++)
                {
                    if(t[i].start == state[idx] && t[i].edge == s.ToString() && !nextstate.Contains(t[i].end))
                        nextstate.Add(t[i].end);
                }
                idx++;
            }
            return nextstate;
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
            System.Console.WriteLine(Solve(states,alphabets,finalstates,transitions));
        }
    }
}
