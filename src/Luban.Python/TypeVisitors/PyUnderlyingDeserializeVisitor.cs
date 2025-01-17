using Luban.DataExporter.Builtin.Json;
using Luban.Datas;
using Luban.Python.TemplateExtensions;
using Luban.Types;
using Luban.TypeVisitors;

namespace Luban.Python.TypeVisitors;

public class PyUnderlyingDeserializeVisitor : ITypeFuncVisitor<string, string, string>
{
    public static PyUnderlyingDeserializeVisitor Ins { get; } = new();

    public string Accept(TBool type, string jsonVarName, string fieldName)
    {
        return $"{fieldName} = {jsonVarName}";
    }

    public string Accept(TByte type, string jsonVarName, string fieldName)
    {
        return $"{fieldName} = {jsonVarName}";
    }

    public string Accept(TShort type, string jsonVarName, string fieldName)
    {
        return $"{fieldName} = {jsonVarName}";
    }

    public string Accept(TInt type, string jsonVarName, string fieldName)
    {
        return $"{fieldName} = {jsonVarName}";
    }

    public string Accept(TLong type, string jsonVarName, string fieldName)
    {
        return $"{fieldName} = {jsonVarName}";
    }

    public string Accept(TFloat type, string jsonVarName, string fieldName)
    {
        return $"{fieldName} = {jsonVarName}";
    }

    public string Accept(TDouble type, string jsonVarName, string fieldName)
    {
        return $"{fieldName} = {jsonVarName}";
    }

    public string Accept(TEnum type, string jsonVarName, string fieldName)
    {
        return $"{fieldName} = {jsonVarName}";
    }

    public string Accept(TString type, string jsonVarName, string fieldName)
    {
        return $"{fieldName} = {jsonVarName}";
    }

    public string Accept(TBean type, string jsonVarName, string fieldName)
    {
        if (type.DefBean.IsAbstractType)
        {
            return $"{fieldName} = {PythonCommonTemplateExtension.FullName(type.DefBean)}.fromJson({jsonVarName})";
        }
        else
        {
            return $"{fieldName} = {PythonCommonTemplateExtension.FullName(type.DefBean)}({jsonVarName})";
        }
    }

    public string Accept(TArray type, string jsonVarName, string fieldName)
    {
        if (type.Apply(SimpleJsonTypeVisitor.Ins))
        {
            return $"{fieldName} = {jsonVarName}";
        }
        else
        {
            return $"{fieldName} = []\nfor _ele in {jsonVarName}: {type.ElementType.Apply(this, "_ele", "_e")}; {fieldName}.append(_e)";
        }
    }

    public string Accept(TList type, string jsonVarName, string fieldName)
    {
        if (type.Apply(SimpleJsonTypeVisitor.Ins))
        {
            return $"{fieldName} = {jsonVarName}";
        }
        else
        {
            return $"{fieldName} = []\nfor _ele in {jsonVarName}: {type.ElementType.Apply(this, "_ele", "_e")}; {fieldName}.append(_e)";
        }
    }

    public string Accept(TSet type, string jsonVarName, string fieldName)
    {
        if (type.Apply(SimpleJsonTypeVisitor.Ins))
        {
            return $"{fieldName} = {jsonVarName}";
        }
        else
        {
            return $"{fieldName} = set()\nfor _ele in {jsonVarName}: {type.ElementType.Apply(this, "_ele", "_e")}; {fieldName}.add(_e)";
        }
    }

    public string Accept(TMap type, string jsonVarName, string fieldName)
    {
        return $"{fieldName} = {{}}\nfor _ek, _ev in {jsonVarName}: {type.KeyType.Apply(this, "_ek", "_k")}; {type.ValueType.Apply(this, "_ev", "_v")}; {fieldName}[_k] =_v";
    }

    public string Accept(TDateTime type, string jsonVarName, string fieldName)
    {
        return $"{fieldName} = {jsonVarName}";
    }
}