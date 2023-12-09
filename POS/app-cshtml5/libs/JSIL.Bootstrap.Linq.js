if (typeof (JSIL) === "undefined")
    throw new Error("JSIL.Core is required");

if (!$jsilcore)
    throw new Error("JSIL.Core is required");

JSIL.DeclareNamespace("System.Linq");
JSIL.DeclareNamespace("System.Linq.Expressions");

JSIL.MakeClass("System.Object", "JSIL.AbstractEnumerable", true, ["T"], function ($) {
    var T = new JSIL.GenericParameter("T", "JSIL.AbstractEnumerable");

    $.Method({ Static: false, Public: true }, ".ctor",
      new JSIL.MethodSignature(null, [JSIL.AnyType, JSIL.AnyType, JSIL.AnyType]),
      function (getNextItem, reset, dispose) {
          if (arguments.length === 1) {
              this._getEnumerator = getNextItem;
          } else {
              this._getEnumerator = null;
              this._getNextItem = getNextItem;
              this._reset = reset;
              this._dispose = dispose;
          }
      }
    );

    function getEnumeratorImpl() {
        if (this._getEnumerator !== null)
            return this._getEnumerator();
        else
            return new (JSIL.AbstractEnumerator.Of(this.T))(this._getNextItem, this._reset, this._dispose);
    };

    $.Method({ Static: false, Public: false }, null,
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.IEnumerator"), []),
      getEnumeratorImpl
    )
      .Overrides("System.Collections.IEnumerable", "GetEnumerator");

    $.Method({ Static: false, Public: true }, "GetEnumerator",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerator`1", [T]), []),
      getEnumeratorImpl
    )
      .Overrides("System.Collections.Generic.IEnumerable`1", "GetEnumerator");

    $.ImplementInterfaces(
      /* 0 */ $jsilcore.TypeRef("System.Collections.IEnumerable"),
      /* 1 */ $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [T])
    );
});

