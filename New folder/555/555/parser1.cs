
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF                 =  0, // (EOF)
        SYMBOL_ERROR               =  1, // (Error)
        SYMBOL_WHITESPACE          =  2, // Whitespace
        SYMBOL_LPAREN              =  3, // '('
        SYMBOL_RPAREN              =  4, // ')'
        SYMBOL_COMMA               =  5, // ','
        SYMBOL_COLON               =  6, // ':'
        SYMBOL_SEMI                =  7, // ';'
        SYMBOL_EQ                  =  8, // '='
        SYMBOL_BREAK               =  9, // break
        SYMBOL_CASE                = 10, // case
        SYMBOL_COMPARISON          = 11, // Comparison
        SYMBOL_DEF                 = 12, // def
        SYMBOL_DO                  = 13, // do
        SYMBOL_ELSE                = 14, // else
        SYMBOL_END                 = 15, // end
        SYMBOL_FOR                 = 16, // for
        SYMBOL_IDENTIFIER          = 17, // Identifier
        SYMBOL_IF                  = 18, // if
        SYMBOL_NEWLINE             = 19, // NewLine
        SYMBOL_NUMBER              = 20, // Number
        SYMBOL_OPERATOR            = 21, // Operator
        SYMBOL_STRINGLITERAL       = 22, // StringLiteral
        SYMBOL_SWITCH              = 23, // switch
        SYMBOL_WHILE               = 24, // while
        SYMBOL_ASSIGNMENT          = 25, // <Assignment>
        SYMBOL_CONDITION           = 26, // <Condition>
        SYMBOL_DOWHILESTATEMENT    = 27, // <DoWhileStatement>
        SYMBOL_EXPRESSION          = 28, // <Expression>
        SYMBOL_FORLOOP             = 29, // <ForLoop>
        SYMBOL_FUNCTIONDECLARATION = 30, // <FunctionDeclaration>
        SYMBOL_IFSTATEMENT         = 31, // <IfStatement>
        SYMBOL_NL                  = 32, // <nl>
        SYMBOL_NLOPT               = 33, // <nl Opt>
        SYMBOL_PARAMETERLIST       = 34, // <ParameterList>
        SYMBOL_PROGRAM             = 35, // <Program>
        SYMBOL_START               = 36, // <Start>
        SYMBOL_STATEMENT           = 37, // <Statement>
        SYMBOL_STATEMENTLIST       = 38, // <StatementList>
        SYMBOL_SWITCHCASE          = 39, // <SwitchCase>
        SYMBOL_SWITCHCASES         = 40, // <SwitchCases>
        SYMBOL_SWITCHSTATEMENT     = 41, // <SwitchStatement>
        SYMBOL_TERM                = 42, // <Term>
        SYMBOL_WHILESTATEMENT      = 43  // <WhileStatement>
    };

    enum RuleConstants : int
    {
        RULE_NL_NEWLINE                                                 =  0, // <nl> ::= NewLine <nl>
        RULE_NL_NEWLINE2                                                =  1, // <nl> ::= NewLine
        RULE_NLOPT_NEWLINE                                              =  2, // <nl Opt> ::= NewLine <nl Opt>
        RULE_NLOPT                                                      =  3, // <nl Opt> ::= 
        RULE_START                                                      =  4, // <Start> ::= <nl Opt> <Program>
        RULE_PROGRAM                                                    =  5, // <Program> ::= <StatementList>
        RULE_STATEMENTLIST                                              =  6, // <StatementList> ::= <Statement>
        RULE_STATEMENTLIST2                                             =  7, // <StatementList> ::= <Statement> <StatementList>
        RULE_STATEMENT                                                  =  8, // <Statement> ::= <Assignment>
        RULE_STATEMENT2                                                 =  9, // <Statement> ::= <IfStatement>
        RULE_STATEMENT3                                                 = 10, // <Statement> ::= <WhileStatement>
        RULE_STATEMENT4                                                 = 11, // <Statement> ::= <DoWhileStatement>
        RULE_STATEMENT5                                                 = 12, // <Statement> ::= <ForLoop>
        RULE_STATEMENT6                                                 = 13, // <Statement> ::= <SwitchStatement>
        RULE_STATEMENT7                                                 = 14, // <Statement> ::= <FunctionDeclaration>
        RULE_ASSIGNMENT_IDENTIFIER_EQ                                   = 15, // <Assignment> ::= Identifier '=' <Expression>
        RULE_IFSTATEMENT_IF_COLON_END                                   = 16, // <IfStatement> ::= if <Condition> ':' <StatementList> end
        RULE_IFSTATEMENT_IF_COLON_ELSE_COLON_END                        = 17, // <IfStatement> ::= if <Condition> ':' <StatementList> else ':' <StatementList> end
        RULE_WHILESTATEMENT_WHILE_COLON_END                             = 18, // <WhileStatement> ::= while <Condition> ':' <StatementList> end
        RULE_DOWHILESTATEMENT_DO_COLON_WHILE_END                        = 19, // <DoWhileStatement> ::= do ':' <StatementList> while <Condition> end
        RULE_FORLOOP_FOR_SEMI_SEMI_COLON_END                            = 20, // <ForLoop> ::= for <Assignment> ';' <Condition> ';' <Assignment> ':' <StatementList> end
        RULE_SWITCHSTATEMENT_SWITCH_COLON_END                           = 21, // <SwitchStatement> ::= switch <Expression> ':' <SwitchCases> end
        RULE_SWITCHCASES                                                = 22, // <SwitchCases> ::= <SwitchCase>
        RULE_SWITCHCASES2                                               = 23, // <SwitchCases> ::= <SwitchCase> <SwitchCases>
        RULE_SWITCHCASE_CASE_COLON_BREAK                                = 24, // <SwitchCase> ::= case <Expression> ':' <StatementList> break
        RULE_FUNCTIONDECLARATION_DEF_IDENTIFIER_LPAREN_RPAREN_COLON_END = 25, // <FunctionDeclaration> ::= def Identifier '(' <ParameterList> ')' ':' <StatementList> end
        RULE_PARAMETERLIST_IDENTIFIER                                   = 26, // <ParameterList> ::= Identifier
        RULE_PARAMETERLIST_IDENTIFIER_COMMA                             = 27, // <ParameterList> ::= Identifier ',' <ParameterList>
        RULE_EXPRESSION                                                 = 28, // <Expression> ::= <Term>
        RULE_EXPRESSION_OPERATOR                                        = 29, // <Expression> ::= <Expression> Operator <Term>
        RULE_TERM_IDENTIFIER                                            = 30, // <Term> ::= Identifier
        RULE_TERM_NUMBER                                                = 31, // <Term> ::= Number
        RULE_TERM_STRINGLITERAL                                         = 32, // <Term> ::= StringLiteral
        RULE_TERM_LPAREN_RPAREN                                         = 33, // <Term> ::= '(' <Expression> ')'
        RULE_CONDITION_COMPARISON                                       = 34  // <Condition> ::= <Expression> Comparison <Expression>
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lst;
        ListBox ls;

        public MyParser(string filename,ListBox lst,ListBox ls)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this.lst = lst;
            this.ls = ls;
            
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BREAK :
                //break
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMPARISON :
                //Comparison
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEF :
                //def
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO :
                //do
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //end
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //Identifier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NEWLINE :
                //NewLine
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUMBER :
                //Number
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OPERATOR :
                //Operator
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGLITERAL :
                //StringLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENT :
                //<Assignment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONDITION :
                //<Condition>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOWHILESTATEMENT :
                //<DoWhileStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<Expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORLOOP :
                //<ForLoop>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCTIONDECLARATION :
                //<FunctionDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFSTATEMENT :
                //<IfStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NL :
                //<nl>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NLOPT :
                //<nl Opt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMETERLIST :
                //<ParameterList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<Program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //<Start>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<Statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTLIST :
                //<StatementList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCHCASE :
                //<SwitchCase>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCHCASES :
                //<SwitchCases>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCHSTATEMENT :
                //<SwitchStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<Term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILESTATEMENT :
                //<WhileStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_NL_NEWLINE :
                //<nl> ::= NewLine <nl>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NL_NEWLINE2 :
                //<nl> ::= NewLine
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NLOPT_NEWLINE :
                //<nl Opt> ::= NewLine <nl Opt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NLOPT :
                //<nl Opt> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_START :
                //<Start> ::= <nl Opt> <Program>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PROGRAM :
                //<Program> ::= <StatementList>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTLIST :
                //<StatementList> ::= <Statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTLIST2 :
                //<StatementList> ::= <Statement> <StatementList>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<Statement> ::= <Assignment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT2 :
                //<Statement> ::= <IfStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT3 :
                //<Statement> ::= <WhileStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT4 :
                //<Statement> ::= <DoWhileStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT5 :
                //<Statement> ::= <ForLoop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT6 :
                //<Statement> ::= <SwitchStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT7 :
                //<Statement> ::= <FunctionDeclaration>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT_IDENTIFIER_EQ :
                //<Assignment> ::= Identifier '=' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTATEMENT_IF_COLON_END :
                //<IfStatement> ::= if <Condition> ':' <StatementList> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTATEMENT_IF_COLON_ELSE_COLON_END :
                //<IfStatement> ::= if <Condition> ':' <StatementList> else ':' <StatementList> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILESTATEMENT_WHILE_COLON_END :
                //<WhileStatement> ::= while <Condition> ':' <StatementList> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DOWHILESTATEMENT_DO_COLON_WHILE_END :
                //<DoWhileStatement> ::= do ':' <StatementList> while <Condition> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORLOOP_FOR_SEMI_SEMI_COLON_END :
                //<ForLoop> ::= for <Assignment> ';' <Condition> ';' <Assignment> ':' <StatementList> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCHSTATEMENT_SWITCH_COLON_END :
                //<SwitchStatement> ::= switch <Expression> ':' <SwitchCases> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCHCASES :
                //<SwitchCases> ::= <SwitchCase>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCHCASES2 :
                //<SwitchCases> ::= <SwitchCase> <SwitchCases>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCHCASE_CASE_COLON_BREAK :
                //<SwitchCase> ::= case <Expression> ':' <StatementList> break
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONDECLARATION_DEF_IDENTIFIER_LPAREN_RPAREN_COLON_END :
                //<FunctionDeclaration> ::= def Identifier '(' <ParameterList> ')' ':' <StatementList> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMETERLIST_IDENTIFIER :
                //<ParameterList> ::= Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMETERLIST_IDENTIFIER_COMMA :
                //<ParameterList> ::= Identifier ',' <ParameterList>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<Expression> ::= <Term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_OPERATOR :
                //<Expression> ::= <Expression> Operator <Term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_IDENTIFIER :
                //<Term> ::= Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_NUMBER :
                //<Term> ::= Number
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_STRINGLITERAL :
                //<Term> ::= StringLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_LPAREN_RPAREN :
                //<Term> ::= '(' <Expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONDITION_COMPARISON :
                //<Condition> ::= <Expression> Comparison <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"in line: "+args.UnexpectedToken.Location.LineNr;
            lst.Items.Add(message);
            string m2 ="ExpectedToken "+args.ExpectedTokens.ToString();
            lst.Items.Add(m2);
            //todo: Report message to UI?
        }
        private void TokenReadEvent(LALRParser pr, TokenReadEventArgs args)
        {
            string info = args.Token.Text + "\t  \t" +(SymbolConstants) args.Token.Symbol.Id;
            ls.Items.Add(info);
        }

    }
}
