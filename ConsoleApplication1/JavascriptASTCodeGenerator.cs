﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime.Tree;

namespace ConsoleApplication1
{
    /// <summary>
    /// Represents a code Generator (from AST) for Javascript
    /// </summary>
    public class JavascriptAstCodeGenerator
    {
        /// <summary>
        /// Keeps the original tree
        /// </summary>
        private ITree _tree;

        /// <summary>
        /// Return the base tree of code transformation
        /// </summary>
        public ITree Tree {
            get { return _tree; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public JavascriptAstCodeGenerator(ITree tree)
        {
            _tree = tree;
        }

        /// <summary>
        /// Do the magic. Visits the AST nodes and translation back to code
        /// </summary>
        /// <returns></returns>
        public string DoCodeTransformation()
        {
            var sb = new StringBuilder();

            //for each instruction in tree
            for (int i = 0; i < _tree.ChildCount; i++)
            {
                var instruction = _tree.GetChild(i);
                sb.AppendLine(HandleChild(instruction));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Decides for each type of Node what to code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleChild(ITree instruction)
        {
            string instructionCode = "";

            switch (instruction.Type)
            {
                case 17: 
                    instructionCode = HandleFunctionInstruction(instruction);
                    break;
                case 18:
                    instructionCode = HandleIfInstruction(instruction);
                    break;
                case 22:
                    instructionCode = HandleReturnInstruction(instruction);
                    break;
                case 28: 
                    instructionCode = HandleVarInstruction(instruction);
                    break;
                case 74:
                    instructionCode = HandleLessEqualInstruction(instruction);
                    break;
                case 76:
                    instructionCode = HandleEqualInstruction(instruction);
                    break;
                case 80:
                    instructionCode = HandleSumInstruction(instruction);
                    break;
                case 94:
                    instructionCode = HandleAndInstruction(instruction);
                    break;
                case 95:
                    instructionCode = HandleOrInstruction(instruction);
                    break;
                case 98:
                    instructionCode = HandleSetInstruction(instruction);
                    break;
                case 83:
                    instructionCode = HandleModInstruction(instruction);
                    break;
                case 109:
                    instructionCode = HandleDivideInstruction(instruction);
                    break;
                case 113:
                    instructionCode = HandleBlockInstruction(instruction);
                    break;
                case 116:
                    instructionCode = HandleCallInstruction(instruction);
                    break;
                case 126:
                    instructionCode = HandleParamExprInstruction(instruction);
                    break;
                case 149:
                    instructionCode = HandleStringLiteral(instruction);
                    break;
                default:
                    instructionCode = instruction.Text;
                    break;
            }

            return instructionCode;
        }

        /// <summary>
        /// Generates code for a Literal String
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleStringLiteral(ITree instruction)
        {

            string instructionCode = "";
            instructionCode = string.Format("{0}", instruction.Text);

            if (instruction.Parent.Type == 0)
                instructionCode += ";"; // use strict exception! oO

            return instructionCode; 
            
        }

        /// <summary>
        /// Generates && Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleAndInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = string.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

            return instructionCode; 
        }

        /// <summary>
        /// Generates Equal Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleEqualInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = string.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

            return instructionCode; 
        }

        /// <summary>
        /// Generates OR code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleOrInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = string.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

            return instructionCode; 
        }

        /// <summary>
        /// Generates Return Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleReturnInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = string.Format("return {0}", HandleChild(instruction.GetChild(0)));

            return instructionCode;
        }

        /// <summary>
        /// Generates (itens) code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleParamExprInstruction(ITree instruction)
        {
            string instructionCode = "";
            
            instructionCode = string.Format("({0})", HandleChild(instruction.GetChild(0)));
            
            return instructionCode;
        }

        /// <summary>
        /// Generates Mod Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleModInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = string.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

            return instructionCode;
        }

        /// <summary>
        /// Generates Divide Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleDivideInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = string.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