JSIL.ImplementExternals(
  "System.Linq.Enumerable", function ($) {
      $.Method({ Static: true, Public: true }, "Any",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Boolean"), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"]),
        function (T, enumerable) {
            var enumerator = JSIL.GetEnumerator(enumerable, T);

            var moveNext = System.Collections.IEnumerator.MoveNext;

            try {
                if (moveNext.Call(enumerator))
                    return true;
            } finally {
                JSIL.Dispose(enumerator);
            }

            return false;
        }
      );

      $.Method({ Static: true, Public: true }, "Any",
        new JSIL.MethodSignature(
          $jsilcore.TypeRef("System.Boolean"),
          [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $jsilcore.TypeRef("System.Boolean")])],
          ["TSource"]
        ),
        function (T, enumerable, predicate) {
            var enumerator = JSIL.GetEnumerator(enumerable, T);

            var tIEnumerator = System.Collections.Generic.IEnumerator$b1.Of(T);
            var moveNext = System.Collections.IEnumerator.MoveNext;
            var get_Current = tIEnumerator.get_Current;

            try {
                while (moveNext.Call(enumerator)) {
                    if (predicate(get_Current.Call(enumerator)))
                        return true;
                }
            } finally {
                JSIL.Dispose(enumerator);
            }

            return false;
        }
      );

      $.Method({ Static: true, Public: true }, "Count",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Int32"), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"]),
        function (T, enumerable) {
            var enumerator = JSIL.GetEnumerator(enumerable, T);

            var moveNext = System.Collections.IEnumerator.MoveNext;

            var result = 0;
            try {
                while (moveNext.Call(enumerator))
                    result += 1;
            } finally {
                JSIL.Dispose(enumerator);
            }
            return result;
        }
      );

      var elementAtImpl = function (enumerable, index) {
          var e = JSIL.GetEnumerator(enumerable);

          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          var getCurrent = $jsilcore.System.Collections.IEnumerator.get_Current;

          try {
              while (moveNext.Call(e)) {
                  if (index === 0) {
                      return { success: true, value: getCurrent.Call(e) };
                  } else {
                      index -= 1;
                  }
              }
          } finally {
              JSIL.Dispose(e);
          }

          return { success: false };
      };

      var firstImpl = function (enumerable, predicate) {
          var e = JSIL.GetEnumerator(enumerable);

          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          var getCurrent = $jsilcore.System.Collections.IEnumerator.get_Current;

          try {
              if (arguments.length >= 2) {
                  while (moveNext.Call(e)) {
                      var value = getCurrent.Call(e);
                      if (predicate(value))
                          return { success: true, value: value };
                  }
              } else {
                  if (moveNext.Call(e))
                      return { success: true, value: getCurrent.Call(e) };
              }
          } finally {
              JSIL.Dispose(e);
          }

          return { success: false };
      };

      $.Method({ Static: true, Public: true }, "First",
        new JSIL.MethodSignature(
          "!!0",
          [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])],
          ["TSource"]
        ),
        function (T, enumerable) {
            var result = firstImpl(enumerable);
            if (!result.success)
                throw new System.InvalidOperationException("Sequence contains no elements");

            return result.value;
        }
      );

      $.Method({ Static: true, Public: true }, "FirstOrDefault",
        new JSIL.MethodSignature(
          "!!0",
          [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])],
          ["TSource"]
        ),
        function (T, enumerable) {
            var result = firstImpl(enumerable);
            if (!result.success)
                return JSIL.DefaultValue(T);

            return result.value;
        }
      );

      $.Method({ Static: true, Public: true }, "First",
        new JSIL.MethodSignature(
          "!!0",
          [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]),
           $jsilcore.TypeRef("System.Func`2", ["!!0", $.Boolean])],
          ["TSource"]
        ),
        function (T, enumerable, predicate) {
            var result = firstImpl(enumerable, predicate);
            if (!result.success)
                throw new System.InvalidOperationException("Sequence contains no elements");

            return result.value;
        }
      );

      $.Method({ Static: true, Public: true }, "FirstOrDefault",
        new JSIL.MethodSignature(
          "!!0",
          [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]),
           $jsilcore.TypeRef("System.Func`2", ["!!0", $.Boolean])],
          ["TSource"]
        ),
        function (T, enumerable, predicate) {
            var result = firstImpl(enumerable, predicate);
            if (!result.success)
                return JSIL.DefaultValue(T);

            return result.value;
        }
      );

      var lastImpl = function (T, enumerable, predicate) {
          var IList = System.Collections.Generic.IList$b1.Of(T);
          var list = IList.$As(enumerable);
          if (list !== null) {
              var item = IList.get_Item;
              var useLength = (typeof list.Count) == 'undefined';
              if ((useLength && list.length === 0) || list.Count === 0)
                  return { success: false };
              var len = useLength ? list.length : list.Count;
              if (arguments.length >= 3) {
                  for (var i = len - 1; i >= 0; i--) {
                      var val = item.Call(list, [], i);
                      if (predicate(val))
                          return {
                              success: true,
                              value: val
                          }
                  }
                  return { success: false };
              } else {
                  return {
                      success: true,
                      value: item.Call(list, [], len - 1)
                  };
              }
          }
          var e = JSIL.GetEnumerator(enumerable);

          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          var getCurrent = $jsilcore.System.Collections.IEnumerator.get_Current;

          try {
              var acceptedVal;
              var val;
              while (moveNext.Call(e)) {
                  val = getCurrent.Call(e);
                  if (arguments.length >= 3) {
                      if (predicate(val))
                          acceptedVal = val;
                  } else
                      acceptedVal = val;
              }
              if (typeof acceptedVal !== 'undefined')
                  return { success: true, value: acceptedVal };
              return { success: false };
          } finally {
              JSIL.Dispose(e);
          }
      };

      $.Method({ Static: true, Public: true }, "Last",
        new JSIL.MethodSignature(
          "!!0",
          [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1",
            ["!!0"])],
          ["TSource"]
        ),
        function (T, enumerable) {
            var result = lastImpl(T, enumerable);
            if (!result.success)
                throw new System.InvalidOperationException("Sequence contains no elements");

            return result.value;
        }
      );

      $.Method({ Static: true, Public: true }, "Last",
        new JSIL.MethodSignature(
          "!!0",
          [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]),
           $jsilcore.TypeRef("System.Func`2", ["!!0", $.Boolean])],
          ["TSource"]
        ),
        function (T, enumerable, predicate) {
            var result = lastImpl(T, enumerable, predicate);
            if (!result.success)
                throw new System.InvalidOperationException("Sequence contains no elements");

            return result.value;
        }
      );

      $.Method({ Static: true, Public: true }, "Select",
        new JSIL.MethodSignature(
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!1"]),
          [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])],
          ["TSource", "TResult"]
        ),
        function (TSource, TResult, enumerable, selector) {
            var state = {};

            var tIEnumerator = System.Collections.Generic.IEnumerator$b1.Of(TSource);
            var moveNext = System.Collections.IEnumerator.MoveNext;
            var get_Current = tIEnumerator.get_Current;

            return new (JSIL.AbstractEnumerable.Of(TResult))(
              function getNext(result) {
                  var ok = moveNext.Call(state.enumerator);
                  if (ok)
                      result.set(selector(get_Current.Call(state.enumerator)));

                  return ok;
              },
              function reset() {
                  state.enumerator = JSIL.GetEnumerator(enumerable, TSource);
              },
              function dispose() {
                  JSIL.Dispose(state.enumerator);
              }
            );
        }
      );

      $.Method({ Static: true, Public: true }, "Select",
        new JSIL.MethodSignature(
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!1"]),
          [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`3", ["!!0", $.Int32, "!!1"])],
          ["TSource", "TResult"]
        ),
        function (TSource, TResult, enumerable, selector) {
            var state = {};

            var tIEnumerator = System.Collections.Generic.IEnumerator$b1.Of(TSource);
            var moveNext = System.Collections.IEnumerator.MoveNext;
            var get_Current = tIEnumerator.get_Current;
            var index = -1;

            return new (JSIL.AbstractEnumerable.Of(TResult))(
              function getNext(result) {
                  var ok = moveNext.Call(state.enumerator);
                  index++;
                  if (ok)
                      result.set(selector(get_Current.Call(state.enumerator), index));

                  return ok;
              },
              function reset() {
                  state.enumerator = JSIL.GetEnumerator(enumerable, TSource);
              },
              function dispose() {
                  JSIL.Dispose(state.enumerator);
              }
            );
        }
      );

      $.Method({ Static: true, Public: true }, "SelectMany",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!1"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!1"])])], ["TSource", "TResult"]),
        function SelectMany$b2(TSource, TResult, source, selector) {
            var state = {
                enumerator: null,
                currentSubsequence: null
            };

            var tIEnumerator = System.Collections.Generic.IEnumerator$b1.Of(TSource);
            var tIEnumeratorResult = System.Collections.Generic.IEnumerator$b1.Of(TResult);
            var moveNext = System.Collections.IEnumerator.MoveNext;
            var get_Current = tIEnumerator.get_Current;
            var get_CurrentResult = tIEnumeratorResult.get_Current;

            return new (JSIL.AbstractEnumerable.Of(TResult))(
              function getNext(result) {
                  while (true) {
                      if (state.currentSubsequence !== null) {
                          var ok = moveNext.Call(state.currentSubsequence);
                          if (ok) {
                              result.set(get_CurrentResult.Call(state.currentSubsequence));
                              return ok;
                          } else {
                              state.currentSubsequence = null;
                              JSIL.Dispose(state.currentSubsequence);
                          }
                      }

                      var ok = moveNext.Call(state.enumerator);
                      if (ok) {
                          var enumerable = selector(get_Current.Call(state.enumerator));
                          state.currentSubsequence = JSIL.GetEnumerator(enumerable, TResult);
                      } else {
                          return ok;
                      }
                  }
              },
              function reset() {
                  state.enumerator = JSIL.GetEnumerator(source, TSource);
                  state.currentSubsequence = null;
              },
              function dispose() {
                  JSIL.Dispose(state.enumerator);
                  JSIL.Dispose(state.currentSubsequence);
              }
            );
        }
      );
      $.Method({ Static: true, Public: true }, "ToArray",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Array", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"]),
        function (T, enumerable) {
            return JSIL.EnumerableToArray(enumerable, T);
        }
      );

      $.Method({ Static: true, Public: true }, "Contains",
        (new JSIL.MethodSignature($.Boolean, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), "!!0"], ["TSource"])),
        function Contains$b1(T, source, item) {
            var enumerator = JSIL.GetEnumerator(source, T);

            var tIEnumerator = System.Collections.Generic.IEnumerator$b1.Of(T);
            var moveNext = System.Collections.IEnumerator.MoveNext;
            var get_Current = tIEnumerator.get_Current;

            try {
                while (moveNext.Call(enumerator)) {
                    if (JSIL.ObjectEquals(get_Current.Call(enumerator), item))
                        return true;
                }
            } finally {
                JSIL.Dispose(enumerator);
            }

            return false;
        }
      );

      $.Method({ Static: true, Public: true }, "Cast",
        new JSIL.MethodSignature(
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]),
          [$jsilcore.TypeRef("System.Collections.IEnumerable")],
          ["TResult"]
        ),
       function (TResult, enumerable) {
           var state = {};

           var moveNext = System.Collections.IEnumerator.MoveNext;
           var get_Current = System.Collections.IEnumerator.get_Current;

           return new (JSIL.AbstractEnumerable.Of(TResult))(
             function getNext(result) {
                 var ok = moveNext.Call(state.enumerator);

                 if (ok)
                     result.set(TResult.$Cast(get_Current.Call(state.enumerator)));

                 return ok;
             },
             function reset() {
                 state.enumerator = JSIL.GetEnumerator(enumerable);
             },
             function dispose() {
                 JSIL.Dispose(state.enumerator);
             }
           );
       });

      $.Method({ Static: true, Public: true }, "Empty",
        new JSIL.MethodSignature(
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]),
          [],
          ["TResult"]
        ),
       function (TResult) {
           return new (JSIL.AbstractEnumerable.Of(TResult))(
             function getNext(result) {
                 return false;
             },
             function reset() {
             },
             function dispose() {
             }
           );
       });

      $.Method({ Static: true, Public: true }, "ToList",
        new JSIL.MethodSignature(
         $jsilcore.TypeRef("System.Collections.Generic.List`1", ["!!0"]),
         [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])],
         ["TSource"]
      ),
      function (TSource, enumerable) {
          var constructor = new JSIL.ConstructorSignature($jsilcore.TypeRef("System.Collections.Generic.List`1", [TSource]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [TSource])]);
          return constructor.Construct(enumerable);
      });

      $.Method({ Static: true, Public: true }, "ElementAt",
        new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $.Int32], ["TSource"]),
        function ElementAt$b1(TSource, source, index) {
            var result = elementAtImpl(source, index);
            if (!result.success)
                throw new System.ArgumentOutOfRangeException("index");

            return result.value;
        }
      );

      $.Method({ Static: true, Public: true }, "ElementAtOrDefault",
        new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $.Int32], ["TSource"]),
        function ElementAtOrDefault$b1(TSource, source, index) {
            var result = elementAtImpl(source, index);
            if (!result.success)
                return JSIL.DefaultValue(TSource);

            return result.value;
        }
      );

      $.Method({ Static: true, Public: true }, "OfType",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.IEnumerable")], ["TResult"]),
        function OfType$b1(TResult, source) {
            var state = {};

            var moveNext = System.Collections.IEnumerator.MoveNext;
            var get_Current = System.Collections.IEnumerator.get_Current;

            return new (JSIL.AbstractEnumerable.Of(TResult))(
              function getNext(result) {
                  while (moveNext.Call(state.enumerator)) {
                      var current = get_Current.Call(state.enumerator);

                      if (TResult.$Is(current)) {
                          result.set(current);
                          return true;
                      }
                  }

                  return false;
              },
              function reset() {
                  state.enumerator = JSIL.GetEnumerator(source);
              },
              function dispose() {
                  JSIL.Dispose(state.enumerator);
              }
            );
        }
      );

      $.Method({ Static: true, Public: true }, "Where",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Boolean])], ["TSource"]),
        function Where$b1(TSource, source, predicate) {
            var state = {};

            var moveNext = System.Collections.IEnumerator.MoveNext;
            var get_Current = System.Collections.IEnumerator.get_Current;

            return new (JSIL.AbstractEnumerable.Of(TSource))(
              function getNext(result) {
                  while (moveNext.Call(state.enumerator)) {
                      var current = get_Current.Call(state.enumerator);

                      if (predicate(current)) {
                          result.set(current);
                          return true;
                      }
                  }

                  return false;
              },
              function reset() {
                  state.enumerator = JSIL.GetEnumerator(source);
              },
              function dispose() {
                  JSIL.Dispose(state.enumerator);
              }
            );
        }
      );

      $.Method({ Static: true, Public: true }, "Where",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`3", ["!!0", $.Int32, $.Boolean])], ["TSource"]),
        function Where$b1(TSource, source, predicate) {
            var state = {};

            var moveNext = System.Collections.IEnumerator.MoveNext;
            var get_Current = System.Collections.IEnumerator.get_Current;
            var index = -1;

            return new (JSIL.AbstractEnumerable.Of(TSource))(
              function getNext(result) {
                  while (moveNext.Call(state.enumerator)) {
                      index++;
                      var current = get_Current.Call(state.enumerator);

                      if (predicate(current, index)) {
                          result.set(current);
                          return true;
                      }
                  }

                  return false;
              },
              function reset() {
                  state.enumerator = JSIL.GetEnumerator(source);
              },
              function dispose() {
                  JSIL.Dispose(state.enumerator);
              }
            );
        }
      );

      $.Method({ Static: true, Public: true }, "OrderBy",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.IOrderedEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])], ["TSource", "TKey"]),
        function OrderBy$b2(TSource, TKey, source, keySelector) {
            return new ($jsilcore.System.Linq.OrderedEnumerable$b2.Of(TSource, TKey))(source, keySelector, null, false);
        }
      );

      $.Method({ Static: true, Public: true }, "OrderBy",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.IOrderedEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"]), $jsilcore.TypeRef("System.Collections.Generic.IComparer`1", ["!!1"])], ["TSource", "TKey"]),
        function OrderBy$b2(TSource, TKey, source, keySelector, comparer) {
            return new ($jsilcore.System.Linq.OrderedEnumerable$b2.Of(TSource, TKey))(source, keySelector, comparer, false);
        }
      );

      $.Method({ Static: true, Public: true }, "OrderByDescending",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.IOrderedEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])], ["TSource", "TKey"]),
        function OrderBy$b2(TSource, TKey, source, keySelector) {
            return new ($jsilcore.System.Linq.OrderedEnumerable$b2.Of(TSource, TKey))(source, keySelector, null, true);
        }
      );

      $.Method({ Static: true, Public: true }, "OrderByDescending",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.IOrderedEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"]), $jsilcore.TypeRef("System.Collections.Generic.IComparer`1", ["!!1"])], ["TSource", "TKey"]),
        function OrderBy$b2(TSource, TKey, source, keySelector, comparer) {
            return new ($jsilcore.System.Linq.OrderedEnumerable$b2.Of(TSource, TKey))(source, keySelector, comparer, true);
        }
      );

      $.Method({ Static: true, Public: true }, "Range",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [$.Int32]), [$.Int32, $.Int32], []),
        function Range(start, count) {
            var nextValue = start;
            var left = count;

            return new (JSIL.AbstractEnumerable.Of(System.Int32.__Type__))(
              function getNext(result) {
                  if (left <= 0)
                      return false;

                  result.set(nextValue);

                  nextValue += 1;
                  left -= 1;

                  return true;
              },
              function reset() {
                  nextValue = start;
                  left = count;
              },
              function dispose() {
              }
            );
        }
      );

      $.Method({ Static: true, Public: true }, "Single",
      new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"]),
      Enumerable_Single$b1
    );

      function Enumerable_Single$b1(TSource, source) {
          var $im00 = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var $im01 = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          var $S00 = function () {
              return ($S00 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var $S01 = function () {
              return ($S01 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.InvalidOperationException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var $IM00 = function () {
              return ($IM00 = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
          };
          var $IM01 = function () {
              return ($IM01 = JSIL.Memoize(System.IDisposable.Dispose))();
          };

          if (source === null) {
              throw $S00().Construct("The source is null.");
          }
          var ok = false;
          var returnValue = (
            TSource.IsValueType
               ? JSIL['CreateInstanceOfType'](TSource)
               : null)
          ;
          var enumerator = $im00.Call(source, null);
          try {

              $loop0:
                  while ($IM00().Call(enumerator, null)) {
                      var element = $im01.Call(enumerator, null);
                      if (ok) {
                          ok = false;
                          break $loop0;
                      }
                      returnValue = element;
                      ok = true;
                  }
          } finally {
              if (enumerator !== null) {
                  $IM01().Call(enumerator, null);
              }
          }
          if (ok) {
              return returnValue;
          }
          throw $S01().Construct("The source contains no or multiple elements.");
      };

      $.Method({ Static: true, Public: true }, "Single",
      new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Boolean])], ["TSource"]),
      Stat_Single$b1
    );

      function Stat_Single$b1(TSource, source, predicate) {
          var $S00 = function () {
              return ($S00 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var $S01 = function () {
              return ($S01 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.InvalidOperationException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var $IM00 = function () {
              return ($IM00 = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
          };
          var $IM01 = function () {
              return ($IM01 = JSIL.Memoize(System.IDisposable.Dispose))();
          };

          var $im00 = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var $im01 = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          if (source === null) {
              throw $S00().Construct("The source is null.");
          }
          var ok = false;
          var returnValue = (
            TSource.IsValueType
               ? JSIL['CreateInstanceOfType'](TSource)
               : null)
          ;
          var enumerator = $im00.Call(source, null);
          try {

              $loop0:
                  while ($IM00().Call(enumerator, null)) {
                      var element = JSIL.CloneParameter(TSource, $im01.Call(enumerator, null));
                      if (!ok) {
                          if (predicate(JSIL.CloneParameter(TSource, element))) {
                              returnValue = JSIL.CloneParameter(TSource, element);
                              ok = true;
                          }
                      } else if (predicate(JSIL.CloneParameter(TSource, element))) {
                          ok = false;
                          break $loop0;
                      }
                  }
          } finally {
              if (enumerator !== null) {
                  $IM01().Call(enumerator, null);
              }
          }
          if (ok) {
              return returnValue;
          }
          throw $S01().Construct("The source contains no or multiple elements.");
      };


      $.Method({ Static: true, Public: true }, "DefaultIfEmpty",
      new JSIL.MethodSignature(
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]),
          [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])],
          ["TSource"]),
      function DefaultIfEmptyForDefaultValue$b1(T, source) {
          return DefaultIfEmpty$b1(T, source, JSIL.DefaultValue(T));
      }
    );
     

      $.Method({ Static: true, Public: true }, "DefaultIfEmpty",
        new JSIL.MethodSignature(
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]),
              [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), "!!0"],
          ["TSource"]),
        DefaultIfEmpty$b1
    );

      function DefaultIfEmpty$b1(T, source, defaultValue) {
          var enumerator = source.GetEnumerator();
          if (!enumerator.MoveNext()) {
              var $s00 = new JSIL.ConstructorSignature($asm_mscorlib.TypeRef("System.Collections.Generic.List`1", [T]), null);
              var $s01 = new JSIL.MethodSignature(null, [T]);
              var list = $s00.Construct();
              $s01.CallVirtual("Add", null, list, JSIL.CloneParameter(T, defaultValue));
              return list;
          }
          return source;
      }

      $.Method({ Static: true, Public: true }, "Sum",
        new JSIL.MethodSignature(
         $.Int32,
         [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [$.Int32])],
         []
      ),
      function Sum_Int32(enumerable) {
          var result = 0;

          var e = JSIL.GetEnumerator(enumerable, $jsilcore.System.Int32);

          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          var getCurrent = $jsilcore.System.Collections.Generic.IEnumerator$b1.Of($jsilcore.System.Int32).get_Current;

          try {
              while (moveNext.Call(e)) {
                  result = (result + getCurrent.Call(e)) | 0;
              }
          } finally {
              JSIL.Dispose(e);
          }

          return result;
      });

      $.Method({ Static: true, Public: true }, "Sum",
        new JSIL.MethodSignature(
         $.Single,
         [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [$.Single])],
         []
      ),
      function Sum_Single(enumerable) {
          var result = +0;

          var e = JSIL.GetEnumerator(enumerable, $jsilcore.System.Single);

          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          var getCurrent = $jsilcore.System.Collections.Generic.IEnumerator$b1.Of($jsilcore.System.Single).get_Current;

          try {
              while (moveNext.Call(e)) {
                  result = +(result + getCurrent.Call(e));
              }
          } finally {
              JSIL.Dispose(e);
          }

          return result;
      });

      $.Method({ Static: true, Public: true }, "Sum",
        new JSIL.MethodSignature(
         $.Double,
         [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [$.Double])],
         []
      ),
      function Sum_Double(enumerable) {
          var result = +0;

          var e = JSIL.GetEnumerator(enumerable, $jsilcore.System.Double);

          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          var getCurrent = $jsilcore.System.Collections.Generic.IEnumerator$b1.Of($jsilcore.System.Double).get_Current;

          try {
              while (moveNext.Call(e)) {
                  result = +(result + getCurrent.Call(e));
              }
          } finally {
              JSIL.Dispose(e);
          }

          return result;
      });


      $.Method({ Static: true, Public: true }, "AsEnumerable",
        new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"]),
        function AsEnumerable$b1(TSource, source) {
            return source;
        }
        );

      $.Method({ Static: true, Public: true }, "Distinct",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"]),
      Enumerable_Distinct$b1
    )

      function Enumerable_Distinct$b1(TSource, source) {
          var argNullException = function () {
              return (argNullException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var moveNext = function () {
              return (moveNext = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
          };
          var dispose = function () {
              return (dispose = JSIL.Memoize(System.IDisposable.Dispose))();
          };
          var collection = new JSIL.ConstructorSignature($jsilcore.TypeRef("System.Collections.ObjectModel.Collection`1", [TSource]), null);
          var getEnum = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var getCurrent = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          if (source === null) {
              throw argNullException().Construct("The source is null.");
          }
          var result = collection.Construct();
          var enumerator = getEnum.Call(source, null);
          try {

              while (moveNext().Call(enumerator, null)) {
                  var element = JSIL.CloneParameter(TSource, getCurrent.Call(enumerator, null));
                  if (!result['Contains'](JSIL.CloneParameter(TSource, element))) {
                      result['Add'](JSIL.CloneParameter(TSource, element));
                  }
              }
          } finally {
              if (enumerator !== null) {
                  dispose().Call(enumerator, null);
              }
          }
          return result;
      };

      $.Method({ Static: true, Public: true }, "Except",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"]),
      Enumerable_Except$b1
    );

      function Enumerable_Except$b1(TSource, first, second) {
          var enumerableType = function () {
              return (enumerableType = JSIL.Memoize(System.Linq.Enumerable))();
          };
          var argNullException = function () {
              return (argNullException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var $S01 = function () {
              return ($S01 = JSIL.Memoize(new JSIL.MethodSignature($jsilcore.TypeRef("System.Boolean"), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), "!!0"], ["TSource"])))();
          };
          var moveNext = function () {
              return (moveNext = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
          };
          var dispose = function () {
              return (dispose = JSIL.Memoize(System.IDisposable.Dispose))();
          };
          var getEnum = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var getCurrent = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          if (!((first !== null) && (second !== null))) {
              throw argNullException().Construct("First or second is null");
          }
          var result = new (System.Collections.Generic.HashSet2$b1.Of(TSource))();
          var enumerator = getEnum.Call(first, null);
          try {

              while (moveNext().Call(enumerator, null)) {
                  var elementFrom = JSIL.CloneParameter(TSource, getCurrent.Call(enumerator, null));
                  if (!(result['Contains'](JSIL.CloneParameter(TSource, elementFrom)) || $S01().CallStatic(enumerableType(), "Contains$b1", [TSource], second, JSIL.CloneParameter(TSource, elementFrom)))) {
                      result['Add'](JSIL.CloneParameter(TSource, elementFrom));
                  }
              }
          } finally {
              if (enumerator !== null) {
                  dispose().Call(enumerator, null);
              }
          }
          return result;
      };

      $.Method({ Static: true, Public: true }, "GroupBy",
    new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [$jsilcore.TypeRef("System.Linq.IGrouping`2", ["!!1", "!!0"])]),
      [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])],
      ["TSource", "TKey"]),
      function Enumerable_GroupBy$b2(TSource, TKey, source, keySelector) {
          return new (System.Linq.GroupedEnumerable$b3.Of(
            TSource, TKey,
            TSource
        ))(source, keySelector, function (o) { return o; }, null);
      }
      );

      $.Method({ Static: true, Public: true }, "GroupBy",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!2"]), [
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"]),
          $jsilcore.TypeRef("System.Func`3", [
              "!!1", $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]),
              "!!2"
          ])
      ], ["TSource", "TKey", "TResult"]),
      function Enumerable_GroupBy$b3(TSource, TKey, TResult, source, keySelector, resultSelector) {
          return new (System.Linq.GroupedEnumerable$b4.Of(
                TSource, TKey,
                TSource, TResult
            ))(source, keySelector, function (o) { return o; }, resultSelector, null);
      }
    );

      $.Method({Static:true , Public:true }, "Contains", 
      new JSIL.MethodSignature($.Boolean, [
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), "!!0", 
          $jsilcore.TypeRef("System.Collections.Generic.IEqualityComparer`1", ["!!0"])
      ], ["TSource"]), 
      Enumerable_Contains$b1
    );

      function Enumerable_Contains$b1 (TSource, source, value, comparer) {
          var $S00 = function () {
              return ($S00 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")]))) ();
          };
          var $IM00 = function () {
              return ($IM00 = JSIL.Memoize(System.Collections.IEnumerator.MoveNext)) ();
          };
          var $IM01 = function () {
              return ($IM01 = JSIL.Memoize(System.IDisposable.Dispose)) ();
          };
          var $im00 = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var $im01 = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          var $im02 = System.Collections.Generic.IEqualityComparer$b1.Of(TSource).GetHashCode;
          var $im03 = System.Collections.Generic.IEqualityComparer$b1.Of(TSource).Equals;
          if (source === null) {
              throw $S00().Construct("The source is null.");
          }
          var enumerator = $im00.Call(source, null);
          try {

              while ($IM00().Call(enumerator, null)) {
                  var element = JSIL.CloneParameter(TSource, $im01.Call(enumerator, null));
                  if (!((($im02.Call(comparer, null, JSIL.CloneParameter(TSource, element)) | 0) !== ($im02.Call(comparer, null, JSIL.CloneParameter(TSource, value)) | 0)) || !$im03.Call(comparer, null, JSIL.CloneParameter(TSource, element), JSIL.CloneParameter(TSource, value)))) {
                      var result = true;
                      return result;
                  }
              }
          } finally {
              if (enumerator !== null) {
                  $IM01().Call(enumerator, null);
              }
          }
          result = false;
          return result;
      };

      $.Method({ Static: true, Public: true }, "Aggregate",
      new JSIL.MethodSignature("!!1", [
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), "!!1",
          $jsilcore.TypeRef("System.Func`3", [
              "!!1", "!!0",
              "!!1"
          ])
      ], ["TSource", "TAccumulate"]),
      Enumerable_Aggregate$b2
    );

      function Enumerable_Aggregate$b2(TSource, TAccumulate, source, seed, func) {
          var $S00 = function () {
              return ($S00 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var $IM00 = function () {
              return ($IM00 = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
          };
          var $IM01 = function () {
              return ($IM01 = JSIL.Memoize(System.IDisposable.Dispose))();
          };
          var $im00 = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var $im01 = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          if (source === null) {
              throw $S00().Construct("Source is null.");
          }
          if (func === null) {
              throw $S00().Construct("func is null.");
          }
          var result = JSIL.CloneParameter(TAccumulate, seed);
          var enumerator = $im00.Call(source, null);
          try {

              while ($IM00().Call(enumerator, null)) {
                  var element = JSIL.CloneParameter(TSource, $im01.Call(enumerator, null));
                  result = JSIL.CloneParameter(TAccumulate, func(JSIL.CloneParameter(TAccumulate, result), JSIL.CloneParameter(TSource, element)));
              }
          } finally {
              if (enumerator !== null) {
                  $IM01().Call(enumerator, null);
              }
          }
          return result;
      };


      $.Method({ Static: true, Public: true }, "Intersect",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"]),
      Enumerable_Intersect$b1$00
    );

      function Enumerable_Intersect$b1$00 (TSource, first, second) {
          var $S00 = function () {
              return ($S00 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")]))) ();
          };
          var $IM00 = function () {
              return ($IM00 = JSIL.Memoize(System.Collections.IEnumerator.MoveNext)) ();
          };
          var $IM01 = function () {
              return ($IM01 = JSIL.Memoize(System.IDisposable.Dispose)) ();
          };
          var $im00 = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var $im01 = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          if (!((first !== null) && (second !== null))) {
              throw $S00().Construct("One of the sources is null");
          }
          var buffer = new (System.Collections.Generic.HashSet2$b1.Of(TSource)) ();
          var enumerator = $im00.Call(second, null);
          try {

              while ($IM00().Call(enumerator, null)) {
                  var element = JSIL.CloneParameter(TSource, $im01.Call(enumerator, null));
                  buffer['Add'](JSIL.CloneParameter(TSource, element));
              }
          } finally {
              if (enumerator !== null) {
                  $IM01().Call(enumerator, null);
              }
          }
          var result = new (System.Collections.Generic.HashSet2$b1.Of(TSource)) ();
          enumerator = $im00.Call(first, null);
          try {

              while ($IM00().Call(enumerator, null)) {
                  element = JSIL.CloneParameter(TSource, $im01.Call(enumerator, null));
                  if (!(result['Contains'](JSIL.CloneParameter(TSource, element)) || !buffer['Contains'](JSIL.CloneParameter(TSource, element)))) {
                      result['Add'](JSIL.CloneParameter(TSource, element));
                  }
              }
          } finally {
              if (enumerator !== null) {
                  $IM01().Call(enumerator, null);
              }
          }
          return result;
      };


      $.Method({ Static: true, Public: true }, "Intersect",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]),
          $jsilcore.TypeRef("System.Collections.Generic.IEqualityComparer`1", ["!!0"])
      ], ["TSource"]),
      Enumerable_Intersect$b1$01
    );


      function Enumerable_Intersect$b1$01 (TSource, first, second, comparer) {
          var $S00 = function () {
              return ($S00 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")]))) ();
          };
          var $S01 = function () {
              return ($S01 = JSIL.Memoize(new JSIL.MethodSignature($jsilcore.TypeRef("System.Boolean"), [
                  $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), "!!0",
                  $jsilcore.TypeRef("System.Collections.Generic.IEqualityComparer`1", ["!!0"])
              ], ["TSource"])))();
          };
          var $IM00 = function () {
              return ($IM00 = JSIL.Memoize(System.Collections.IEnumerator.MoveNext)) ();
          };
          var $IM01 = function () {
              return ($IM01 = JSIL.Memoize(System.IDisposable.Dispose)) ();
          };
          var $T04 = function () {
              return ($T04 = JSIL.Memoize(System.Linq.Enumerable))();
          };
          var $im00 = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var $im01 = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          if (!((first !== null) && (second !== null))) {
              throw $S00().Construct("One of the sources is null");
          }
          var buffer = new (System.Collections.Generic.HashSet2$b1.Of(TSource)) ();
          var enumerator = $im00.Call(second, null);
          try {

              while ($IM00().Call(enumerator, null)) {
                  var element = JSIL.CloneParameter(TSource, $im01.Call(enumerator, null));
                  buffer['Add'](JSIL.CloneParameter(TSource, element));
              }
          } finally {
              if (enumerator !== null) {
                  $IM01().Call(enumerator, null);
              }
          }
          var result = new (System.Collections.Generic.HashSet2$b1.Of(TSource)) ();
          enumerator = $im00.Call(first, null);
          try {

              while ($IM00().Call(enumerator, null)) {
                  element = JSIL.CloneParameter(TSource, $im01.Call(enumerator, null));
                  if (!(result['Contains'](JSIL.CloneParameter(TSource, element)) || !$S01().CallStatic($T04(), "Contains$b1", [TSource], buffer, JSIL.CloneParameter(TSource, element), comparer))) {
                      result['Add'](JSIL.CloneParameter(TSource, element));
                  }
              }
          } finally {
              if (enumerator !== null) {
                  $IM01().Call(enumerator, null);
              }
          }
          return result;
      };

      $.Method({ Static: true, Public: true }, "LastOrDefault",
      new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"]),
      Enumerable_LastOrDefault$b1
    );

      function Enumerable_LastOrDefault$b1(TSource, source) {
          var enumerable = function () {
              return (enumerable = JSIL.Memoize(System.Linq.Enumerable))();
          };
          var argNullException = function () {
              return (argNullException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var $S01 = function () {
              return ($S01 = JSIL.Memoize(new JSIL.MethodSignature($jsilcore.TypeRef("System.Int32"), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"])))();
          };

          if (source === null) {
              throw argNullException().Construct("Source is null.");
          }
          var count = ($S01().CallStatic(enumerable(), "Count$b1", [TSource], source) | 0);
          if (count === 0) {
              var result = (
                TSource.IsValueType
                   ? JSIL['CreateInstanceOfType'](TSource)
                   : null)
              ;
          } else {
              result = JSIL.CloneParameter(TSource, enumerable()['ElementAt$b1'](TSource)(source, ((count - 1) | 0)));
          }
          return result;
      };

      $.Method({ Static: true, Public: true }, "Max",
        new JSIL.MethodSignature(
         $.Int32,
         [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [$.Int32])],
         []
      ),
      function Max_Int32(enumerable) {
          var maxValue = -2147483648; //int.MinValue
          var amountOfElements = 0;

          var e = JSIL.GetEnumerator(enumerable, $jsilcore.System.Int32);

          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          var getCurrent = $jsilcore.System.Collections.Generic.IEnumerator$b1.Of($jsilcore.System.Int32).get_Current;

          try {
              while (moveNext.Call(e)) {
                  ((amountOfElements + 1) | 0);
                  var currentValue = getCurrent.Call(e);
                  if (maxValue < currentValue) {
                      maxValue = currentValue;
                  }
              }
          } finally {
              JSIL.Dispose(e);
          }

          return maxValue;
      });

      $.Method({ Static: true, Public: true }, "Max",
        new JSIL.MethodSignature(
         $.Single,
         [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [$.Single])],
         []
      ),
      function Max_Single(enumerable) {
          var maxValue = -2147483648; //int.MinValue
          var amountOfElements = 0;

          var e = JSIL.GetEnumerator(enumerable, $jsilcore.System.Single);

          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          var getCurrent = $jsilcore.System.Collections.Generic.IEnumerator$b1.Of($jsilcore.System.Single).get_Current;

          try {
              while (moveNext.Call(e)) {
                  ((amountOfElements + 1) | 0);
                  var currentValue = getCurrent.Call(e);
                  if (maxValue < currentValue) {
                      maxValue = currentValue;
                  }
              }
          } finally {
              JSIL.Dispose(e);
          }

          return maxValue;
      });

      $.Method({ Static: true, Public: true }, "Max",
        new JSIL.MethodSignature(
         $.Double,
         [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [$.Double])],
         []
      ),
      function Max_Double(enumerable) {
          var maxValue = -2147483648; //int.MinValue
          var amountOfElements = 0;

          var e = JSIL.GetEnumerator(enumerable, $jsilcore.System.Double);

          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          var getCurrent = $jsilcore.System.Collections.Generic.IEnumerator$b1.Of($jsilcore.System.Double).get_Current;

          try {
              while (moveNext.Call(e)) {
                  ((amountOfElements + 1) | 0);
                  var currentValue = getCurrent.Call(e);
                  if (maxValue < currentValue) {
                      maxValue = currentValue;
                  }
              }
          } finally {
              JSIL.Dispose(e);
          }

          return maxValue;
      });

      $.Method({ Static: true, Public: true }, "Max",
    new JSIL.MethodSignature($.Int32, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Int32])], ["TSource"]),
    Enumerable_Max$b1
  );

      $.Method({ Static: true, Public: true }, "Max",
    new JSIL.MethodSignature($.Double, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Double])], ["TSource"]),
    Enumerable_Max$b1
  );

      function Enumerable_Max$b1(TSource, source, selector) {
          var argNullException = function () {
              return (argNullException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var invalidOpException = function () {
              return (invalidOpException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.InvalidOperationException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var moveNext = function () {
              return (moveNext = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
          };
          var dispose = function () {
              return (dispose = JSIL.Memoize(System.IDisposable.Dispose))();
          };
          var getEnum = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var getCurrent = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          if (!((source !== null) && (selector !== null))) {
              throw argNullException().Construct("Source or selector is null.");
          }
          var maxValue = -2147483648;
          var amountOfElements = 0;
          var enumerator = getEnum.Call(source, null);
          try {

              while (moveNext().Call(enumerator, null)) {
                  var element = JSIL.CloneParameter(TSource, getCurrent.Call(enumerator, null));
                  amountOfElements = ((amountOfElements + 1) | 0);
                  var elementValue = selector(JSIL.CloneParameter(TSource, element));
                  if (elementValue > maxValue) {
                      maxValue = elementValue;
                  }
              }
          } finally {
              if (enumerator !== null) {
                  dispose().Call(enumerator, null);
              }
          }
          if (amountOfElements === 0) {
              throw invalidOpException().Construct("Source contains no elements.");
          }
          return maxValue;
      };

      $.Method({ Static: true, Public: true }, "Max",
      new JSIL.MethodSignature("!!1", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])], ["TSource", "TResult"]),
      Enumerable_Max$b2
    );

      function Enumerable_Max$b2(TSource, TResult, source, selector) {
          var $S00 = function () {
              return ($S00 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var $S01 = function () {
              return ($S01 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.InvalidOperationException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var $IM00 = function () {
              return ($IM00 = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
          };
          var $IM01 = function () {
              return ($IM01 = JSIL.Memoize(System.IDisposable.Dispose))();
          };
          var $im00 = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var $im01 = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          if (source === null) {
              throw $S00().Construct("Source is null.");
          }
          if (selector === null) {
              throw $S00().Construct("Selector is null.");
          }
          var first = true;
          var maxValue = (
            TResult.IsValueType
               ? JSIL['CreateInstanceOfType'](TResult)
               : null)
          ;
          var amountOfElements = 0;
          var defaultComparer = System.Collections.Generic.Comparer$b1.Of(TResult)['get_Default']();
          var enumerator = $im00.Call(source, null);
          try {

              while ($IM00().Call(enumerator, null)) {
                  var element = JSIL.CloneParameter(TSource, $im01.Call(enumerator, null));
                  amountOfElements = ((amountOfElements + 1) | 0);
                  var elementValue = JSIL.CloneParameter(TResult, selector(JSIL.CloneParameter(TSource, element)));
                  if (!(!first && ((defaultComparer['Compare'](JSIL.CloneParameter(TResult, elementValue), JSIL.CloneParameter(TResult, maxValue)) | 0) <= 0))) {
                      maxValue = elementValue;
                      first = false;
                  }
              }
          } finally {
              if (enumerator !== null) {
                  $IM01().Call(enumerator, null);
              }
          }
          if (amountOfElements === 0) {
              throw $S01().Construct("Source contains no elements.");
          }
          return maxValue;
      };

      $.Method({ Static: true, Public: true }, "Min",
      new JSIL.MethodSignature($.Int32, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [$.Int32])], []),
      function Min_Int32(enumerable) {
          var minValue = 2147483647; //int.MaxValue

          var e = JSIL.GetEnumerator(enumerable, $jsilcore.System.Int32);

          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          var getCurrent = $jsilcore.System.Collections.Generic.IEnumerator$b1.Of($jsilcore.System.Int32).get_Current;

          try {
              while (moveNext.Call(e)) {
                  var currentValue = getCurrent.Call(e);
                  if (minValue > currentValue) {
                      minValue = currentValue;
                  }
              }
          } finally {
              JSIL.Dispose(e);
          }

          return minValue;
      });

      $.Method({ Static: true, Public: true }, "Min",
      new JSIL.MethodSignature($.Single, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [$.Single])], []),
      function Min_Single(enumerable) {
          var minValue = 2147483647; //int.MaxValue

          var e = JSIL.GetEnumerator(enumerable, $jsilcore.System.Single);

          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          var getCurrent = $jsilcore.System.Collections.Generic.IEnumerator$b1.Of($jsilcore.System.Single).get_Current;

          try {
              while (moveNext.Call(e)) {
                  var currentValue = getCurrent.Call(e);
                  if (minValue > currentValue) {
                      minValue = currentValue;
                  }
              }
          } finally {
              JSIL.Dispose(e);
          }

          return minValue;
      });

      $.Method({ Static: true, Public: true }, "Min",
      new JSIL.MethodSignature($.Double, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [$.Double])], []),
      function Min_Double(enumerable) {
          var minValue = 2147483647; //int.MaxValue

          var e = JSIL.GetEnumerator(enumerable, $jsilcore.System.Double);

          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          var getCurrent = $jsilcore.System.Collections.Generic.IEnumerator$b1.Of($jsilcore.System.Double).get_Current;

          try {
              while (moveNext.Call(e)) {
                  var currentValue = getCurrent.Call(e);
                  if (minValue > currentValue) {
                      minValue = currentValue;
                  }
              }
          } finally {
              JSIL.Dispose(e);
          }

          return minValue;
      });

      $.Method({ Static: true, Public: true }, "Min",
      new JSIL.MethodSignature($.Int32, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Int32])], ["TSource"]),
      Enumerable_Min$b1
    );

      $.Method({ Static: true, Public: true }, "Min",
      new JSIL.MethodSignature($.Double, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Double])], ["TSource"]),
      Enumerable_Min$b1
    );

      function Enumerable_Min$b1(TSource, source, selector) {
          var ArgNullException = function () {
              return (ArgNullException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var InvalidOpException = function () {
              return (InvalidOpException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.InvalidOperationException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var moveNext = $jsilcore.System.Collections.IEnumerator.MoveNext;
          
          var dispose = function () {
              return (dispose = JSIL.Memoize(System.IDisposable.Dispose))();
          };
          var getEnum = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var e = JSIL.GetEnumerator(source, $jsilcore.System.Int32);

          var getCurrent = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          if (!((source !== null) && (selector !== null))) {
              throw ArgNullException().Construct("Source or selector is null.");
          }
          var minValue = 2147483647; //int.MaxValue
          var amountOfElements = 0;
          var enumerator = getEnum.Call(source, null);
          try {

              while (moveNext.Call(e)) {
                  var element = JSIL.CloneParameter(TSource, getCurrent.Call(e));
                  amountOfElements = ((amountOfElements + 1) | 0);
                  var elementValue = selector(JSIL.CloneParameter(TSource, element)); //the line below is not compatible with double
                  //var elementValue = (selector(JSIL.CloneParameter(TSource, element)) | 0);
                  if (elementValue < minValue) {
                      minValue = elementValue;
                  }
              }
          } finally {
              if (enumerator !== null) {
                  dispose().Call(enumerator, null);
                  dispose
              }
              if (amountOfElements === 0) {
                  throw InvalidOpException().Construct("Source contains no elements.");
              }
              return minValue;
          };
      };

      $.Method({ Static: true, Public: true }, "Min",
      new JSIL.MethodSignature("!!1", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])], ["TSource", "TResult"]),
      Enumerable_Min$b2
    );

      function Enumerable_Min$b2(TSource, TResult, source, selector) {
          var $S00 = function () {
              return ($S00 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var $S01 = function () {
              return ($S01 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.InvalidOperationException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var $IM00 = function () {
              return ($IM00 = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
          };
          var $IM01 = function () {
              return ($IM01 = JSIL.Memoize(System.IDisposable.Dispose))();
          };
          var $im00 = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var $im01 = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          if (source === null) {
              throw $S00().Construct("Source is null.");
          }
          if (selector === null) {
              throw $S00().Construct("Selector is null.");
          }
          var first = true;
          var minValue = (
            TResult.IsValueType
               ? JSIL['CreateInstanceOfType'](TResult)
               : null)
          ;
          var amountOfElements = 0;
          var defaultComparer = System.Collections.Generic.Comparer$b1.Of(TResult)['get_Default']();
          var enumerator = $im00.Call(source, null);
          try {

              while ($IM00().Call(enumerator, null)) {
                  var element = JSIL.CloneParameter(TSource, $im01.Call(enumerator, null));
                  amountOfElements = ((amountOfElements + 1) | 0);
                  var elementValue = JSIL.CloneParameter(TResult, selector(JSIL.CloneParameter(TSource, element)));
                  if (!(!first && ((defaultComparer['Compare'](JSIL.CloneParameter(TResult, elementValue), JSIL.CloneParameter(TResult, minValue)) | 0) >= 0))) {
                      minValue = elementValue;
                      first = false;
                  }
              }
          } finally {
              if (enumerator !== null) {
                  $IM01().Call(enumerator, null);
              }
          }
          if (amountOfElements === 0) {
              throw $S01().Construct("Source contains no elements.");
          }
          return minValue;
      };


      $.Method({ Static: true, Public: true }, "Sum",
     new JSIL.MethodSignature($.Int32, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Int32])], ["TSource"]),
     Enumerable_SumInt$b1
   )

      function Enumerable_SumInt$b1(TSource, source, selector) {
          var argNullException = function () {
              return (argNullException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
          };
          var moveNext = function () {
              return (moveNext = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
          };
          var dispose = function () {
              return (dispose = JSIL.Memoize(System.IDisposable.Dispose))();
          };
          var getEnum = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
          var getCurrent = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
          if (source === null) {
              throw argNullException().Construct("The source is null");
          }
          if (selector === null) {
              throw argNullException().Construct("The selector is null");
          }
          var result = 0;
          var enumerator = getEnum.Call(source, null);
          try {

              while (moveNext().Call(enumerator, null)) {
                  var element = JSIL.CloneParameter(TSource, getCurrent.Call(enumerator, null));
                  result = ((result + (selector(JSIL.CloneParameter(TSource, element)) | 0)) | 0);
              }
          } finally {
              if (enumerator !== null) {
                  dispose().Call(enumerator, null);
              }
          }
          return result;
      };

       $.Method({Static:true , Public:true }, "ThenBy", 
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.IOrderedEnumerable`1", ["!!0"]), [
          $jsilcore.TypeRef("System.Linq.IOrderedEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"]),
          $jsilcore.TypeRef("System.Collections.Generic.IComparer`1", ["!!1"])
        ], ["TSource", "TKey"]), 
      function Enumerator_ThenBy$b2(TSource, TKey, source, keySelector, comparer) {
          if (!(!((source === null) ||
                (keySelector === null)))) {
              throw $S00().Construct("The source or the keySelector is null.");
          }
          return new ($jsilcore.System.Linq.OrderedEnumerable$b2.Of(TSource, TKey))(source, keySelector, comparer, false);
      }
    )

       $.Method({ Static: true, Public: true }, "ThenBy",
       new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.IOrderedEnumerable`1", ["!!0"]), [
           $jsilcore.TypeRef("System.Linq.IOrderedEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])
       ], ["TSource", "TKey"]),
       function Enumerator_ThenBy$b2(TSource, TKey, source, keySelector) {
           if (!(!((source === null) ||
                 (keySelector === null)))) {
               throw $S00().Construct("The source or the keySelector is null.");
           }
           return new ($jsilcore.System.Linq.OrderedEnumerable$b2.Of(TSource, TKey))(source, keySelector, null, false);
       }
     )

      //function Enumerator_ThenBy$b2(TSource, TKey, source, keySelector, comparer) {
      //    if (!(!((source === null) ||
      //          (keySelector === null)) && (comparer !== null))) {
      //        throw $S00().Construct("The source, the keySelector or the comparer is null.");
      //    }
      //    var $im00 = System.Linq.IOrderedEnumerable$b1.Of(TSource).CreateOrderedEnumerable$b1;
      //    return $im00.Call(source, [TKey], keySelector, comparer, false);

      //    //below comes from another class made as a test
      //    //var $im00 = System.Linq.IOrderedEnumerable$b1.Of(TSource).CreateOrderedEnumerable$b1;
      //    //return $im00.Call(source, [TKey], keySelector, comparer, false);


      //    //return new (System.Linq.OrderedEnumerable$b2.Of(TSource, TKey))(source, keySelector, comparer, false);
      //};

          $.Method({ Static: true, Public: true }, "ToDictionary",
          new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.Dictionary`2", ["!!1", "!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])], ["TSource", "TKey"]),
          function Enumerable_ToDictionary$b2(TSource, TKey, source, keySelector) {
              return Enumerable_ToDictionary$b3(TSource, TKey, TSource, source, keySelector, function (o) { return o; });
              //return $thisType['ToDictionary$b3'](TSource, TKey, TSource)(source, keySelector, System.Func$b2.Of(TSource, TSource)['New']($thisType, $thisType['$lToDictionary$gb__1$b2'](TSource, TKey), function () { return JSIL.GetMethodInfo($thisType, "$lToDictionary$gb__1", new JSIL.MethodSignature("!!0", ["!!0"], ["TSource", "TKey"]), true, [TSource, TKey]); }));
          });

          $.Method({ Static: true, Public: true }, "ToDictionary",
          new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.Dictionary`2", ["!!1", "!!2"]), [
              $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"]),
              $jsilcore.TypeRef("System.Func`2", ["!!0", "!!2"])
          ], ["TSource", "TKey", "TElement"]),
          Enumerable_ToDictionary$b3
        )

          function Enumerable_ToDictionary$b3(TSource, TKey, TElement, source, keySelector, elementSelector) {
              var ArgNullException = function () {
                  return (ArgNullException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
              };
              var ArgException = function () {
                  return (ArgException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentException"), [$jsilcore.TypeRef("System.String")])))();
              };
              var moveNext = function () {
                  return (moveNext = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
              };
              var dispose = function () {
                  return (dispose = JSIL.Memoize(System.IDisposable.Dispose))();
              };
              var dicConst = new JSIL.ConstructorSignature($jsilcore.TypeRef("System.Collections.Generic.Dictionary`2", [TKey, TElement]), null);
              var getEnumerator = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
              var getCurrent = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
              if (!(!((source === null) ||
                    (keySelector === null)) && (elementSelector !== null))) {
                  throw ArgNullException().Construct("source or keySelector or elementSelector is null.");
              }
              var dictionary = dicConst.Construct();
              var enumerator = getEnumerator.Call(source, null);
              try {

                  while (moveNext().Call(enumerator, null)) {
                      var sourceElement = JSIL.CloneParameter(TSource, getCurrent.Call(enumerator, null));
                      var key = JSIL.CloneParameter(TKey, keySelector(JSIL.CloneParameter(TSource, sourceElement)));
                      if (key === null) {
                          throw ArgNullException().Construct("The keySelector produces a key that is null.");
                      }
                      if (dictionary['ContainsKey'](JSIL.CloneParameter(TKey, key))) {
                          throw ArgException().Construct("The keySelector produces duplicate keys for two elements.");
                      }
                      var element = JSIL.CloneParameter(TElement, elementSelector(JSIL.CloneParameter(TSource, sourceElement)));
                      dictionary['Add'](JSIL.CloneParameter(TKey, key), JSIL.CloneParameter(TElement, element));
                  }
              } finally {
                  if (enumerator !== null) {
                      dispose().Call(enumerator, null);
                  }
              }
              return dictionary;
          };

          $.Method({ Static: true, Public: true }, "SingleOrDefault",
          new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Boolean])], ["TSource"]),
          Enumerable_SingleOrDefault$b1
        )

          function Enumerable_SingleOrDefault$b1(TSource, source, predicate) {
              var ArgNullException = function () {
                  return (ArgNullException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
              };
              var ArgException = function () {
                  return (ArgException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentException"), [$jsilcore.TypeRef("System.String")])))();
              };
              var moveNext = function () {
                  return (moveNext = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
              };
              var dispose = function () {
                  return (dispose = JSIL.Memoize(System.IDisposable.Dispose))();
              };
              var getEnum = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
              var getCurrent = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
              if (!((source !== null) && (predicate !== null))) {
                  throw ArgNullException().Construct("The source or the predicate is null.");
              }
              var returnValue = (
                TSource.IsValueType
                   ? JSIL['CreateInstanceOfType'](TSource)
                   : null)
              ;
              var enumerator = getEnum.Call(source, null);
              try {

                  while (moveNext().Call(enumerator, null)) {
                      var element = JSIL.CloneParameter(TSource, getCurrent.Call(enumerator, null));
                      if (predicate(JSIL.CloneParameter(TSource, element))) {
                          if (false) {
                              throw ArgException().Construct("More than one element satisfies the condition.");
                          }
                          returnValue = element;
                      }
                  }
              } finally {
                  if (enumerator !== null) {
                      dispose().Call(enumerator, null);
                  }
              }
              return returnValue;
          };

          $.Method({ Static: true, Public: true }, "SingleOrDefault",
          new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"]),
          Stat_SingleOrDefault$b1
        );
          function Stat_SingleOrDefault$b1(TSource, source) {
              var $S00 = function () {
                  return ($S00 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
              };
              var $S01 = function () {
                  return ($S01 = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.InvalidOperationException"), [$jsilcore.TypeRef("System.String")])))();
              };
              var $IM00 = function () {
                  return ($IM00 = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
              };
              var $IM01 = function () {
                  return ($IM01 = JSIL.Memoize(System.IDisposable.Dispose))();
              };
              var $im00 = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
              var $im01 = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
              if (source === null) {
                  throw $S00().Construct("The source is null.");
              }
              var ok = true;
              var returnValue = (
                TSource.IsValueType
                   ? JSIL['CreateInstanceOfType'](TSource)
                   : null)
              ;
              var enumerator = $im00.Call(source, null);
              try {

                  while ($IM00().Call(enumerator, null)) {
                      var element = $im01.Call(enumerator, null);
                      if (!ok) {
                          throw $S01().Construct("The source too many elements.");
                      }
                      returnValue = element;
                      ok = false;
                  }
              } finally {
                  if (enumerator !== null) {
                      $IM01().Call(enumerator, null);
                  }
              }
              return returnValue;
          };



          $.Method({ Static: true, Public: true }, "Skip",
          new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $.Int32], ["TSource"]),
          Enumerable_Skip$b1
        )

          function Enumerable_Skip$b1(TSource, source, count) {
              var argNullException = function () {
                  return (argNullException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
              };
              var moveNext = function () {
                  return (moveNext = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
              };
              var dispose = function () {
                  return (dispose = JSIL.Memoize(System.IDisposable.Dispose))();
              };
              var listConst = new JSIL.ConstructorSignature($jsilcore.TypeRef("System.Collections.Generic.List`1", [TSource]), null);
              var getEnum = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
              var getCurrent = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
              if (source === null) {
                  throw argNullException().Construct("source is null");
              }
              var currentIndex = 0;
              var result = listConst.Construct();
              var enumerator = getEnum.Call(source, null);
              try {

                  while (moveNext().Call(enumerator, null)) {
                      var element = JSIL.CloneParameter(TSource, getCurrent.Call(enumerator, null));
                      if (currentIndex !== (count | 0)) {
                          currentIndex = ((currentIndex + 1) | 0);
                      } else {
                          result['Add'](JSIL.CloneParameter(TSource, element));
                      }
                  }
              } finally {
                  if (enumerator !== null) {
                      dispose().Call(enumerator, null);
                  }
              }
              return result;
          };


          $.Method({ Static: true, Public: true }, "Take",
          new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $.Int32], ["TSource"]),
          Enumerable_Take$b1
        )

          function Enumerable_Take$b1(TSource, source, count) {
              var argNullException = function () {
                  return (argNullException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
              };
              var moveNext = function () {
                  return (moveNext = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
              };
              var dispose = function () {
                  return (dispose = JSIL.Memoize(System.IDisposable.Dispose))();
              };
              var list = new JSIL.ConstructorSignature($jsilcore.TypeRef("System.Collections.Generic.List`1", [TSource]), null);
              var getEnum = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
              var getCurrent = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
              if (source === null) {
                  throw argNullException().Construct("source is null");
              }
              var i = 0;
              var result = list.Construct();
              var enumerator = getEnum.Call(source, null);
              try {

                  $loop0:
                      while (moveNext().Call(enumerator, null)) {
                          var element = JSIL.CloneParameter(TSource, getCurrent.Call(enumerator, null));
                          if (i >= (count | 0)) {
                              break $loop0;
                          }
                          i = ((i + 1) | 0);
                          result['Add'](JSIL.CloneParameter(TSource, element));
                      }
              } finally {
                  if (enumerator !== null) {
                      dispose().Call(enumerator, null);
                  }
              }
              return result;
          };
      
          $.Method({ Static: true, Public: true }, "Union",
          new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"]),
          Enumerable_Union$b1
        )

          function Enumerable_Union$b1(TSource, first, second) {
              var argNullException = function () {
                  return (argNullException = JSIL.Memoize(new JSIL.ConstructorSignature($jsilcore.TypeRef("System.ArgumentNullException"), [$jsilcore.TypeRef("System.String")])))();
              };
              var moveNext = function () {
                  return (moveNext = JSIL.Memoize(System.Collections.IEnumerator.MoveNext))();
              };
              var dispose = function () {
                  return (dispose = JSIL.Memoize(System.IDisposable.Dispose))();
              };
              var getEnum = System.Collections.Generic.IEnumerable$b1.Of(TSource).GetEnumerator;
              var getCurrent = System.Collections.Generic.IEnumerator$b1.Of(TSource).get_Current;
              if (!((first !== null) && (second !== null))) {
                  throw argNullException().Construct("One or both of the sources is null.");
              }
              var result = new (System.Collections.Generic.HashSet2$b1.Of(TSource))();
              var enumerator = getEnum.Call(first, null);
              try {

                  while (moveNext().Call(enumerator, null)) {
                      var element = JSIL.CloneParameter(TSource, getCurrent.Call(enumerator, null));
                      if (!result['Contains'](JSIL.CloneParameter(TSource, element))) {
                          result['Add'](JSIL.CloneParameter(TSource, element));
                      }
                  }
              } finally {
                  if (enumerator !== null) {
                      dispose().Call(enumerator, null);
                  }
              }
              enumerator = getEnum.Call(second, null);
              try {

                  while (moveNext().Call(enumerator, null)) {
                      element = JSIL.CloneParameter(TSource, getCurrent.Call(enumerator, null));
                      if (!result['Contains'](JSIL.CloneParameter(TSource, element))) {
                          result['Add'](JSIL.CloneParameter(TSource, element));
                      }
                  }
              } finally {
                  if (enumerator !== null) {
                      dispose().Call(enumerator, null);
                  }
              }
              return result;
          };

  });

JSIL.MakeStaticClass("System.Linq.Enumerable", true, [], function ($) {

    $.ExternalMethod({ Static: true, Public: true }, "Any",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Boolean"), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Any",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Boolean"), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $jsilcore.TypeRef("System.Boolean")])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Count",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Int32"), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "First",
      new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Select",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!1"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])], ["TSource", "TResult"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Select",
  new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!1"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`3", ["!!0", $.Int32, "!!1"])], ["TSource", "TResult"])
  );

    $.ExternalMethod({ Static: true, Public: true }, "ToArray",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Array", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Except",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"])
    );


    $.ExternalMethod({ Static: true, Public: true }, "Distinct",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "GroupBy",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [$jsilcore.TypeRef("System.Linq.IGrouping`2", ["!!1", "!!0"])]),
        [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])],
        ["TSource", "TKey"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "GroupBy",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!2"]), [
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"]),
          $jsilcore.TypeRef("System.Func`3", [
              "!!1", $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]),
              "!!2"
          ])
      ], ["TSource", "TKey", "TResult"])
    );

    $.ExternalMethod({Static:true , Public:true }, "Contains", 
      new JSIL.MethodSignature($.Boolean, [
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), "!!0", 
          $jsilcore.TypeRef("System.Collections.Generic.IEqualityComparer`1", ["!!0"])
      ], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Aggregate",
      new JSIL.MethodSignature("!!1", [
          $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), "!!1",
          $jsilcore.TypeRef("System.Func`3", [
              "!!1", "!!0",
              "!!1"
          ])
      ], ["TSource", "TAccumulate"])
    )

    $.ExternalMethod({ Static: true, Public: true }, "Intersect",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Intersect",
    new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [
        $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]),
        $jsilcore.TypeRef("System.Collections.Generic.IEqualityComparer`1", ["!!0"])
    ], ["TSource"])
  );

    $.ExternalMethod({ Static: true, Public: true }, "LastOrDefault",
      new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Single",
      new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Single",
      new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Boolean])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Sum",
      new JSIL.MethodSignature($.Int32, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Int32])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Max",
      new JSIL.MethodSignature($.Int32, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Int32])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Max",
      new JSIL.MethodSignature($.Double, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Double])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Max",
      new JSIL.MethodSignature("!!1", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])], ["TSource", "TResult"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Min",
      new JSIL.MethodSignature($.Int32, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Int32])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Min",
      new JSIL.MethodSignature($.Double, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Double])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Min",
      new JSIL.MethodSignature("!!1", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])], ["TSource", "TResult"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "ThenBy",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.IOrderedEnumerable`1", ["!!0"]), [
          $jsilcore.TypeRef("System.Linq.IOrderedEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"]),
          $jsilcore.TypeRef("System.Collections.Generic.IComparer`1", ["!!1"])
      ], ["TSource", "TKey"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "ThenBy",
       new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.IOrderedEnumerable`1", ["!!0"]), [
           $jsilcore.TypeRef("System.Linq.IOrderedEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])
       ], ["TSource", "TKey"])
     );

    $.ExternalMethod({ Static: true, Public: true }, "ToDictionary",
    new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.Dictionary`2", ["!!1", "!!2"]), [
            $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"]),
            $jsilcore.TypeRef("System.Func`2", ["!!0", "!!2"])],
        ["TSource", "TKey", "TElement"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "ToDictionary",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.Dictionary`2", ["!!1", "!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", "!!1"])], ["TSource", "TKey"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "SingleOrDefault",
      new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Func`2", ["!!0", $.Boolean])], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "SingleOrDefault",
          new JSIL.MethodSignature("!!0", [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"])
        );

    $.ExternalMethod({ Static: true, Public: true }, "Skip",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $.Int32], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Take",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $.Int32], ["TSource"])
    );

    $.ExternalMethod({ Static: true, Public: true }, "Union",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"])], ["TSource"])
    );
});


