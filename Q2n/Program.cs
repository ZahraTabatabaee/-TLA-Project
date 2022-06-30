using System;
using System.Collections.Generic;

namespace Q2n
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
            List<List<string>> result = new List<List<string>>();
            List<string> state = new List<string>();
            state.Add(states[0]);
            result.Add(Lambdatransitions(state,transitions));
            int i = 0;
            while(i < result.Count)
            {
                for (int j = 0; j < alphabets.Length; j++)
                {
                    List<string> m = Transitions(result[i],transitions,alphabets[j]);
                    List<string> n = Lambdatransitions(m,transitions);
                    List<bool> b = new List<bool>();
                    if(!equal(result,n))
                        result.Add(n);
                }
                i++;
            }
            return result.Count;
        }
        public static bool equal(List<List<string>> LL , List<string> s)
        {
            s.Sort();
            for (int i = 0; i < LL.Count; i++)
            {
                LL[i].Sort();
                if(LL.Count != s.Count)
                    return false;
                for (int j = 0; j < s.Count; j++)
                {
                    if(LL[j].Equals(s[j]))
                        return false;
                }
            }
            return true;
        }
        public static List<string> Lambdatransitions(List<string> state,List<transition> transitions)
        {
            List<string> answer = new List<string>();
            for (int j = 0; j < state.Count; j++)
            {
                List<string> result = new List<string>();
                result.Add(state[j]);
                int idx = 0;
                while(idx < result.Count)
                {
                    for (int i = 0; i < transitions.Count; i++)
                    {
                        if(transitions[i].start == result[idx] && transitions[i].edge == '$'.ToString() && !result.Contains(transitions[i].end))
                            result.Add(transitions[i].end);
                    }
                    idx++;
                }
                for (int k = 0; k < result.Count; k++)
                {
                    if(!answer.Contains(result[k]))
                        answer.Add(result[k]);
                }
            }
            return answer;
        }
        public static List<string> Transitions(List<string> state,List<transition> t , string s)
        {
            List<string> result = new List<string>();
            for (int j = 0; j < state.Count; j++)
            {
                for (int i = 0; i < t.Count; i++)
                {
                    if(t[i].start == state[j] && t[i].edge == s && !result.Contains(t[i].end))
                        result.Add(t[i].end);
                }
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
            System.Console.WriteLine(Solve(states,alphabets,finalstates,transitions));
        }
    }
}
