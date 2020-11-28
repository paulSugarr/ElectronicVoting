using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using ElectronicVoting.Extensions;

namespace ElectronicVoting.Cryptography
{
    public static class KeyCreator
    {
        public static Dictionary<string, object> CreatePrivateKey()
        {
            var p = BigInteger.Parse("466926069311667573442943231337990856457836882696009733480626520610643914123591144231545800128291622468045661765196316980106381681885717860060828762156366012359085927473094603249310200870245784787730272848859122946679280394247934619420896730259788721548420173134462241007859973428827445679782734098961");
            var q = BigInteger.Parse("691607713810169858258031256895764209850027451006111527126799805252399255828996966285360683287818818236891479879053535861521850297794342182373526694202917569117881514831458796948615835211811399214450600379872007095213202740723661925283373223792993196646544740082326176410886072445419402234731873379639");
            var n = q * p;
            var phi = (p - 1) * (q - 1);
            var e = Gcd(p, q, out _, out _);
            Gcd(e, phi, out var x, out var y);

            var d = x > 0 ? x : x + phi;
            
            // Console.WriteLine($"phi = {phi}");
            // Console.WriteLine($"x = {x}");
            // Console.WriteLine($"y = {y}");
            // Console.WriteLine($"d = {d}");


            var result = new Dictionary<string, object>();
            result.Add("p", p.ToString());
            result.Add("q", q.ToString());
            result.Add("n", n.ToString());
            result.Add("phi", phi.ToString());
            result.Add("e", e.ToString());
            result.Add("d", d.ToString());
            return result;
        }
        public static Dictionary<string, object> CreatePublicKey(Dictionary<string, object> privateKey)
        {
            var result = new Dictionary<string, object>();
            result.Add("n", privateKey["n"]);
            result.Add("e", privateKey["e"]);
            return result;
        }
        private static BigInteger Gcd(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (b < a)
            {
                var t = a;
                a = b;
                b = t;
            }
    
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
 
            var gcd = Gcd(b % a, a, out x, out y);
    
            var newY = x;
            var newX = y - (b / a) * x;
    
            x = newX;
            y = newY;
            return gcd;
        }
    }
}