JSIL.MakeClass($jsilcore.TypeRef("System.Object"), "System.Linq.Expressions.Expression", true, [], function ($) {
    var $thisType = $.publicInterface;

    $.ExternalMethod({ Static: true, Public: true }, "Constant",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.Expressions.ConstantExpression"), [$.Object], []))
    );

    $.ExternalMethod({ Static: true, Public: true }, "Constant",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.Expressions.ConstantExpression"), [$.Object, $jsilcore.TypeRef("System.Type")], []))
    );

    $.ExternalMethod({ Static: true, Public: true }, "Lambda",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.Expressions.Expression`1", ["!!0"]), [$jsilcore.TypeRef("System.Linq.Expressions.Expression"), $jsilcore.TypeRef("System.Array", [$jsilcore.TypeRef("System.Linq.Expressions.ParameterExpression")])], ["TDelegate"]))
    );
});

JSIL.ImplementExternals("System.Linq.Expressions.Expression", function ($) {
    $.Method({ Static: true, Public: true }, "Constant",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.Expressions.ConstantExpression"), [$.Object], [])),
      function Constant(value) {
          return new System.Linq.Expressions.ConstantExpression(value);
      }
    );

    $.Method({ Static: true, Public: true }, "Constant",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.Expressions.ConstantExpression"), [$.Object, $jsilcore.TypeRef("System.Type")], [])),
      function Constant(value, type) {
          return System.Linq.Expressions.ConstantExpression.Make(value, type);
      }
    );

    var $TParameterExpressionEnumerable = function () {
        return ($TParameterExpressionEnumerable = JSIL.Memoize($jsilcore.System.Collections.Generic.IEnumerable$b1.Of($jsilcore.System.Linq.Expressions.ParameterExpression)))();
    };

    $.Method({ Static: true, Public: true }, "Lambda",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.Expressions.Expression`1", ["!!0"]), [$jsilcore.TypeRef("System.Linq.Expressions.Expression"), $jsilcore.TypeRef("System.Array", [$jsilcore.TypeRef("System.Linq.Expressions.ParameterExpression")])], ["TDelegate"])),
      function Lambda$b1(TDelegate, body, parameters) {
          var name = null;
          var tailCall = false;
          return new (System.Linq.Expressions.Expression$b1.Of(TDelegate))(body, name, tailCall, $TParameterExpressionEnumerable().$Cast(parameters));
      }
    );

    $.Method({ Static: true, Public: true }, "Parameter",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.Expressions.ParameterExpression"), [$jsilcore.TypeRef("System.Type")], []),
      function Parameter(type) {
          return System.Linq.Expressions.ParameterExpression.Make(type, null, type.IsByRef);
      }
    );

    $.Method({ Static: true, Public: true }, "Parameter",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.Expressions.ParameterExpression"), [$jsilcore.TypeRef("System.Type"), $.String], []),
      function Parameter(type, name) {
          return System.Linq.Expressions.ParameterExpression.Make(type, name, type.IsByRef);
      }
    );
});

