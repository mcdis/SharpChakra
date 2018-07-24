using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChakra
{
    public struct JsPromise
    {
        public JsPromise(JsValue promise, JsValue resolveFunc, JsValue rejectFunc)
        {
            Promise = promise;
            ResolveFunc = resolveFunc;
            RejectFunc = rejectFunc;
        }

        public JsValue Promise { get; }

        public JsValue ResolveFunc { get; }

        public JsValue RejectFunc { get; }

        public static JsPromise Create()
        {
            Native.ThrowIfError(Native.JsCreatePromise(out var promise, out var resolveFunc, out var rejectFunc));

            return new JsPromise(promise, resolveFunc, rejectFunc);
        }
    }
}
