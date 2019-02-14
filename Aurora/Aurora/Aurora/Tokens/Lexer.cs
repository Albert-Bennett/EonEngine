/* Created: 28/08/2015
 * Last Updated: 02/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Aurora.Scaning;
using Aurora.Tokens.Keywords;
using Aurora.Tokens.Keywords.Methods;
using Aurora.Tokens.Keywords.Variables;
using Aurora.Tokens.Misc;
using Aurora.Tokens.Operators.Calls;
using Aurora.Tokens.Operators.Comparison;
using Aurora.Tokens.Operators.Infix;
using System;
using System.Collections.Generic;

namespace Aurora.Tokens
{
    /// <summary>
    /// Defines a class that is used to Generate tokens.
    /// </summary>
    internal sealed class Lexer
    {
        List<Token> tokens;
        List<Character> characters;

        /// <summary>
        /// Generates a set of Tokens.
        /// </summary>
        /// <param name="characters">The Characters to be used in 
        /// the generation of the Tokens.</param>
        public Lexer(List<Character> characters, ref List<Token>tokens)
        {
            this.characters = characters;
            this.tokens = tokens;

            for (int i = 0; i < characters.Count; i++)
                tokens.Add(Parse(characters[i], i, out i));
        }

        Token Parse(Character character, int idx, out int newIdx)
        {
            newIdx = idx;

            switch (character.Value)
            {
                case ',':
                    return new CommaToken();

                #region Digits

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return new ByteDigit(character.Value);

                case '.':
                    return new FullStop();

                #endregion
                #region Calls

                case '{':
                    return new OpenedBrace();

                case '}':
                    return new ClosedBrace();

                case '(':
                    return new OpenedBracket();

                case ')':
                    return new ClosedBracket();

                case ':':
                    return new ColonSet();

                case '#':
                    return FindText(character, ' ', newIdx, out newIdx);

                case ';':
                    return new SemiColon();

                case '[':
                    return new OpenedCrotchet();

                case ']':
                    return new ClosedCrotchet();

                #endregion
                #region Comparison

                case '=':
                    return new EqualToOperator();

                case '"':
                    return FindText(character, '"', newIdx+=1, out newIdx);

                case '!':
                    return GenerateComparision(character, newIdx, out newIdx);

                #endregion
                #region Infix

                case '^':
                    return new CaretOperator();

                case '/':
                    return new DivideOperator();

                case '*':
                    return new MultiplyOperator();

                case '-':
                case '+':
                    return GenerateMathSymbol(character.Value, newIdx, out newIdx);

                #endregion
                #region Logical

                case '&':
                    return new AndOperator();

                case '|':
                    return new OrOperator();

                #endregion

                default:
                    return GenerateKeyword(newIdx, out newIdx);
            }
        }

        Token GenerateKeyword(int idx, out int newIdx)
        {
            newIdx = idx;

            string keyword = FindToSpace(':', ref newIdx);

            switch (keyword)
            {
                case "true":
                    return new TrueToken();

                case "false":
                    return new FalseToken();

                case "prop_set":
                    return new PropSetToken();

                case "colour":
                    return new ColourToken();

                case "vector2":
                    return new VectorToken(2);

                case "vector3":
                    return new VectorToken(3);

                case "vector4":
                    return new VectorToken(4);

                case "intarray":
                    return new IntArrayToken();

                case "stringarray":
                    return new StringArrayToken();

                case "random_colour":
                    return new RandomColourToken();

                case "random_vector2":
                    return new RandomVector2Token();

                case "random_vector3":
                    return new RandomVector3Token();

                case "random_vector4":
                    return new RandomVector4Token();

                case "if":
                    return new IFToken();

                case "elseif":
                    return new ElseIFToken();

                case "else":
                    return new ElseToken();
            }

            throw new ArgumentException("Invalid Keyword: " + keyword);
        }

        Token FindText(Character character, char stop, int idx, out int newIdx)
        {
            newIdx = idx;

            string id = FindToSpace(stop, ref newIdx);

            if (stop == '"')
            {
                newIdx++;

                return new CharacterSet(id);
            }

            return new HashID(id);
        }

        string FindToSpace(char stop, ref int newIdx)
        {
            bool found = false;
            string id = "";

            while (!found && newIdx < characters.Count)
            {
                if (characters[newIdx].Value != stop && characters[newIdx].Value != '{'
                    && characters[newIdx].Value != ',' && characters[newIdx].Value != ':'
                    && characters[newIdx].Value != '[' && characters[newIdx].Value != '(')
                {
                    id += characters[newIdx].Value.ToString();
                    newIdx++;
                }
                else
                    found = true;
            }

            newIdx--;

            return id;
        }

        Token GenerateComparision(Character character, int idx, out int newIdx)
        {
            newIdx = idx;

            if (characters[idx++].Value == '=')
            {
                newIdx++;
                return new NotEqualOperator();
            }
            else
                return new NotOperator();
        }

        Token GenerateMathSymbol(char value, int idx, out int newIdx)
        {
            newIdx = idx;

            if (value == '-')
                if (characters[idx++].Value == '-')
                {
                    newIdx++;
                    return new MinusMinus();
                }
                else
                    return new MinusOperator();
            else
                if (characters[idx++].Value == '+')
                {
                    newIdx++;
                    return new PlusPlus();
                }
                else
                    return new PlusOperator();
        }
    }
}