JSIL.MakeClass($jsilcore.TypeRef("System.Linq.Expressions.Expression"), "System.Linq.Expressions.ConstantExpression", true, [], function ($) {
    var $thisType = $.publicInterface;

    $.ExternalMethod({ Static: true, Public: false }, "Make",
      (new JSIL.MethodSignature($.Type, [$.Object, $jsilcore.TypeRef("System.Type")], []))
    );
});

JSIL.MakeClass($jsilcore.TypeRef("System.Linq.Expressions.ConstantExpression"), "System.Linq.Expressions.TypedConstantExpression", true, [], function ($) {
    var $thisType = $.publicInterface;
});

JSIL.ImplementExternals("System.Linq.Expressions.ConstantExpression", function ($) {
    $.Method({ Static: false, Public: false }, ".ctor",
      (new JSIL.MethodSignature(null, [$.Object], [])),
      function _ctor(value) {
          this._value = value;
      }
    );

    $.Method({ Static: true, Public: false }, "Make",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.Expressions.ConstantExpression"), [$.Object, $jsilcore.TypeRef("System.Type")], [])),
      function Make(value, type) {
          if (value == null && type == $jsilcore.System.Object.__Type__ || value != null && JSIL.GetType(value) == type) {
              return new System.Linq.Expressions.ConstantExpression(value);
          } else {
              return new System.Linq.Expressions.TypedConstantExpression(value, type);
          }
      }
    );
});