            return instructionCode;
        }

        /// <summary>
        /// Generates LessOrEqual Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleLessEqualInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = string.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

            return instructionCode;
        }

        /// <summary>
        /// Generates Add code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleSumInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = string.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

            return instructionCode;
        }

        /// <summary>
        /// Generates Block Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleBlockInstruction(ITree instruction)
        {
            string blockCode = "";
            
            for (int i = 0; i < instruction.ChildCount; i++)
            {
                var actualInstruction = instruction.GetChild(i);

                blockCode += "  " + HandleChild(actualInstruction);
                blockCode += actualInstruction.Type == 18 ? "" : ";"; //se for uma condição em if não colocar ';'
                blockCode += Environment.NewLine;
            }
            
            return blockCode;
        }

        /// <summary>
        /// Generates IF code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleIfInstruction(ITree instruction)
        {
            string instructionCode = "";
            var condition = instruction.GetChild(0);
            var block1 = instruction.GetChild(1);
            var block2 = instruction.GetChild(2);

            string conditionCode = "";
            string block1Code = "";
            string block2Code = "";

            if (IsFunction(condition))
                conditionCode = HandleChild(condition);

            if (IsFunction(block1))
                block1Code = HandleChild(block1);

            if (IsFunction(block2))
                block2Code = HandleChild(block2);
            

            if(block2Code != "")
                if (block2.Type == 18) //else if
                    instructionCode = string.Format("if ({0}) {{\r\n  {1}\r\n  }}  else {2}", conditionCode, block1Code, block2Code);
                else
                    instructionCode = string.Format("if ({0}) {{\r\n  {1}\r\n  }}  else  {{\r\n  {2}\r\n  }}", conditionCode, block1Code, block2Code);
                
            else
                instructionCode = string.Format("if ({0}) {{\r\n  {1}\r\n  }}", conditionCode, block1Code); 

            return instructionCode;
        }

        /// <summary>
        /// Generates Call code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleCallInstruction(ITree instruction)
        {
            string instructionCode = "";
            string functionName = instruction.GetChild(0).Text;
            var args = instruction.GetChild(1);

            string argsNames = "";
            for (int i = 0; i < args.ChildCount; i++)
            {


                if (IsFunction(args.GetChild(i)))
                    argsNames += HandleChild(args.GetChild(i));
                else
                    argsNames += args.GetChild(i).Text;


                if (i < (args.ChildCount - 1))
                    argsNames += ", ";
            }

            instructionCode = string.Format("{0}({1})", functionName, argsNames);

            return instructionCode;
        }

        /// <summary>
        /// Generates Set code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleSetInstruction(ITree instruction)
        {
            string instructionCode = "";

            if (IsFunction(instruction.GetChild(0)))
                instructionCode = string.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), instruction.GetChild(1), instruction.Text);

            if (IsFunction(instruction.GetChild(1)))
                instructionCode = string.Format("{0} {2} {1}", instruction.GetChild(0), HandleChild(instruction.GetChild(1)), instruction.Text);

            else
                instructionCode = string.Format("{0} {2} {1}", instruction.GetChild(0), instruction.GetChild(1), instruction.Text);
            
            return instructionCode;
        }

        /// <summary>
        /// Generates function body code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleFunctionInstruction(ITree instruction)
        {
            string instructionCode = "";
            
            string functionName = instruction.GetChild(0).Text;
            var args = instruction.GetChild(1);
            var block = instruction.GetChild(2);

            string argsNames = "";
            for (int i = 0; i < args.ChildCount; i++)
            {
                argsNames += args.GetChild(i).Text;
                
                if (i < (args.ChildCount -1))
                    argsNames += ", ";
            }

            string blockCode = HandleBlockInstruction(block);

            instructionCode = string.Format("function {0}({1}) {{\r\n{2}}}", functionName, argsNames, blockCode);

            return instructionCode;
        }

        /// <summary>
        /// Generate var declaration code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleVarInstruction(ITree instruction)
        {
            string instructionCode = "";

            if (instruction.ChildCount==1)
            {
                instructionCode = string.Format("{0} {1}", instruction.Text, HandleChild(instruction.GetChild(0)));
            }
            else
            {
                //TODO: forcei aqui sabendo que só tem essas duas opções no código alvo
                instructionCode = string.Format("{0} {1}, {2}, {3}", instruction.Text, instruction.GetChild(0).Text, instruction.GetChild(1).Text, instruction.GetChild(2).Text);
            }
            
            return instructionCode;
        }

        #region Facilities 
        /// <summary>
        /// Determine if the instruction is a function node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool IsFunction(ITree node)
        {
            switch (node.Type)
            {
                case 113: //Block
                    return true;
                    break;
                case 116: //Call
                    return true;
                    break;
                case 126: //PAREXPR = {}
                    return true;
                    break;
                default: // Restante
                    if (node.Type >= 7)
                    {
                        if (node.Type <= 110)
                        {
                            return true;
                        }
                    }
                    break;

            }
            return false;
        }

        /// <summary>
        /// Determine if the instruction is a function node
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsFunction(int number)
        {
            switch (number)
            {
                case 113: //Block
                    return true;
                    break;
                case 116: //Call
                    return true;
                    break;
                case 126: //PAREXPR = {}
                    return true;
                    break;
                default: // Restante
                    if (number >= 7)
                    {
                        if (number <= 110)
                        {
                            return true;
                        }
                    }
                    break;

            }
            return false;
        }

        #endregion
    }
}
