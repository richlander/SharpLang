﻿using System;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;
using SharpLLVM;

namespace SharpLang.CompilerServices
{
    public partial class Compiler
    {
        /// <summary>
        /// Gets the specified type.
        /// </summary>
        /// <param name="typeReference">The type reference.</param>
        /// <returns></returns>
        Type GetType(TypeReference typeReference)
        {
            Type type;
            var typeDefinition = typeReference.Resolve();
            if (types.TryGetValue(typeDefinition, out type))
                return type;

            type = BuildType(typeDefinition, false);

            types.Add(typeDefinition, type);

            return type;
        }
        
        /// <summary>
        /// Compiles the specified type.
        /// </summary>
        /// <param name="typeReference">The type reference.</param>
        /// <returns></returns>
        private Type CreateType(TypeReference typeReference)
        {
            Type type;
            var typeDefinition = typeReference.Resolve();
            if (types.TryGetValue(typeDefinition, out type))
                return type;

            type = BuildType(typeDefinition, true);

            types.Add(typeDefinition, type);

            return type;
        }

        /// <summary>
        /// Internal helper to actually builds the type.
        /// </summary>
        /// <param name="typeDefinition">The type definition.</param>
        /// <param name="allowClassResolve">if set to <c>true</c> [allow class resolve].</param>
        /// <returns></returns>
        private Type BuildType(TypeDefinition typeDefinition, bool allowClassResolve)
        {
            TypeRef dataType;
            StackValueType stackType;

            switch (typeDefinition.MetadataType)
            {
                case MetadataType.Void:
                    dataType = LLVM.VoidTypeInContext(context);
                    stackType = StackValueType.Unknown;
                    break;
                case MetadataType.Boolean:
                    dataType = LLVM.Int1TypeInContext(context);
                    stackType = StackValueType.Int32;
                    break;
                case MetadataType.Char:
                case MetadataType.Byte:
                case MetadataType.SByte:
                    dataType = LLVM.Int8TypeInContext(context);
                    stackType = StackValueType.Int32;
                    break;
                case MetadataType.Int16:
                case MetadataType.UInt16:
                    dataType = LLVM.Int16TypeInContext(context);
                    stackType = StackValueType.Int32;
                    break;
                case MetadataType.Int32:
                case MetadataType.UInt32:
                    dataType = LLVM.Int32TypeInContext(context);
                    stackType = StackValueType.Int32;
                    break;
                case MetadataType.Int64:
                case MetadataType.UInt64:
                    dataType = LLVM.Int64TypeInContext(context);
                    stackType = StackValueType.Int64;
                    break;
                case MetadataType.String:
                    // String: 32 bit length + char pointer
                    dataType = LLVM.StructCreateNamed(context, typeDefinition.FullName);
                    LLVM.StructSetBody(dataType,
                        new[] { LLVM.Int32TypeInContext(context), LLVM.PointerType(LLVM.Int8TypeInContext(context), 0) }, false);
                    stackType = StackValueType.Value;
                    break;
                case MetadataType.ValueType:
                case MetadataType.Class:
                case MetadataType.Object:
                {
                    if (!allowClassResolve)
                        throw new InvalidOperationException();

                    // When resolved, void becomes a real type
                    if (typeDefinition.FullName == typeof(void).FullName)
                    {
                        goto case MetadataType.Void;
                    }

                    // Process non-static fields
                    var @class = CreateClass(typeDefinition);

                    dataType = @class.Type;
                    stackType = @class.StackType;

                    break;
                }
                default:
                    throw new NotImplementedException();
            }

            return new Type(typeDefinition, dataType, stackType);
        }
    }
}