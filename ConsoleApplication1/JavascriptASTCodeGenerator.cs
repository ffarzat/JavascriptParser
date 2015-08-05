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
        /// List for all functions body
        /// </summary>
        private static List<ITree> _functions = new List<ITree>();

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
        /// List for all functions body
        /// </summary>
        public static List<ITree> Functions
        {
            get { return _functions; }
            set { _functions = value; }
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
                #region Debug
                //var function = FindFunctionTree(_tree as CommonTree, "bs__translate");
                //sb.AppendLine(HandleChild(function) + ";");
                #endregion

                sb.AppendLine(HandleChild(_tree) + ";");
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
                case 9:
                    instructionCode = HandleCatchInstruction(instruction);
                    break;
                case 12:
                    instructionCode = HandleDeleteInstruction(instruction);
                    break;
                case 16:
                    instructionCode = HandleForInstruction(instruction);
                    break;
                case 17: 
                    instructionCode = HandleFunctionInstruction(instruction);
                    break;
                case 18:
                    instructionCode = HandleIfInstruction(instruction);
                    break;
                case 19:
                    instructionCode = HandleInInstruction(instruction);
                    break;
                case 20:
                    instructionCode = HandleInstanceOfInstruction(instruction);
                    break;
                case 21:
                    instructionCode = HandleNewInstruction(instruction);
                    break;
                case 22:
                    instructionCode = HandleReturnInstruction(instruction);
                    break;
                case 23:
                    instructionCode = HandleSwitchInstruction(instruction);
                    break;
                case 25:
                    instructionCode = HandleNewInstruction(instruction);
                    break;
                case 26:
                    instructionCode = HandleTryInstruction(instruction);
                    break;
                case 27:
                    instructionCode = HandleTypeOfInstruction(instruction);
                    break;
                case 28: 
                    instructionCode = HandleVarInstruction(instruction);
                    break;
                case 29:
                    instructionCode = HandleVarInstruction(instruction);
                    break;
                case 30:
                    instructionCode = HandleWhileInstruction(instruction);
                    break;
                case 72:
                    instructionCode = HandleLessEqualInstruction(instruction);
                    break;
                case 73:
                    instructionCode = HandleLessEqualInstruction(instruction);
                    break;
                case 74:
                    instructionCode = HandleLessEqualInstruction(instruction);
                    break;
                case 75:
                    instructionCode = HandleEqualInstruction(instruction);
                    break;
                case 76:
                    instructionCode = HandleEqualInstruction(instruction);
                    break;
                case 77:
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
                case 81:
                    instructionCode = HandleSumInstruction(instruction);
                    break;
                case 82:
                    instructionCode = HandleMultiplyInstruction(instruction);
                    break;
                case 83:
                    instructionCode = HandleModInstruction(instruction);
                    break;
                case 84:
                    instructionCode = HandlePlusPlusInstruction(instruction);
                    break;
                case 85:
                    instructionCode = HandleMinusMinusInstruction(instruction);
                    break;
                case 92:
                    instructionCode = HandleNotInstruction(instruction);
                    break;
                case 93:
                    instructionCode = HandleTilInstruction(instruction);
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
                case 99:
                    instructionCode = HandleSetIncrementalInstruction(instruction);
                    break;
                case 100:
                    instructionCode = HandleSetDecrementalInstruction(instruction);
                    break;
                case 101:
                    instructionCode = HandleMultiplyInstruction(instruction);
                    break;
                case 102:
                    instructionCode = HandleModEqualInstruction(instruction);
                    break;
                case 109:
                    instructionCode = HandleDivideInstruction(instruction);
                    break;
                case 112:
                    instructionCode = HandleArrayInstruction(instruction);
                    break;
                case 113:
                    instructionCode = HandleBlockInstruction(instruction);
                    break;
                case 114:
                    instructionCode = HandleByFieldInstruction(instruction);
                    break;
                case 115:
                    instructionCode = HandleByIndexdInstruction(instruction);
                    break;
                case 116:
                    instructionCode = HandleCallInstruction(instruction);
                    break;
                case 118:
                    instructionCode = HandleChild(instruction.GetChild(0));
                    break;
                case 117:
                    instructionCode = HandleCexprInstruction(instruction);
                    break;
                case 119:
                    instructionCode = HandleForIterInstruction(instruction);
                    break;
                case 120:
                    instructionCode = HandleForStepInstruction(instruction);
                    break;
                case 121:
                    instructionCode = HandleItemInstruction(instruction);
                    break;
                case 123:
                    instructionCode = HandleNamedValueInstruction(instruction);
                    break;
                case 124:
                    instructionCode = HandleNegInstruction(instruction);
                    break;
                case 125:
                    instructionCode = HandleObjectInstruction(instruction);
                    break;
                case 126:
                    instructionCode = HandleParamExprInstruction(instruction);
                    break;
                case 127:
                    instructionCode = HandleMinusMinusInstruction(instruction);
                    break;
                case 128:
                    instructionCode = HandlePlusPlusInstruction(instruction);
                    break;
                case 129:
                    instructionCode = HandlePosInstruction(instruction);
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
        /// %= code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleModEqualInstruction(ITree instruction)
        {
            return HandleSetInstruction(instruction);
        }

        /// <summary>
        /// switch Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleSwitchInstruction(ITree instruction)
        {
            var condition = HandleChild(instruction.GetChild(0));
            string casesCode = "";

            //there is a dafault node?
            var defaultInstruction = instruction.GetChild(1).Text == "default";
            int init = 1;
            string defaultCode = "";

            #region Default code
            if (defaultInstruction)
            {
                init = 2;
                
                var actualInstruction = instruction.GetChild(1);
                var blockCase = HandleChild(actualInstruction.GetChild(0));
                
                blockCase = blockCase == "" ? "" : blockCase + ";";

                defaultCode = string.Format("{0} : \r\n {1}", "default", blockCase);
            }
            #endregion

            for (var i = init; i < instruction.ChildCount; i++)
            {
                var actualInstruction = instruction.GetChild(i);
                
                var textInstruction = actualInstruction.Text == "default" ? "default" : "case";
                string conditionCase = HandleChild(actualInstruction.GetChild(0));
                string blockCase = "";


                if (actualInstruction.ChildCount == 2)
                {
                    blockCase = HandleChild(actualInstruction.GetChild(1));
                    blockCase = blockCase == "" ? "" : blockCase + ";";
                }
                else
                {
                    var block = new CommonTree();

                    for (int j = 1; j < actualInstruction.ChildCount; j++)
                    {
                        block.AddChild(actualInstruction.GetChild(j));

                    }

                    blockCase += HandleBlockInstruction(block);
                }


                casesCode += string.Format("{0} {1} : \r\n {2}", textInstruction, conditionCase, blockCase);

            }

            casesCode += defaultCode;

            return string.Format("switch ({0}) \r\n {{{1}}}", condition, casesCode);
        }

        /// <summary>
        /// In code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleInInstruction(ITree instruction)
        {
            return HandleSetInstruction(instruction);
        }

        /// <summary>
        /// ~ code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleTilInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = String.Format("{2} {0} {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)), instruction.Text);

            return instructionCode;
        }

        /// <summary>
        /// * code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleMultiplyInstruction(ITree instruction)
        {
            return HandleSetInstruction(instruction);
        }

        /// <summary>
        /// -= Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleSetDecrementalInstruction(ITree instruction)
        {
            return HandleSetInstruction(instruction);
        }

        /// <summary>
        /// += code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleSetIncrementalInstruction(ITree instruction)
        {
            return HandleSetInstruction(instruction);
        }

        /// <summary>
        /// CEXPR Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleCexprInstruction(ITree instruction)
        {
            var condition1 = HandleChild(instruction.GetChild(0));
            var condition2 = HandleChild(instruction.GetChild(1));

            return string.Format("{0}, {1}", condition1, condition2);
        }

        /// <summary>
        /// Delete code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleDeleteInstruction(ITree instruction)
        {
            return string.Format("{0} {1}", instruction.Text, HandleChild(instruction.GetChild(0)));
        }

        /// <summary>
        /// Catch code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleCatchInstruction(ITree instruction)
        {
            var exceptionCode = HandleChild(instruction.GetChild(0));
            var blockCode = HandleChild(instruction.GetChild(1));

            return string.Format("catch ({0})\r\n {{{1}}}", exceptionCode, blockCode);

        }

        /// <summary>
        /// Try code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleTryInstruction(ITree instruction)
        {

            var blockCode = HandleChild(instruction.GetChild(0));
            var catchsCode = HandleChild(instruction.GetChild(1));
            
            return string.Format("try {{{0}}} \r\n {1}", blockCode, catchsCode);
        }

        /// <summary>
        /// -- code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleMinusMinusInstruction(ITree instruction)
        {
            return string.Format("{0}--", HandleChild(instruction.GetChild(0)));
        }

        /// <summary>
        /// While Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleWhileInstruction(ITree instruction)
        {
            var condition = HandleChild(instruction.GetChild(0));
            var blockCode = HandleChild(instruction.GetChild(1));

            return string.Format("while ({0}) {{{1}}}", condition, blockCode);
        }

        /// <summary>
        /// Generates POS code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandlePosInstruction(ITree instruction)
        {
            return string.Format("+{0}", HandleChild(instruction.GetChild(0)));
        }

        /// <summary>
        /// Generates New instruction
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleNewInstruction(ITree instruction)
        {
            return string.Format("{0} {1}", instruction.Text, HandleChild(instruction.GetChild(0)));
        }

        /// <summary>
        /// Generates Not instruction
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleNotInstruction(ITree instruction)
        {
            return string.Format("!{0}", HandleChild(instruction.GetChild(0)));
        }

        /// <summary>
        /// Generates NEG code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleNegInstruction(ITree instruction)
        {
            return string.Format("-{0}", HandleChild(instruction.GetChild(0)));
        }

        /// <summary>
        /// Generates dictonary code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleNamedValueInstruction(ITree instruction)
        {
            return string.Format("{0} : {1}", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)));
        }

        /// <summary>
        /// Generates objects code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleObjectInstruction(ITree instruction)
        {
            string instructionCode = "{";

            for (int i = 0; i < instruction.ChildCount; i++)
            {
                instructionCode += HandleChild(instruction.GetChild(i));
                if (i < instruction.ChildCount - 1)
                    instructionCode += ", \r\n";
            }

            return instructionCode + "}"; 
        }

        /// <summary>
        /// Generates foreach condition code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleForIterInstruction(ITree instruction)
        {
            var condition1 = HandleChild(instruction.GetChild(0));
            var condition2 = HandleChild(instruction.GetChild(1));
            return string.Format("{0} in {1}", condition1, condition2);
        }

        /// <summary>
        /// Generates array[index] code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleByIndexdInstruction(ITree instruction)
        {
            return string.Format("{0}[{1}]", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)));
        }

        /// <summary>
        /// Generates ++ code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandlePlusPlusInstruction(ITree instruction)
        {
            string instructionCode = string.Format("{0}{1}", HandleChild(instruction.GetChild(0)), instruction.Text); //mega estranho... mais blz

            return instructionCode; 
        }

        /// <summary>
        /// Generates FORSTEP code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleForStepInstruction(ITree instruction)
        {
            string instructionCode = "";

            for (int i = 0; i < instruction.ChildCount; i++)
            {
                instructionCode += HandleChild(instruction.GetChild(i));

                if (i < instruction.ChildCount - 1)
                    instructionCode += "; ";
            }

            return instructionCode; 
        }

        /// <summary>
        /// Generates FOR code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleForInstruction(ITree instruction)
        {
            var forstepsCode = HandleChild(instruction.GetChild(0));
            var blockCode = HandleChild(instruction.GetChild(1));
            string code = "";

            if (CountInstructionsOf(instruction.GetChild(1)) == 1)
                code = string.Format("for ({0}) {1}", forstepsCode, blockCode);
            else
                code = string.Format("for ({0}) {{{1}}}", forstepsCode, blockCode);

            return code;
        }

        /// <summary>
        /// Generates Array Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleArrayInstruction(ITree instruction)
        {
            string instructionCode = "[]";

            if (instruction.ChildCount == 1)
                instructionCode = string.Format("[{0}]", HandleChild(instruction.GetChild(0)));
            
            if (instruction.ChildCount == 2)
                instructionCode = string.Format("[{0}, {1}]", HandleChild(instruction.GetChild(0).GetChild(0) ), HandleChild(instruction.GetChild(1).GetChild(0)));
            
            if (instruction.ChildCount > 2)
            {
                instructionCode = "[";

                for (int i = 0; i < instruction.ChildCount; i++) //para cada item um par.
                {
                    instructionCode += HandleItemInstruction(instruction.GetChild(i));
                    
                    if (i < instruction.ChildCount - 1)
                        instructionCode += ",";
                }

                instructionCode += "]";
            }

            return instructionCode; 

        }

        /// <summary>
        /// Item code (for array and Dicts)
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleItemInstruction(ITree instruction)
        {
            string instructionCode = "";

            if (instruction.ChildCount == 1)
                instructionCode = string.Format("{0}", HandleChild(instruction.GetChild(0)));
            
            if (instruction.ChildCount == 2)
                instructionCode = string.Format("[{0}, {1}]", HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)));


            return instructionCode;
        }

        /// <summary>
        /// Generates Instance of Code
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private string HandleInstanceOfInstruction(ITree instruction)
        {
            string instructionCode = "";

            instructionCode = String.Format("{1} {0} {2}", instruction.Text, HandleChild(instruction.GetChild(0)), HandleChild(instruction.GetChild(1)));

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

            instructionCode = String.Format("{1} {0}", HandleChild(instruction.GetChild(0)), instruction.Text);

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
                blockCode += (IsNecessaryComma(actualInstruction)) ? ";" : "";
                
                blockCode += Environment.NewLine;
            }
            
            return blockCode;
        }

        /// <summary>
        /// Determines or not use comma at EOF
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private bool IsNecessaryComma(ITree instruction)
        {
            bool returningVal = true;

            switch (instruction.Type)
            {
                case 3: //Catch
                    returningVal = false;
                    break;
                case 16: //For
                    returningVal = false;
                    break;
                case 17: //Function
                    returningVal = false;
                    break;
                case 18: //If
                    returningVal = false;
                    break;
                case 23: //switch
                    returningVal = false;
                    break;
                case 26: //Try
                    returningVal = false;
                    break;
                case 30: //While
                    returningVal = false;
                    break;
            }
            
            return returningVal;
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

            conditionCode = HandleChild(condition);
            block1Code = HandleChild(block1);
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
            {
                if (CountInstructionsOf(block1) == 1)
                    instructionCode = String.Format("if ({0}) {1};\r\n", conditionCode, block1Code);
                else
                    instructionCode = String.Format("if ({0}) {{\r\n  {1}\r\n  }}", conditionCode, block1Code);
            }

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

            if (instruction.GetChild(0).Text == "group")
                return "";

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
                string functionName = HandleChild(instruction.GetChild(0));
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

            if (instruction.Parent != null && instruction.Parent.IsNil)
                instructionCode += ";";

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
                        argsNames += HandleChild(args.GetChild(i));

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
        /// Validates a node is a instruction node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool IsInstruction(ITree node)
        {
            switch (node.Type)
            {
                case 22: //Return
                    return true;
                default: // Restante
                    return IsFunction(node);
            }
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
                ITree funcaoAtual = tree.GetChild(i);
                bool tipo = funcaoAtual.Type == 17 ;
                string nome = funcaoAtual.GetChild(0) == null ? "" : funcaoAtual.GetChild(0).Text;
                

                if (tipo && nome == nomeFuncaoOtimizar) 
                    return funcaoAtual;
                
                var neto = FindFunctionTree(funcaoAtual as CommonTree, nomeFuncaoOtimizar);

                if (neto != null)
                    return neto;

            }

            return null;
        }
        
        /// <summary>
        /// Makes a list of Top functions (Loc)
        /// </summary>
        /// <param name="functionsMostUseds"></param>
        /// <returns></returns>
        public static IEnumerable<string> FindTopLocFunctions(Dictionary<string, int> functionsMostUseds)
        {
            var locFunctions = new Dictionary<string, KeyValuePair<int, int>>();
            var realFunctions = functionsMostUseds.Where(f => Functions.Count(tree1 => tree1.GetChild(0).Text == f.Key) > 0);

            foreach (var pair in realFunctions)
            {
                string name = pair.Key;
                int calls = pair.Value;
                int loc = CountInstructionsOf(Functions.Single(t => t.GetChild(0).Text.Equals(name)).GetChild(2));
                var locCallPair = new KeyValuePair<int, int>(calls, loc);
                locFunctions.Add(name, locCallPair);
            }
            
            var orderedResult =
                locFunctions.OrderByDescending(c => c.Value.Key)
                            .OrderByDescending(l => l.Value.Value)
                            .Select(f => f.Key)
                            .AsEnumerable();
            
            return orderedResult;
        }

        /// <summary>
        /// Count instructions of a function by name
        /// </summary>
        /// <param name="functionBody"></param>
        /// <returns></returns>
        public static int CountInstructionsOf(ITree functionBody)
        {
            if (functionBody == null)
                return 0;

            int instructions = 0; // IsInstruction(functionBody) ? 1 : 0;

            for (int i = 0; i < functionBody.ChildCount; i++)
            {
                if (IsInstruction(functionBody.GetChild(i)))
                    instructions++;
                
                instructions += CountInstructionsOf(functionBody.GetChild(i));
            }

            return instructions;
        }

        /// <summary>
        /// Finds all functions CALLS
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static Dictionary<string, int> FindTopUsedFunctions(CommonTree tree)
        {

            var functionsFounded = new List<string>();

            for (int i = 0; i < tree.ChildCount; i++)
            {
                functionsFounded.AddRange(VisitForFunctionCall(tree.GetChild(i)));
            }

            var topCount = functionsFounded.GroupBy(item => item).OrderByDescending(x=> x.Count()).ToDictionary(x => x.Key, x => x.Count());

            return topCount;
        }


        /// <summary>
        /// Visits the node and its children to find functions calls
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static IEnumerable<string> VisitForFunctionCall(ITree node)
        {
            var functions = new List<string>();

            #region IsFunction Node?
            if (node.Type == 116) //function CALL
            {
                if (!functions.Contains(node.GetChild(0).Text))
                    functions.Add(node.GetChild(0).Text);
            }
            #endregion

            for (int i = 0; i < node.ChildCount; i++)
            {
                functions.AddRange(VisitForFunctionCall(node.GetChild(i)));
            }

            return functions;
        }


        /// <summary>
        /// Reads the entire tree and keeps the functions for crossover and mutation operations
        /// </summary>
        /// <returns></returns>
        public static List<ITree> BuildFunctionList(CommonTree tree)
        {
            if (Functions.Count > 0)
                return Functions;

            var functionsFounded = new List<string>();

            for (int i = 0; i < tree.ChildCount; i++)
            {
                functionsFounded.AddRange(VisitForFunctionName(tree.GetChild(i)));
            }

            functionsFounded.ForEach(f => Functions.Add(JavascriptAstCodeGenerator.FindFunctionTree(tree, f)));

            return Functions;
        }

        /// <summary>
        /// Visits the node and its children to find functions nodes
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static IEnumerable<string> VisitForFunctionName(ITree node)
        {
            var functions = new List<string>();

            #region IsFunction Node?
            if (node.Type == 17 && node.GetChild(0).Text != "ARGS") //function
            {
                functions.Add(node.GetChild(0).Text);
            }
            #endregion

            for (int i = 0; i < node.ChildCount; i++)
            {
                functions.AddRange(VisitForFunctionName(node.GetChild(i)));
            }

            return functions;
        }

        /// <summary>
        /// Creates a Clone of a Tree
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static CommonTree DeepClone(CommonTree tree)
        {
            var root = tree.DupNode();

            for (int i = 0; i < tree.ChildCount; i++)
            {
                var clonedChild = DeepClone(tree.GetChild(i));
                root.AddChild(clonedChild);
            }

            return root as CommonTree;
        }

        /// <summary>
        /// Creates a Deep Clone of a ITree node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static ITree DeepClone(ITree node)
        {
            var cloneNode = node.DupNode();

            for (int i = 0; i < node.ChildCount; i++)
            {
                var cloneChildNode = DeepClone(node.GetChild(i)); //gets a dupNode cloned
                cloneNode.AddChild(cloneChildNode); //adds
            }

            return cloneNode;
        }

        /// <summary>
        /// Gets the Node of Function 
        /// </summary>
        /// <param name="nomeFuncaoTarget"></param>
        /// <returns></returns>
        public static ITree GetFunctionTree(string nomeFuncaoTarget)
        {
            return Functions.First(f => f.GetChild(0).Text.Equals(nomeFuncaoTarget));
        }

        #endregion


    }
}