JSIL.ImplementExternals("System.Linq.Expressions.TypedConstantExpression", function ($) {
    $.Method({ Static: false, Public: false }, ".ctor",
      (new JSIL.MethodSignature(null, [$.Object, $jsilcore.TypeRef("System.Type")], [])),
      function _ctor(value, type) {
          this._value = value;
          this._type = type;
      }
    );

    $.Method({ Static: false, Public: true }, "get_Type",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Type"), [], []),
      function get_Type() {
          return this._type;
      }
    );
});

JSIL.MakeClass($jsilcore.TypeRef("System.Linq.Expressions.Expression"), "System.Linq.Expressions.ParameterExpression", true, [], function ($) {
    var $thisType = $.publicInterface;
});

JSIL.MakeClass($jsilcore.TypeRef("System.Linq.Expressions.Expression"), "System.Linq.Expressions.LambdaExpression", true, [], function ($) {
    var $thisType = $.publicInterface;
});

JSIL.MakeClass($jsilcore.TypeRef("System.Linq.Expressions.LambdaExpression"), "System.Linq.Expressions.Expression`1", true, ["TDelegate"], function ($) {
    var $thisType = $.publicInterface;
});

JSIL.ImplementExternals("System.Linq.Expressions.Expression`1", function ($) {
});


