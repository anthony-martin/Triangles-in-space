using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TrianglesInSpace.Messaging
{
    internal class FieldContractResolver : DefaultContractResolver
    {
        public FieldContractResolver()
        {
            DefaultMembersSearchFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        }

        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            List<MemberInfo> members = new List<MemberInfo>();

            while (objectType != null)
            {
                members.AddRange(GetFields(objectType));
                objectType = objectType.BaseType;
            }

            return members;
        }

        private IEnumerable<MemberInfo> GetFields(IReflect type)
        {
            return type
                .GetFields(DefaultMembersSearchFlags)
                .Where(x => !x.IsDefined(typeof(NonSerializedAttribute), false));
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);

            foreach (var property in properties)
            {
                property.Readable = true;
                property.Writable = true;
            }

            return properties;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            const string MemberPrefix = "m_";

            if (propertyName.StartsWith(MemberPrefix, StringComparison.Ordinal))
            {
                return propertyName.Substring(MemberPrefix.Length);
            }

            const string BackingPrefix = "<";
            const string BackingSuffix = ">k__BackingField";

            if (propertyName.EndsWith(BackingSuffix, StringComparison.Ordinal))
            {
                return propertyName.Substring(
                    BackingPrefix.Length,
                    propertyName.Length - BackingPrefix.Length - BackingSuffix.Length);
            }

            return propertyName;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);

            contract.DefaultCreator = () => FormatterServices.GetUninitializedObject(objectType);
            contract.DefaultCreatorNonPublic = false;
            contract.ParametrizedConstructor = null;

            return contract;
        }
    }
}
