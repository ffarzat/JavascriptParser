using System;
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

            //for each instruction in tree (sometime including tree root node)
            if (_tree.IsNil)
            {
                for (int i = 0; i < _tree.ChildCount; i++)
                {
                    var instruction = _tree.GetChild(i);
                    sb.AppendLine(HandleChild(instruction));
                }
            }
            else
            {
                sb.AppendLine(HandleChild(_tree));
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

            if (instruction == null)
                return "";

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
                case 27:
                    instructionCode = HandleTypeOfInstruction(instruction);
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
                case 78:
                    instructionCode = Handle3EqualInstruction(instruction);
                    break;
                case 79:
                    instructionCode = Handle3EqualInstruction(instruction);
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
                case 96:
                    instructionCode = HandleQuestionInstruction(instruction);
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
                case 114:
                    instructionCode = HandleByFieldInstruction(instruction);
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
        /// Generates TypeOf code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleTypeOfInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = String.Format("{0} {1}", instruction.Text, HandleChild(instruction.GetChild(0)));

            return instructionCode; 
        }

        /// <summary>
        /// Generates === Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string Handle3EqualInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = String.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

            return instructionCode; 
        }

        /// <summary>
        /// Generates ? code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleQuestionInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = String.Format("{0} ? {1} : {2}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), HandleChild(instruction.GetChild(2)));

            return instructionCode;
        }

        /// <summary>
        /// Generates ByField code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleByFieldInstruction(ITree instruction)
        {
            return string.Format("{0}.{1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)));

        }

        /// <summary>
        /// Generates code for a Literal String
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleStringLiteral(ITree instruction)
        {

            string instructionCode = "";
            instructionCode = String.Format("{0}", instruction.Text);

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

            instructionCode = String.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

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

            instructionCode = String.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

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

            instructionCode = String.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

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

            instructionCode = String.Format("return {0}", HandleChild(instruction.GetChild(0)));

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
            
            instructionCode = String.Format("({0})", HandleChild(instruction.GetChild(0)));
            
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

            instructionCode = String.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

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

            instructionCode = String.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

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

            instructionCode = String.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

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

            instructionCode = String.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

            return instructionCode;
        }

        /// <summary>
        /// Generates Block Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleBlockInstruction(ITree instruction)
        {
            if(instruction == null)
                return "";

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


            if (block2Code != "")
            {
                if (block2.Type == 18) //else if
                    instructionCode = String.Format("if ({0}) {{\r\n  {1}\r\n  }}  else {2}", conditionCode, block1Code,
                                                    block2Code);
                else
                    instructionCode = String.Format("if ({0}) {{\r\n  {1}\r\n  }}  else  {{\r\n  {2}\r\n  }}",
                                                    conditionCode, block1Code, block2Code);
            }
            else
                instructionCode = String.Format("if ({0}) {{\r\n  {1}\r\n  }}", conditionCode, block1Code);

            return instructionCode;
        }

        /// <summary>
        /// Generates Call code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        /// <remarks>
        /// A Call node may contains a Function Name OR a Function Node
        /// </remarks>
        private string HandleCallInstruction(ITree instruction)
        {
            string instructionCode = "";

            if (instruction.GetChild(0).Text == "function")
            {
                #region When a call defines a function
                string functionCode = HandleFunctionInstruction(instruction.GetChild(0));
                var args = instruction.GetChild(1);

                string argsNames = "";
                if (args != null)
                {
                    for (int i = 0; i < args.ChildCount; i++)
                    {
                        argsNames += HandleChild(args.GetChild(i));

                        if (i < (args.ChildCount - 1))
                            argsNames += ", ";
                    }
                }

                instructionCode = String.Format("{0} ({1})", functionCode, argsNames); //rever essa exceção da função raiz... código vem antes!!!
                #endregion
            }
            else
            {
                #region When a single call of a named function
                string functionName = instruction.GetChild(0).Text;
                var args = instruction.GetChild(1);

                string argsNames = "";
                if (args != null)
                {
                    for (int i = 0; i < args.ChildCount; i++)
                    {
                        argsNames += HandleChild(args.GetChild(i));

                        if (i < (args.ChildCount - 1))
                            argsNames += ", ";
                    }
                }

                instructionCode = String.Format("{0}({1})", functionName, argsNames);
                #endregion
            }


            

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

            instructionCode = String.Format("{0} {2} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

            return instructionCode;
        }

        /// <summary>
        /// Generates function body code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        /// <remarks>
        /// Existem dois tipos de nó função: função na raiz e definição de função anonima
        /// </remarks>
        private string HandleFunctionInstruction(ITree instruction)
        {
            string instructionCode = "";

            if (instruction.GetChild(0).Text == "ARGS")
            {
                #region Function defination

                var args = instruction.GetChild(0);
                var block = instruction.GetChild(1);

                string argsNames = "";
                if (args != null)
                {
                    for (int i = 0; i < args.ChildCount; i++)
                    {
                        argsNames += HandleChild(args.GetChild(i));

                        if (i < (args.ChildCount - 1))
                            argsNames += ", ";
                    }
                }

                string blockCode = HandleBlockInstruction(block);

                instructionCode = String.Format("function ({0}) {{{1}}}", argsNames, blockCode);
                #endregion

            }
            else
            {
                #region Function Body inside a commom node
                string functionName = instruction.GetChild(0).Text;
                var args = instruction.GetChild(1);
                var block = instruction.GetChild(2);

                string argsNames = "";
                if (args != null)
                {
                    for (int i = 0; i < args.ChildCount; i++)
                    {
                        argsNames += args.GetChild(i).Text;

                        if (i < (args.ChildCount - 1))
                            argsNames += ", ";
                    }
                }

                string blockCode = HandleBlockInstruction(block);

                instructionCode = String.Format("function {0}({1}) {{\r\n{2}}}", functionName, argsNames, blockCode);
                #endregion
            }


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
                instructionCode = String.Format("{0} {1}", instruction.Text, HandleChild(instruction.GetChild(0)));
            }
            else
            {
                string vars = "var ";
                for (int i = 0; i < instruction.ChildCount; i++)
                {
                    vars += HandleChild(instruction.GetChild(i)) + (i == (instruction.ChildCount -1) ? "": ",") ;
                }

                instructionCode = vars;
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
            if (node == null) 
                return false;

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


        /// <summary>
        /// Finds TreeNode of functionName
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="nomeFuncaoOtimizar"></param>
        /// <returns></returns>
        public static ITree FindFunctionTree(CommonTree tree, string nomeFuncaoOtimizar)
        {

            for (int i = 0; i < tree.ChildCount; i++)
            {
                var funcaoAtual = tree.GetChild(i);
                var nome = funcaoAtual.GetChild(0) == null ? "" : funcaoAtual.GetChild(0).Text;

                if (nome == nomeFuncaoOtimizar)
                    return funcaoAtual;
            }

            return null;
        }

        #endregion
    }
}
