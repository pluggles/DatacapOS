﻿//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Management.Instrumentation;
//using System.Text;
//using System.Text.RegularExpressions;
//using SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes;
//using SSSGroup.Datacap.CustomActions.FormulaProcessor.Resources;
//
//namespace SSSGroup.Datacap.CustomActions.FormulaProcessor
//{
//    public class Parser
//    {
//        private readonly string _currentID = Templates.IdSymbols; // The last formed value of the variable identifier
//
//        private int _idNumber; // The last number of the variable identifier  
//
//        #region Strings
//        //private string _inParentheses = Templates.Empty;
//        private string _value = Templates.Empty;
//        private List<string> _arithmetic;
//        private List<string> _logical;
//        private List<string> _operations;
//        private List<string> _arichmeticL;
//        private List<string> _arichmeticR;
//        private readonly Dictionary<string, Func<string, string>> _fList;
//        private Stack<string> _data = new Stack<string>();
//        #endregion
//        private string _mainFormula;
//        public string MainFormula // The character positioning data in the search string 
//        {
//            get { return _mainFormula; }
//            set { _mainFormula = value; Parse();}
//        }
//
//        public Parser(Dictionary<string, Func<string, string>> fList = null)
//        {
//            _fList = fList;
//        }
//        /// <summary>
//        ///     Constructor of Parser Class
//        /// </summary>
//        private void Parse()
//        {
//            if (string.IsNullOrEmpty(_mainFormula))
//                return;
//            _iDic.Clear();
//            _arithmetic = new List<string>(Templates.ArithmeticOperations.Split(','));
//            _logical = new List<string>(Templates.LogicalOperators.Split(','));
//            var specialBegin = Templates.Prefix.Split(',');
//            var specialEnd = Templates.Suffix.Split(',');
//
//            _operations = new List<string>(_arithmetic);
//            //     _operations.AddRange(_arithmetic);
//            _operations.AddRange(_logical);
//            _operations.AddRange(specialBegin);
//
//            _arichmeticL = new List<string>(_arithmetic);
//            _arichmeticL.AddRange(specialBegin);
//            _arichmeticR = new List<string>(_arithmetic);
//            _arichmeticR.AddRange(specialEnd);
//
//            FormulaReduce();
//            LegalTwinsCorrection();
//        }
//
//        /// <summary>
//        ///     Translation of the formula into an internal identification format
//        /// </summary>
//        public void FormulaReduce()
//        {
//            formulaReduce(ref _mainFormula);
//        }
//
//        private void formulaReduce(ref string formula)
//        {
//            var r = new Regex(Templates.RegexParentMatch, RegexOptions.IgnorePatternWhitespace);
//            //var query = @"funcPow((3),2) * (9+1)";
//            var matches = r.Matches(formula);
//            foreach (string match in matches)
//            {
//                _data.Push(match);
//            }
//            SubstituteOperands(ref formula);
//        }
//
//        private void SubstituteOperands(ref string formula)
//        {
//            var pattern = Templates.RegexFormulaMatch;
//            var signs = Regex.Matches(formula, pattern);
//            var values = Regex.Split(formula, pattern);
//            var i = 0;
//            var sb = new StringBuilder();
//            foreach (var s in values)
//            {
//                var sO = SetCurrentID(); //Change the currently number
//                _iDic.Add(sO, s); // Add to coolection
//                sb.Append(sO);
//                if (i < signs.Count)
//                    sb.Append(signs[i].ToString().Trim());
//                i++;
//            }
//            formula = sb.ToString();
//        }
//
//        private int SubstituteOperand(ref string formula, int start,int end)
//        {
//            var s = _mainFormula.Substring(start, end-start).Trim();
//            var sO = SetCurrentID(); //Change the currently number
//            _iDic.Add(sO, s); // Add to coolection
//            var difference = formula.Length;
//            formula = formula.Remove(start, end-start);
//            formula = formula.Insert(start, sO);
//            difference = difference - formula.Length;
//            return difference;
//        }
//
//        /// <summary>
//        ///     The main program module for syntactic parsing of the formula as a substrings combination
//        /// </summary>
//        /// <returns></returns>
//        public string SubstringPush(string newFormula = "")
//        {
//           /* if (!string.IsNullOrEmpty(newFormula))
//            {
//                _mainFormula = newFormula;
//                Parse();
//            }
//            var i = 0;
//            while (i < _mainFormula.Length)
//            {
//                if (_mainFormula[i] == Templates.Separators[0]) _parenthesOpens.Push(i);
//                if (_mainFormula[i] == Templates.Separators[1])
//                {
//                    try
//                    {
//                        _subStart = _parenthesOpens.Pop();
//                    }
//                    catch (InvalidOperationException)
//                    {
//                        throw new FTException(ErrorEx.Empty);
//                    }
//                    _next = i;
//                    _value = ParenthesesAnalisys(_mainFormula.Substring(_subStart, _next - _subStart + 1));
//                    _mainFormula = _mainFormula.Remove(_subStart, _next - _subStart + 1);
//                    _mainFormula = _mainFormula.Insert(_subStart, _value);
//                    i = _subStart;
//                }
//                i = i + 1;
//            }
//            if (_parenthesOpens.Count > 0)
//            {
//                throw new FTException(ErrorEx.Balance);
//            }
//            return _iDic[_mainFormula];*/
//            return "";
//        }
//
//        /// <summary>
//        ///     Analysis of the contents in brackets
//        /// </summary>
//        /// <param name="s"></param>
//        /// <returns></returns>
//        public string ParenthesesAnalisys(string s)
//        {
//            // It is a Function
//            /*if ((0 >= _subStart) || OperatorPresent(_subStart - 1, _operations, _mainFormula))
//                return OperatorContain(s, _logical) ? LogicalResult(s) : ArithmeticResult(s);
//            if (OperatorPosition(_subStart - 1, _operations, _mainFormula) < 0)
//                return OperatorContain(s, _logical) ? LogicalResult(s) : ArithmeticResult(s);
//            var n = OperatorPosition(_subStart - 1, _operations, _mainFormula);
//            var ss = _mainFormula.Substring(n + 1, _subStart - n - 1);
//            _subStart = n + 1;
//            return FunctionResult(ss, s);*/
//            return "";
//            //It is not Function
//        }
//
//        // Dummy
//        public string FunctionResult(string functionID, string functionArguments)
//        {
//            return "9";
//        }
//
//        /// <summary>
//        ///     The result of an arithmetic operation
//        /// </summary>
//        /// <param name="operations"></param>
//        /// <returns></returns>
//        public string ArithmeticResult(string operations)
//        {//TODO: reveiew/research on how to simplify
//            while (OperatorContain(operations, _arithmetic))
//            {
//                // ReSharper disable once AccessToModifiedClosure
//                foreach (var op in _arithmetic.Where(op => operations.Contains(op)))
//                {
//                    var i = -1;
//                    while (i < operations.Length)
//                    {
//                        i++;
//                        var ind = operations.IndexOf(op, StringComparison.Ordinal);
//                        if (ind <= -1) continue;
//                        var first = OperatorPosition(ind - 1, _arichmeticL, operations);
//                        var last = OperatorPosition(ind + 1, _arichmeticR, operations, 1);
//                        var left = operations.Substring(first + 1, ind - first - 1);
//                        var right = operations.Substring(ind + 1, last - ind - 1);
//                        var rez = BaseMath.DoMath(_iDic[left], op, _iDic[right]);
//                        var sO = SetCurrentID();
//                        //_iDic.Add(sO, rez);
//                        operations = operations.Remove(first + 1, last - first - 1);
//                        operations = operations.Insert(first + 1, sO);
//                        i = first;
//                        if (!OperatorContain(operations, _arithmetic)) return sO;
//                    }
//                }
//            }
//            return operations.Substring(1, operations.Length - 2);
//        }
//
//        // Dummy
//        public string LogicalResult(string operations)
//        {
//            return "666";
//        }
//
//        #region Collections
//
//        private readonly Dictionary<string, string> _iDic = new Dictionary<string, string>();
//            // Identifiers and values of the variables in formula    
//
//        private readonly Stack<int> _parenthesOpens = new Stack<int>(0);
//            // The current balance of parentheses in the stack
//
//        #endregion        
//
//        #region Twins&Functions
//
//        private readonly string[,] _lTwins =
//        {
//            {"(+", "("}, {"(-", "(0-"}, {"*+", "*"}, {"*-", "*(-1)*"}, {"^+", "^"},
//            {"++", "+"}, {"-+", "-"}, {"+-", "-"}, {"()", ""}, 
//            {"=<", "<="},{"=>", ">="},{"><", "<>"}
//        };
//
//        //private readonly string[] _functionsID = {"sqrt", "ln", "exp", "abs", "sin", "cos", "asin", "acos"};
//        
//
//        #endregion
//
//        #region Service
//
//        public bool OperatorPresent(int firstNumber, List<string> oper, string formulastring)
//        {
//            return (from s in oper let sO = firstNumber - s.Length + 1 >= 0 ? formulastring.Substring(firstNumber - s.Length + 1, s.Length) : Templates.Empty where 0 == string.CompareOrdinal(sO, s) select s).Any();
//        }
//
//        public bool OperatorContain(string s, List<string> oper)
//        {
//            return oper.Any(sO => -1 < s.IndexOf(sO, StringComparison.Ordinal));
//        }
//
//        public int OperatorPosition(int firstNumber, List<string> oper, string formulastring, int dn = -1)
//        {
//            var n = firstNumber;
//            while ((n >= 0) && (n < formulastring.Length))
//            {
//                if (OperatorPresent(n, oper, formulastring)) return n;
//                n = n + dn;
//            }
//            return -1;
//        }
//
//        public string SetCurrentID()
//        {
//            _idNumber++;
//            return _currentID + Convert.ToString(_idNumber);
//        }
//
//        public void LegalTwinsCorrection()
//        {
//            /*if (_mainFormula.StartsWith(Templates.Separators[0].ToString()) &&
//                _mainFormula.StartsWith(Templates.Separators[0].ToString())) return;
//            _mainFormula = Templates.Separators[0] + _mainFormula + Templates.Separators[1];
//            _iDic.Add("0", "0");
//            _iDic.Add("-1", "-1");
//            for (var i = 0; i < _lTwins.Length/2; i++)
//            {
//                _mainFormula = _mainFormula.Replace(_lTwins[i, 0], _lTwins[i, 1]);
//            }
//            foreach (var t in _functionsID)
//            {
//                try
//                {
//                    _iDic.Add(t, "1");
//                }
//                catch (ArgumentException)
//                {
//                    throw new FTException(ErrorEx.IllegalFunctions);
//                }
//            }*/
//        }
//        #endregion
//    }
//}