JSIL.ImplementExternals("System.Linq.Expressions.ParameterExpression", function ($) {
    $.Method({ Static: true, Public: false }, "Make",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Linq.Expressions.ParameterExpression"), [
          $jsilcore.TypeRef("System.Type"), $.String,
          $.Boolean
      ], []),
      function Make(type, name, isByRef) {
          var experession = new System.Linq.Expressions.ParameterExpression(name);
          experession._type = type;
          experession._isByRef = isByRef;
          return experession;
      }
    );

    $.Method({ Static: false, Public: true }, "get_Type",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Type"), [], []),
      function get_Type() {
          return this._type;
      }
    );

    $.Method({ Static: false, Public: true }, "get_IsByRef",
      new JSIL.MethodSignature($jsilcore.TypeRef("System.Boolean"), [], []),
      function get_IsByRef() {
          return this._isByRef;
      }
    );
});

JSIL.MakeInterface(
  "System.Linq.IGrouping`2", true, ["out TKey", "out TElement"], function ($) {
      $.Method({}, "get_Key", new JSIL.MethodSignature($.GenericParameter("TKey").out(), null));
      $.Property({}, "Key");
  }, [$jsilcore.TypeRef("System.Collections.Generic.IEnumerable`1", [new JSIL.GenericParameter("TElement", "System.Linq.IGrouping`2").out()]), $jsilcore.TypeRef("System.Collections.IEnumerable")]);