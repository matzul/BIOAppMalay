"use strict";
"use strict";

if (typeof (JSIL) === "undefined")
    throw new Error("JSIL.Core is required");

if (!$jsilcore)
    throw new Error("JSIL.Core is required");

JSIL.ImplementExternals(
  "System.TimeSpan", function ($) {
      System.DateTime["MaxValue"] = new System.DateTime(System.Int64.Create(3620863, 2651636, 11210));//Note: if we find a better place to put this, move it.
      //This (above) is used to set the correct value to DateTime.MaxValue (for some reason, is was the same as DateTime.MinValue).
      //    The only reason this has been put in TimeSpan is that we cannot put it directly in DateTime since this triggers the building of TimeSpan, creating a loop, which causes an error.
      //    We also cannot put it outside of everything, otherwise it causes problems with Int64, which we use in that call.

      var TicksPerMillisecond, TicksPerSecond, TicksPerMinute, TicksPerHour, TicksPerDay;
      var TempI64A, TempI64B;

      var fromTicks = function (ticks) {
          return JSIL.CreateInstanceOfType(
            $jsilcore.System.TimeSpan.__Type__, "$fromTicks",
            [ticks]
          );
      };

      var fromDoubleTicks = function (ticks) {
          return $jsilcore.System.TimeSpan.FromTicks(
            $jsilcore.System.Int64.FromNumber(ticks)
          );
      };

      $.RawMethod(true, ".cctor", function () {
          var tInt64 = $jsilcore.System.Int64;
          TicksPerMillisecond = tInt64.FromNumber(10000);
          TicksPerSecond = tInt64.FromNumber(10000000);
          TicksPerMinute = tInt64.FromNumber(600000000);
          TicksPerHour = tInt64.FromNumber(36000000000);
          TicksPerDay = tInt64.FromNumber(864000000000);

          TempI64A = new tInt64();
          TempI64B = new tInt64();
      });

      $.RawMethod(false, "__CopyMembers__",
      function TimeSpan_CopyMembers(source, target) {
          target._ticks = source._ticks.MemberwiseClone();
          target._cachedTotalMs = source._cachedTotalMs || 0;
          target._cachedTotalS = source._cachedTotalS || 0;
      }
      );

      $.Method({ Static: true, Public: true }, "FromMilliseconds",
          (new JSIL.MethodSignature($.Type, [$.Double], [])),
        function FromMilliseconds(value) {
            return fromDoubleTicks(value * TicksPerMillisecond.valueOf());
        }
        );

      $.Method({ Static: true, Public: true }, "FromSeconds",
          (new JSIL.MethodSignature($.Type, [$.Double], [])),
        function FromSeconds(value) {
            return fromDoubleTicks(value * TicksPerSecond.valueOf());
        }
        );

      $.Method({ Static: true, Public: true }, "FromMinutes",
          (new JSIL.MethodSignature($.Type, [$.Double], [])),
        function FromMinutes(value) {
            return fromDoubleTicks(value * TicksPerMinute.valueOf());
        }
        );

      $.Method({ Static: true, Public: true }, "FromHours",
          (new JSIL.MethodSignature($.Type, [$.Double], [])),
        function FromHours(value) {
            return fromDoubleTicks(value * TicksPerHour.valueOf());
        }
        );

      $.Method({ Static: true, Public: true }, "FromDays",
          (new JSIL.MethodSignature($.Type, [$.Double], [])),
        function FromDays(value) {
            return fromDoubleTicks(value * TicksPerDay.valueOf());
        }
        );

      $.Method({ Static: true, Public: true }, "FromTicks",
          (new JSIL.MethodSignature($.Type, [$.Int64], [])),
          fromTicks
        );

      $.Method({ Static: true, Public: true }, "op_Addition",
          (new JSIL.MethodSignature($.Type, [$.Type, $.Type], [])),
        function op_Addition(t1, t2) {
            return fromTicks($jsilcore.System.Int64.op_Addition(t1._ticks, t2._ticks, TempI64A));
        }
        );

      $.Method({ Static: true, Public: true }, "op_Equality",
          (new JSIL.MethodSignature($.Boolean, [$.Type, $.Type], [])),
        function op_Equality(t1, t2) {
            return $jsilcore.System.Int64.op_Equality(t1._ticks, t2._ticks);
        }
        );

      $.Method({ Static: true, Public: true }, "op_GreaterThan",
          (new JSIL.MethodSignature($.Boolean, [$.Type, $.Type], [])),
        function op_GreaterThan(t1, t2) {
            return $jsilcore.System.Int64.op_GreaterThan(t1._ticks, t2._ticks);
        }
        );

      $.Method({ Static: true, Public: true }, "op_GreaterThanOrEqual",
          (new JSIL.MethodSignature($.Boolean, [$.Type, $.Type], [])),
        function op_GreaterThanOrEqual(t1, t2) {
            return $jsilcore.System.Int64.op_GreaterThanOrEqual(t1._ticks, t2._ticks);
        }
        );

      $.Method({ Static: true, Public: true }, "op_Inequality",
          (new JSIL.MethodSignature($.Boolean, [$.Type, $.Type], [])),
        function op_Inequality(t1, t2) {
            return $jsilcore.System.Int64.op_Inequality(t1._ticks, t2._ticks);
        }
        );

      $.Method({ Static: true, Public: true }, "op_LessThan",
          (new JSIL.MethodSignature($.Boolean, [$.Type, $.Type], [])),
        function op_LessThan(t1, t2) {
            return $jsilcore.System.Int64.op_LessThan(t1._ticks, t2._ticks);
        }
        );

      $.Method({ Static: true, Public: true }, "op_LessThanOrEqual",
          (new JSIL.MethodSignature($.Boolean, [$.Type, $.Type], [])),
        function op_LessThanOrEqual(t1, t2) {
            return $jsilcore.System.Int64.op_LessThanOrEqual(t1._ticks, t2._ticks);
        }
        );

      $.Method({ Static: true, Public: true }, "op_Subtraction",
          (new JSIL.MethodSignature($.Type, [$.Type, $.Type], [])),
        function op_Subtraction(t1, t2) {
            return fromTicks($jsilcore.System.Int64.op_Subtraction(t1._ticks, t2._ticks, TempI64A));
        }
        );

      $.Method({ Static: true, Public: true }, "op_UnaryNegation",
          (new JSIL.MethodSignature($.Type, [$.Type], [])),
        function op_UnaryNegation(self) {
            return fromTicks($jsilcore.System.Int64.op_UnaryNegation(self._ticks));
        }
        );

      $.RawMethod(false, "$accumulate", function (multiplier, amount) {
          // FIXME: unnecessary garbage
          var tInt64 = $jsilcore.System.Int64;
          var amount64 = tInt64.FromNumber(amount);
          var scaled;

          if (multiplier)
              scaled = tInt64.op_Multiplication(multiplier, amount64);
          else
              scaled = amount64;

          if (!this._ticks)
              this._ticks = scaled;
          else
              this._ticks = tInt64.op_Addition(this._ticks, scaled);

          this.$invalidate();
      });

      $.RawMethod(false, "$fromTicks",
      function fromTicks(ticks) {
          if (typeof (ticks) === "number")
              JSIL.RuntimeError("Argument must be an Int64");

          this._ticks = ticks.MemberwiseClone();
      }
      );

      $.Method({ Static: false, Public: true }, ".ctor",
          (new JSIL.MethodSignature(null, [$.Int64], [])),
        function _ctor(ticks) {
            if (typeof (ticks) === "number")
                JSIL.RuntimeError("Argument must be an Int64");

            this._ticks = ticks.MemberwiseClone();
        }
        );

      $.Method({ Static: false, Public: true }, ".ctor",
          (new JSIL.MethodSignature(null, [
                $.Int32, $.Int32,
                $.Int32
          ], [])),
        function _ctor(hours, minutes, seconds) {
            this.$accumulate(TicksPerHour, hours);
            this.$accumulate(TicksPerMinute, minutes);
            this.$accumulate(TicksPerSecond, seconds);
        }
        );

      $.Method({ Static: false, Public: true }, ".ctor",
          (new JSIL.MethodSignature(null, [
                $.Int32, $.Int32,
                $.Int32, $.Int32
          ], [])),
        function _ctor(days, hours, minutes, seconds) {
            this.$accumulate(TicksPerDay, days);
            this.$accumulate(TicksPerHour, hours);
            this.$accumulate(TicksPerMinute, minutes);
            this.$accumulate(TicksPerSecond, seconds);
        }
        );

      $.Method({ Static: false, Public: true }, ".ctor",
          (new JSIL.MethodSignature(null, [
                $.Int32, $.Int32,
                $.Int32, $.Int32,
                $.Int32
          ], [])),
        function _ctor(days, hours, minutes, seconds, milliseconds) {
            this.$accumulate(TicksPerDay, days);
            this.$accumulate(TicksPerHour, hours);
            this.$accumulate(TicksPerMinute, minutes);
            this.$accumulate(TicksPerSecond, seconds);
            this.$accumulate(TicksPerMillisecond, milliseconds);
        }
        );

      $.Method({ Static: false, Public: true }, "get_Days",
          (new JSIL.MethodSignature($.Int32, [], [])),
        function get_Days() {
            return this.$divide(TicksPerDay);
        }
        );

      $.Method({ Static: false, Public: true }, "get_Hours",
          (new JSIL.MethodSignature($.Int32, [], [])),
        function get_Hours() {
            return this.$modDiv(TicksPerDay, TicksPerHour);
        }
        );

      $.Method({ Static: false, Public: true }, "get_Milliseconds",
          (new JSIL.MethodSignature($.Int32, [], [])),
        function get_Milliseconds() {
            return this.$modDiv(TicksPerSecond, TicksPerMillisecond);
        }
        );

      $.Method({ Static: false, Public: true }, "get_Minutes",
          (new JSIL.MethodSignature($.Int32, [], [])),
        function get_Minutes() {
            return this.$modDiv(TicksPerHour, TicksPerMinute);
        }
        );

      $.Method({ Static: false, Public: true }, "get_Seconds",
          (new JSIL.MethodSignature($.Int32, [], [])),
        function get_Seconds() {
            return this.$modDiv(TicksPerMinute, TicksPerSecond);
        }
        );

      $.Method({ Static: false, Public: true }, "get_Ticks",
          (new JSIL.MethodSignature($.Int64, [], [])),
        function get_Ticks() {
            return this._ticks;
        }
        );

      $.RawMethod(false, "$modDiv", function $modulus(modulus, divisor) {
          var tInt64 = $jsilcore.System.Int64;
          var result = tInt64.op_Modulus(this._ticks, modulus, TempI64A);

          if (divisor)
              result = tInt64.op_Division(result, divisor, TempI64B);

          return result.ToInt32();
      });

      $.RawMethod(false, "$divide", function $divide(divisor) {
          var tInt64 = $jsilcore.System.Int64;
          var result = tInt64.op_Division(this._ticks, divisor, TempI64A);
          return result.ToInt32();
      });

      $.RawMethod(false, "$toNumberDivided", function $toNumberDivided(divisor) {
          var tInt64 = $jsilcore.System.Int64;
          var integral = tInt64.op_Division(this._ticks, divisor, TempI64A);
          var remainder = tInt64.op_Modulus(this._ticks, divisor, TempI64B);
          var scaledRemainder = remainder.ToNumber() / divisor.valueOf();

          var result = integral.ToNumber() + scaledRemainder;
          return result;
      });

      var invalidCachedTotal = 0;

      $.RawMethod(false, "$invalidate", function () {
          this._cachedTotalMs = invalidCachedTotal;
          this._cachedTotalS = invalidCachedTotal;
      });

      $.Method({ Static: false, Public: true }, "get_TotalMilliseconds",
          (new JSIL.MethodSignature($.Double, [], [])),
        function get_TotalMilliseconds() {
            if (!this._ticks.a && !this._ticks.b && !this._ticks.c)
                return 0;
            else if (this._cachedTotalMs)
                return this._cachedTotalMs;
            else
                return this._cachedTotalMs = this.$toNumberDivided(TicksPerMillisecond);
        }
        );

      $.Method({ Static: false, Public: true }, "get_TotalSeconds",
          (new JSIL.MethodSignature($.Double, [], [])),
        function get_TotalSeconds() {
            if (!this._ticks.a && !this._ticks.b && !this._ticks.c)
                return 0;
            else if (this._cachedTotalS)
                return this._cachedTotalS;
            else
                return this._cachedTotalS = this.$toNumberDivided(TicksPerSecond);
        }
        );

      $.Method({ Static: false, Public: true }, "get_TotalMinutes",
          (new JSIL.MethodSignature($.Double, [], [])),
        function get_TotalMinutes() {
            return this.$toNumberDivided(TicksPerMinute);
        }
        );

      $.Method({ Static: false, Public: true }, "get_TotalHours",
          (new JSIL.MethodSignature($.Double, [], [])),
        function get_TotalHours() {
            return this.$toNumberDivided(TicksPerHour);
        }
        );

      $.Method({ Static: false, Public: true }, "get_TotalDays",
          (new JSIL.MethodSignature($.Double, [], [])),
        function get_TotalDays() {
            return this.$toNumberDivided(TicksPerDay);
        }
        );

      $.Method({ Static: true, Public: true }, "Parse",
          (new JSIL.MethodSignature($.Type, [$.String], [])),
        function TimeSpan_Parse(text) {
            var days = 0;
            var hours = 0;
            var minutes = 0;
            var seconds = 0;
            var fractionalSeconds = 0;
            var timeAsString = (text.trim());
            var signKeeper = 1;
            if (System.String.StartsWith(timeAsString, "-")) {
                signKeeper = -1;
            }
            if (!((signKeeper !== -1) && !(System.String.StartsWith(timeAsString, "+")))) {
                timeAsString = (timeAsString.substr(1));
            }

            if (timeAsString.indexOf(':') == -1) {
                days = (System.Int32.Parse(timeAsString) | 0);
            } else {
                var splittedTime = timeAsString.split(':');
                if (!(((splittedTime.length | 0) !== 1) && ((splittedTime.length | 0) <= 3))) {
                    throw new System.FormatException("Could not create KeyTime from \"" + keyTimeCode + "\".");
                }
                var hoursAsString = splittedTime[0];
                var i = (hoursAsString.indexOf("."));
                if (i > 0) {
                    days = (System.Int32.Parse(hoursAsString.substr(0, i)) | 0);
                    hoursAsString = (hoursAsString.substr(((i + 1) | 0)));
                } else if (i === 0) {
                    throw new System.FormatException("Could not create KeyTime from \"" + keyTimeCode + "\".");
                }
                hours = (System.Int32.Parse(hoursAsString) | 0);
                minutes = (System.Int32.Parse(splittedTime[1]) | 0);
                if ((splittedTime.length | 0) === 3) {
                    var secondsAsString = splittedTime[2];
                    i = (secondsAsString.indexOf("."));
                    if (i > 0) {
                        var fractionalSecondsAsString = (secondsAsString.substr(((i + 1) | 0)));
                        fractionalSeconds = Math.imul(System.Int32.Parse(fractionalSecondsAsString), ((Math.pow(10, +(7 - (fractionalSecondsAsString.length | 0)))) | 0));
                        secondsAsString = (secondsAsString.substr(0, i));
                    }
                    seconds = (System.Int32.Parse(secondsAsString) | 0);
                }
            }

            var result = new System.TimeSpan();
            result.$accumulate(TicksPerDay, days);
            result.$accumulate(TicksPerHour, hours);
            result.$accumulate(TicksPerMinute, minutes);
            result.$accumulate(TicksPerSecond, seconds);
            result.$accumulate(null, fractionalSeconds);
            return result;
        }
        );

      $.Method({ Static: false, Public: true }, "toString",
          (new JSIL.MethodSignature($.String, [], [])),
        function TimeSpan_toString() {
            var ticks = this.$modDiv(TicksPerSecond);
            var seconds = this.get_Seconds();
            var minutes = this.get_Minutes();
            var hours = this.get_Hours();
            var days = this.get_Days();

            var formatString;

            if (days) {
                formatString = "{0}.{1:00}:{2:00}:";
            } else {
                formatString = "{1:00}:{2:00}:";
            }

            if (ticks) {
                formatString += "{3:00}.{4:0000000}";
            } else {
                formatString += "{3:00}";
            }

            return System.String.Format(
              formatString,
              days, hours, minutes, seconds, ticks
            );
        }
        );
  }
);

JSIL.MakeStruct("System.ValueType", "System.TimeSpan", true, [], function ($) {
    $.Field({ Static: false, Public: false }, "_ticks", $.Int64);

    $.Property({ Public: true, Static: false }, "Ticks");

    $.Property({ Public: true, Static: false }, "Milliseconds");

    $.Property({ Public: true, Static: false }, "TotalMilliseconds");

    $.Property({ Public: true, Static: false }, "Seconds");

    $.Property({ Public: true, Static: false }, "Minutes");

    $.Property({ Public: true, Static: false }, "Hours");

    $.Property({ Public: true, Static: false }, "Days");

    $.Property({ Public: true, Static: false }, "TotalSeconds");

    $.Property({ Public: true, Static: false }, "TotalMinutes");
});

JSIL.MakeEnum(
  "System.DateTimeKind", true, {
      Unspecified: 0,
      Utc: 1,
      Local: 2
  }, false
);

JSIL.ImplementExternals("System.DateTime", function ($) {

    //--------------------------------------------------
    // Note about the meaning of each private field:
    //--------------------------------------------------
    // - "msSince1970" is the number of milliseconds since 01/01/1970 at 00:00:00 (intended either Local or UTC depending on the current date kind). This is a native "Int32" type.
    // - "dateData" is the number of ticks since 01/01/0001 at 00:00:00 (intended either Local or UTC depending on the current date kind). This is a "UInt64". Note that "ticks" last 100ns, ie. there are 10 million ticks in one second.
    // - "ticks" (passed to the contructor) is the number of 100ns since 01/01/0001 at 00:00:00 (intended either Local or UTC depending on the current date kind). This is a "Int64" (notice the difference with the "UInt64" of "dateData").

    //--------------------------------------------------
    // Note about serialization:
    //--------------------------------------------------
    // - The serialization of DateTime is performed in the "FromDateTime" method located in the class "XmlSerializationWriter.cs".
    // - The deserialization of DateTime is performed in the "ToDateTime" method located in the file "JSIL.XML.js".


    function dateDataToMsSince1970(dateData) {
        var ticksSince1970 = $jsilcore.System.UInt64.op_Subtraction(dateData, ymdToTicks(1970, 1, 1));
        var msSince1970_uint64 = $jsilcore.System.UInt64.op_Division(ticksSince1970, $jsilcore.System.UInt64.FromInt32(10000)); //10000 ticks in 1 millisecond
        return msSince1970_uint64.valueOf();
    }

    function msSince1970ToDateData(msSince1970) {
        return $jsilcore.System.Int64.FromNumber((msSince1970 * 10000) + ymdToTicks(1970, 1, 1));
    }

    $.RawMethod(false, "$fromLocalMilliseconds", function (msSince1970) {
        this.dateData = msSince1970ToDateData(msSince1970);
        this.msSince1970 = msSince1970;
        this.kind = $jsilcore.System.DateTimeKind.Local;
    });

    $.RawMethod(false, "$fromUTCMilliseconds", function (msSince1970) {
        this.dateData = msSince1970ToDateData(msSince1970);
        this.msSince1970 = msSince1970;
        this.kind = $jsilcore.System.DateTimeKind.Utc;
    });

    $.Method({ Static: false, Public: true }, ".ctor",
      (new JSIL.MethodSignature(null, [$.Int64], [])),
      function _ctor(ticks) {
          this.dateData = ticks.ToUInt64();
          this.msSince1970 = dateDataToMsSince1970(this.dateData);
          this.kind = $jsilcore.System.DateTimeKind.Unspecified;
      }
    );

    $.Method({ Static: false, Public: false }, ".ctor",
      (new JSIL.MethodSignature(null, [$.UInt64], [])),
      function _ctor(dateData) {
          this.dateData = dateData;
          this.msSince1970 = dateDataToMsSince1970(this.dateData);
          this.kind = $jsilcore.System.DateTimeKind.Unspecified;
      }
    );

    var normalYearDays = [
    0, 31, 59,
    90, 120, 151,
      181, 212, 243,
      273, 304, 334,
      365
    ];
    var leapYearDays = [
    0, 31, 60,
      91, 121, 152,
      182, 213, 244,
      274, 305, 335,
      366
    ];

    var msBetweenTimeOriginAnd1970 = 62135596800000.0;

    function isLeapYear(year) {
        if (((year % 100) !== 0) || ((year % 400) === 0)) {
            return (year % 4) === 0;
        }
        return false;
    };

    $.RawMethod(false, "$ymdToTicks", function (year, month, day) {
        return ymdToTicks(year, month, day);
    });

    function ymdToTicks(year, month, day) {

        // This method does not care whether we are in LocalType or UTC: it just converts the "year, month, day" intended as a "duration" into ticks.

        if ((year < 1) || (year > 9999))
            throw new System.ArgumentException("year");

        if ((month < 1) || (month > 12))
            throw new System.ArgumentException("month");

        var days = isLeapYear(year) ? leapYearDays : normalYearDays;
        var daysThisMonth = days[month] - days[month - 1];

        if ((day < 1) || (day > daysThisMonth))
            throw new System.ArgumentException("day");

        year -= 1;

        var yearDays = year * 365;
        var leapYearDayOffset = ((year / 4) | 0) - ((year / 100) | 0) + ((year / 400) | 0);
        var monthDays = days[month - 1];

        var totalDays = $jsilcore.System.UInt64.FromInt32(
          (yearDays + leapYearDayOffset + monthDays + (day - 1)) | 0
        );
        var ticksPerDay = $jsilcore.System.UInt64.Parse("864000000000");

        var result = $jsilcore.System.UInt64.op_Multiplication(
          totalDays,
          ticksPerDay
        );

        return result;
    };

    $.RawMethod(false, "$hmsmToTicks", function (hour, minute, second, millisecond) {
        return hmsmToTicks(hour, minute, second, millisecond);
    });

    function hmsmToTicks(hour, minute, second, millisecond) {

        // This method does not care whether we are in LocalType or UTC: it just converts the "hour, minute, second, millisecond" intended as a "duration" into ticks.

        var date = new Date();
        if (millisecond === undefined) {
            millisecond = 0;
        }
        var rest = hour * 60; //60 minutes in a hour
        rest += minute;
        rest *= 60; //60 seconds in a minute
        rest += second;
        rest *= 1000; //1000 milliseconds in a second
        if (millisecond != 0) {
            rest += millisecond + 1; //for some reason, the constructor was creating a DateTime with 1 millisecond less than expected
        }
        rest *= 10000; //10000 ticks in a millisecond
        return $jsilcore.System.UInt64.FromNumber(rest);
    }

    $.Method({ Static: false, Public: true }, ".ctor",
      (new JSIL.MethodSignature(null, [
            $.Int32, $.Int32,
            $.Int32
      ], [])),
      function _ctor(year, month, day) {
          this.dateData = ymdToTicks(year, month, day);
          this.msSince1970 = dateDataToMsSince1970(this.dateData);
          this.year = year;
          this.month = month;
          this.day = day;
          this.kind = $jsilcore.System.DateTimeKind.Unspecified;
      }
    );

    $.Method({ Static: false, Public: true }, ".ctor",
      (new JSIL.MethodSignature(null, [$.Int64, $jsilcore.TypeRef("System.DateTimeKind")], [])),
      function _ctor(ticks, kind) {
          this.dateData = ticks.ToUInt64();
          this.msSince1970 = dateDataToMsSince1970(this.dateData);
          this.kind = kind;
      }
    );

    $.Method({ Static: false, Public: true }, ".ctor",
      (new JSIL.MethodSignature(null, [$.Int32, $.Int32, $.Int32, $.Int32, $.Int32, $.Int32], [])),
      function _ctor(year, month, day, hour, minute, second) {

          var ticks = ymdToTicks(year, month, day);
          var rest = hmsmToTicks(hour, minute, second, 0);
          this.dateData = $jsilcore.System.UInt64.op_Addition(ticks, rest);
          this.msSince1970 = dateDataToMsSince1970(this.dateData);
          this.kind = $jsilcore.System.DateTimeKind.Unspecified;
      }
    );

    $.Method({ Static: false, Public: true }, ".ctor",
      (new JSIL.MethodSignature(null, [$.Int32, $.Int32, $.Int32, $.Int32, $.Int32, $.Int32, $jsilcore.TypeRef("System.DateTimeKind")], [])),
      function _ctor(year, month, day, hour, minute, second, kind) {

          var ticks = ymdToTicks(year, month, day);
          var rest = hmsmToTicks(hour, minute, second, 0);
          this.dateData = $jsilcore.System.UInt64.op_Addition(ticks, rest);
          this.msSince1970 = dateDataToMsSince1970(this.dateData);
          this.kind = kind;
      }
    );

    $.Method({ Static: false, Public: true }, ".ctor",
      (new JSIL.MethodSignature(null, [$.Int32, $.Int32, $.Int32, $.Int32, $.Int32, $.Int32, $.Int32], [])),
      function _ctor(year, month, day, hour, minute, second, millisecond) {

          var ticks = ymdToTicks(year, month, day);
          var rest = hmsmToTicks(hour, minute, second, millisecond);
          //this.msSince1970 = (ticks - ymdToTicks(1970, 1, 1)) / 10000;
          this.dateData = $jsilcore.System.UInt64.op_Addition(ticks, rest);
          this.msSince1970 = dateDataToMsSince1970(this.dateData);
          this.kind = $jsilcore.System.DateTimeKind.Unspecified;
      }
    );

    $.Method({ Static: false, Public: true }, ".ctor",
      (new JSIL.MethodSignature(null, [$.Int32, $.Int32, $.Int32, $.Int32, $.Int32, $.Int32, $.Int32, $jsilcore.TypeRef("System.DateTimeKind")], [])),
      function _ctor(year, month, day, hour, minute, second, millisecond, kind) {

          var ticks = ymdToTicks(year, month, day);
          var rest = hmsmToTicks(hour, minute, second, millisecond);
          this.dateData = $jsilcore.System.UInt64.op_Addition(ticks, rest);
          this.msSince1970 = dateDataToMsSince1970(this.dateData);
          this.kind = kind;
      }
    );

    $.Method({ Static: false, Public: true }, ".ctor",
      (new JSIL.MethodSignature(null, [
            $.Int32, $.Int32,
            $.Int32, $jsilcore.TypeRef("System.Globalization.Calendar")
      ], [])),
      function _ctor(year, month, day, calendar) {
          // FIXME: calendar
          this.dateData = ymdToTicks(year, month, day);
          this.msSince1970 = dateDataToMsSince1970(this.dateData);
          this.year = year;
          this.month = month;
          this.day = day;
          this.kind = $jsilcore.System.DateTimeKind.Unspecified;
      }
    );

    $.RawMethod(false, "__CopyMembers__",
      function DateTime_CopyMembers(source, target) {
          target.dateData = source.dateData.MemberwiseClone();
          target.msSince1970 = source.msSince1970;
          target.kind = source.kind;
      }
    );

    $.Method({ Static: true, Public: true }, "get_Now",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [], [])),
      function get_Now() {
          return JSIL.CreateInstanceOfType(
            $jsilcore.System.DateTime.__Type__, "$fromLocalMilliseconds", [JSIL.Host.getTime() - (new Date()).getTimezoneOffset() * 60000] // Note: 60000 is the number of milliseconds in 1 minute.
          );
      }
    );

    $.Method({ Static: true, Public: true }, "get_UtcNow",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [], [])),
      function get_UtcNow() {
          return JSIL.CreateInstanceOfType(
            $jsilcore.System.DateTime.__Type__, "$fromUTCMilliseconds", [JSIL.Host.getTime()]
          );
      }
    );

    $.Method({ Static: false, Public: true }, "get_Year",
        (new JSIL.MethodSignature($.Int64, [], [])),
      function get_Year() {
          if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              this.msSince1970 = -msBetweenTimeOriginAnd1970;
          var d = new Date(this.msSince1970); // The constructor of "Date" expects a value representing the number of milliseconds since 1 January 1970 00:00:00 UTC. This is why we use "getUTC..." in the line below.
          return d.getUTCFullYear();
      }
      );

    $.Method({ Static: false, Public: true }, "get_Month",
        (new JSIL.MethodSignature($.Int64, [], [])),
      function get_Month() {
          if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              this.msSince1970 = -msBetweenTimeOriginAnd1970;
          var d = new Date(this.msSince1970); // The constructor of "Date" expects a value representing the number of milliseconds since 1 January 1970 00:00:00 UTC. This is why we use "getUTC..." in the line below.
          return d.getUTCMonth() + 1; // Note: this is due to the fact that in JS the first month has an index of "0".
      }
      );

    $.Method({ Static: false, Public: true }, "get_Day",
        (new JSIL.MethodSignature($.Int64, [], [])),
      function get_Day() {
          if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              this.msSince1970 = -msBetweenTimeOriginAnd1970;
          var d = new Date(this.msSince1970); // The constructor of "Date" expects a value representing the number of milliseconds since 1 January 1970 00:00:00 UTC. This is why we use "getUTC..." in the line below.
          return d.getUTCDate();
      }
      );

    $.Method({ Static: false, Public: true }, "get_Hour",
      (new JSIL.MethodSignature($.Int64, [], [])),
      function get_Day() {
          if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              this.msSince1970 = -msBetweenTimeOriginAnd1970;
          var d = new Date(this.msSince1970); // The constructor of "Date" expects a value representing the number of milliseconds since 1 January 1970 00:00:00 UTC. This is why we use "getUTC..." in the line below.
          return d.getUTCHours();
      }
    );
    $.Method({ Static: false, Public: true }, "get_Minute",
    (new JSIL.MethodSignature($.Int64, [], [])),
    function get_Day() {
        if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
            this.msSince1970 = -msBetweenTimeOriginAnd1970;
        var d = new Date(this.msSince1970); // The constructor of "Date" expects a value representing the number of milliseconds since 1 January 1970 00:00:00 UTC. This is why we use "getUTC..." in the line below.
        return d.getUTCMinutes();
    }
  );
    $.Method({ Static: false, Public: true }, "get_Second",
    (new JSIL.MethodSignature($.Int64, [], [])),
    function get_Day() {
        if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
            this.msSince1970 = -msBetweenTimeOriginAnd1970;
        var d = new Date(this.msSince1970); // The constructor of "Date" expects a value representing the number of milliseconds since 1 January 1970 00:00:00 UTC. This is why we use "getUTC..." in the line below.
        return d.getUTCSeconds();
    }
  );

    $.Method({ Static: false, Public: true }, "get_Millisecond",
    (new JSIL.MethodSignature($.Int64, [], [])),
    function get_Day() {
        if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
            this.msSince1970 = -msBetweenTimeOriginAnd1970;
        var d = new Date(this.msSince1970); // The constructor of "Date" expects a value representing the number of milliseconds since 1 January 1970 00:00:00 UTC. This is why we use "getUTC..." in the line below.
        return d.getMilliseconds();
    }
  );

    $.Method({ Static: false, Public: true }, "get_Ticks",
(new JSIL.MethodSignature($.Int64, [], [])),
function get_Ticks() {
    if (typeof this.msSince1970 === 'undefined') { // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
        this.msSince1970 = -msBetweenTimeOriginAnd1970;
        this.dateData = msSince1970ToDateData(this.msSince1970);
    }
    return this.dateData;
}
);

    $.Method({ Static: false, Public: true }, "get_TimeOfDay",
    (new JSIL.MethodSignature($jsilcore.TypeRef("System.TimeSpan"), [], [])),
    function get_TimeOfDay() {
        if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
            this.msSince1970 = -msBetweenTimeOriginAnd1970;
        var d = new Date(this.msSince1970); // The constructor of "Date" expects a value representing the number of milliseconds since 1 January 1970 00:00:00 UTC.
        d.setHours(0, 0, 0, 0);
        if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
            this.msSince1970 = -msBetweenTimeOriginAnd1970;
        return $jsilcore.System.TimeSpan.FromTicks($jsilcore.System.Int64.op_Subtraction($jsilcore.System.UInt64.FromNumber(this.msSince1970 * 10000), $jsilcore.System.UInt64.FromNumber(d.getTime() * 10000)));
    }
  );

    $.Method({ Static: false, Public: true }, "ToUniversalTime",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [], [])),
      function ToUniversalTime() {
          var copy = this.MemberwiseClone();
          if (typeof copy.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              copy.msSince1970 = -msBetweenTimeOriginAnd1970;
          var d = new Date(copy.msSince1970);

          // Note: If the value of "kind" is "Unspecified", the method "ToUniversalTime" assumes that the value is Local.

          if (this.kind !== $jsilcore.System.DateTimeKind.Utc) {
              copy.msSince1970 += d.getTimezoneOffset() * 60000; //(60000) is the number of milliseconds in 1 minute.
          }

          copy.dateData = msSince1970ToDateData(copy.msSince1970);
          copy.kind = $jsilcore.System.DateTimeKind.Utc;
          return copy;
      }
    );

    $.Method({ Static: false, Public: true }, "ToLocalTime",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [], [])),
      function ToLocalTime() {
          var copy = this.MemberwiseClone();
          if (typeof copy.msSince1970 === 'undefined')
              copy.msSince1970 = -msBetweenTimeOriginAnd1970;
          var d = new Date(copy.msSince1970);

          // Note: If the value of "kind" is "Unspecified", the method "ToLocalTime" assumes that the value is UTC.

          if (this.kind !== $jsilcore.System.DateTimeKind.Local) {
              copy.msSince1970 -= d.getTimezoneOffset() * 60000; //(60000) is the number of milliseconds in 1 minute.
          }

          copy.dateData = msSince1970ToDateData(copy.msSince1970);
          copy.kind = $jsilcore.System.DateTimeKind.Local;
          return copy;
      }
    );

    $.Method({ Static: false, Public: true }, "get_DayOfWeek",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.DayOfWeek"), [], [])),
      function get_DayOfWeek() {
          if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              this.msSince1970 = -msBetweenTimeOriginAnd1970;
          var daysSince1970 = (this.msSince1970 / (864000000000 / 10000) | 0);

          var dayOfTheWeek = (daysSince1970 + 4) % 7; // +4 because +3 since the 1st of january 1970 was a thursday and +1 because sunday is 0

          //var $T08 = JSIL.Memoize($jsilcore.System.DayOfWeek);
          //var v = $T08();
          return $jsilcore.System.DayOfWeek.$Cast(dayOfTheWeek);
      }
    );

    $.Method({ Static: true, Public: true }, "op_Subtraction",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.TimeSpan"), [$.Type, $.Type], [])),
      function op_Subtraction(t1, t2) {
          if (typeof t1.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              t1.msSince1970 = -msBetweenTimeOriginAnd1970;
          if (typeof t2.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              t2.msSince1970 = -msBetweenTimeOriginAnd1970;
          return $jsilcore.System.TimeSpan.FromTicks($jsilcore.System.Int64.op_Subtraction($jsilcore.System.Int64.FromNumber(t1.msSince1970 * 10000), $jsilcore.System.Int64.FromNumber(t2.msSince1970 * 10000)));
      }
    );

    $.Method({ Static: true, Public: true }, "op_Equality",
      (new JSIL.MethodSignature($.Boolean, [$.Type, $.Type], [])),
      function op_Equality(dt1, dt2) {
          if (typeof dt1.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              dt1.msSince1970 = -msBetweenTimeOriginAnd1970;
          if (typeof dt2.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              dt2.msSince1970 = -msBetweenTimeOriginAnd1970;
          return (dt1.msSince1970 == dt2.msSince1970);
      }
    );


    $.Method({ Static: false, Public: true }, "GetHashCode",
      (new JSIL.MethodSignature($.Int32, [], [])),
      function GetHashCode() {
          if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              this.msSince1970 = -msBetweenTimeOriginAnd1970;
          return this.msSince1970;
      }
    );

    $.Method({ Static: false, Public: true }, "AddMonths",
    (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [$jsilcore.TypeRef("System.Double")], [])),
  function AddMonths(monthsToAdd) {
      var copy = this.MemberwiseClone();
      if (typeof copy.msSince1970 === 'undefined')
          copy.msSince1970 = -msBetweenTimeOriginAnd1970;
      var month = copy.get_Month();
      var year = copy.get_Year();
      var i = 0;
      var daysToAdd = 0;
      for (i = 0; i < monthsToAdd; i++) {
          var days = isLeapYear(year) ? leapYearDays : normalYearDays;
          daysToAdd += days[month + 1 + i] - days[month + i];
          if (month + 1 + i == 12) {
              month -= 12;
              year += 1;
          }
      }
      copy.msSince1970 += daysToAdd * (864000000000 / 10000);
      copy.dateData = msSince1970ToDateData(copy.msSince1970);
      return copy;
  }
);

    $.Method({ Static: false, Public: true }, "AddYears",
        (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [$jsilcore.TypeRef("System.Double")], [])),
      function AddYears(yearsToAdd) {
          return this.AddMonths(yearsToAdd * 12);
      }
      );

    $.Method({ Static: false, Public: true }, "AddDays",
        (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [$jsilcore.TypeRef("System.Double")], [])),
      function AddDays(daysToAdd) {
          var copy = this.MemberwiseClone();
          if (typeof copy.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              copy.msSince1970 = -msBetweenTimeOriginAnd1970;
          copy.msSince1970 += daysToAdd * (864000000000 / 10000);//(864000000000 / 10000) is Ticks per day / ticks per milliseconds --> milliseconds per day
          copy.dateData = msSince1970ToDateData(copy.msSince1970);
          return copy;
      }
      );

    $.Method({ Static: false, Public: true }, "AddHours",
        (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [$jsilcore.TypeRef("System.Double")], [])),
      function AddHours(hoursToAdd) {
          var copy = this.MemberwiseClone();
          if (typeof copy.msSince1970 === 'undefined')
              copy.msSince1970 = -msBetweenTimeOriginAnd1970;
          copy.msSince1970 += hoursToAdd * 60 * 60 * 1000;
          copy.dateData = msSince1970ToDateData(copy.msSince1970);
          return copy;
      }
      );

    $.Method({ Static: false, Public: true }, "AddMinutes",
        (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [$jsilcore.TypeRef("System.Double")], [])),
      function AddMinutes(minutesToAdd) {
          var copy = this.MemberwiseClone();
          if (typeof copy.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              copy.msSince1970 = -msBetweenTimeOriginAnd1970;
          copy.msSince1970 += minutesToAdd * 60 * 1000;
          copy.dateData = msSince1970ToDateData(copy.msSince1970);
          return copy;
      }
      );

    $.Method({ Static: false, Public: true }, "AddSeconds",
        (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [$jsilcore.TypeRef("System.Double")], [])),
      function AddSeconds(secondsToAdd) {
          var copy = this.MemberwiseClone();
          if (typeof copy.msSince1970 === 'undefined')
              copy.msSince1970 = -msBetweenTimeOriginAnd1970;
          copy.msSince1970 += secondsToAdd * 1000;
          copy.dateData = msSince1970ToDateData(copy.msSince1970);
          return copy;
      }
      );

    $.Method({ Static: false, Public: true }, "AddMilliseconds",
        (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [$jsilcore.TypeRef("System.Double")], [])),
      function AddMilliseconds(millisecondsToAdd) {
          var copy = this.MemberwiseClone();
          if (typeof copy.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              copy.msSince1970 = -msBetweenTimeOriginAnd1970;
          copy.msSince1970 += millisecondsToAdd;
          copy.dateData = msSince1970ToDateData(copy.msSince1970);
          return copy;
      }
      );

    $.Method({ Static: false, Public: true }, "AddTicks",
    (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [$.Int64], [])),
      function AddTicks(ticksToAdd) {
          var copy = this.MemberwiseClone();
          if (typeof copy.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              copy.msSince1970 = -msBetweenTimeOriginAnd1970;
          copy.dateData = $jsilcore.System.UInt64.op_Addition(copy.dateData, ticksToAdd);
          copy.msSince1970 = dateDataToMsSince1970(copy.dateData);
          return copy;
      }
  );

    $.Method({ Static: true, Public: true }, "op_Addition",
        (new JSIL.MethodSignature($.Type, [$.Type, $jsilcore.TypeRef("System.TimeSpan")], [])),
      function op_Addition(dt, ts) {
          var copy = dt.MemberwiseClone();
          if (typeof copy.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              copy.msSince1970 = -msBetweenTimeOriginAnd1970;
          var ticksToAdd = ts._ticks;
          copy.dateData = $jsilcore.System.UInt64.op_Addition(copy.dateData, ticksToAdd);
          copy.msSince1970 = dateDataToMsSince1970(copy.dateData);
          return copy;
      }
      );

    $.Method({ Static: true, Public: true }, "op_GreaterThan",
        (new JSIL.MethodSignature($.Boolean, [$.Type, $.Type], [])),
      function op_GreaterThan(dt1, dt2) {
          if (typeof dt1.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              dt1.msSince1970 = -msBetweenTimeOriginAnd1970;
          if (typeof dt2.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              dt2.msSince1970 = -msBetweenTimeOriginAnd1970;
          return dt1.msSince1970 > dt2.msSince1970;
      }
      );

    $.Method({ Static: true, Public: true }, "op_GreaterThanOrEqual",
        (new JSIL.MethodSignature($.Boolean, [$.Type, $.Type], [])),
      function op_GreaterThanOrEqual(dt1, dt2) {
          if (typeof dt1.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              dt1.msSince1970 = -msBetweenTimeOriginAnd1970;
          if (typeof dt2.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              dt2.msSince1970 = -msBetweenTimeOriginAnd1970;
          return dt1.msSince1970 > dt2.msSince1970;
      }
      );

    $.Method({ Static: true, Public: true }, "op_LessThan",
        (new JSIL.MethodSignature($.Boolean, [$.Type, $.Type], [])),
      function op_LessThan(dt1, dt2) {
          if (typeof dt1.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              dt1.msSince1970 = -msBetweenTimeOriginAnd1970;
          if (typeof dt2.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              dt2.msSince1970 = -msBetweenTimeOriginAnd1970;
          return dt1.msSince1970 < dt2.msSince1970;
      }
      );

    $.Method({ Static: true, Public: true }, "op_LessThanOrEqual",
        (new JSIL.MethodSignature($.Boolean, [$.Type, $.Type], [])),
      function op_LessThanOrEqual(dt1, dt2) {
          if (typeof dt1.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              dt1.msSince1970 = -msBetweenTimeOriginAnd1970;
          if (typeof dt2.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              dt2.msSince1970 = -msBetweenTimeOriginAnd1970;
          return dt1.msSince1970 <= dt2.msSince1970;
      }
      );


    $.Method({ Static: true, Public: true }, "op_Inequality",
        (new JSIL.MethodSignature($.Boolean, [$.Type, $.Type], [])),
      function op_Inequality(dt1, dt2) {
          if (typeof dt1.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              dt1.msSince1970 = -msBetweenTimeOriginAnd1970;
          if (typeof dt2.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              dt2.msSince1970 = -msBetweenTimeOriginAnd1970;
          return dt1.msSince1970 != dt2.msSince1970;
      }
      );

    $.Method({ Static: true, Public: true }, "op_Subtraction",
        (new JSIL.MethodSignature($.Type, [$.Type, $jsilcore.TypeRef("System.TimeSpan")], [])),
      function op_Subtraction(dt, ts) {
          var copy = dt.MemberwiseClone();
          if (typeof copy.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              copy.msSince1970 = -msBetweenTimeOriginAnd1970;
          var ticksToAdd = ts._ticks;
          copy.dateData = $jsilcore.System.UInt64.op_Subtraction(copy.dateData, ticksToAdd);
          copy.msSince1970 = dateDataToMsSince1970(copy.dateData);
          return copy;
      }
      );

    $.Method({ Static: false, Public: true }, "toString",
      (new JSIL.MethodSignature($.String, [], [])),
      function DateTime_toString() {
          if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              this.msSince1970 = -msBetweenTimeOriginAnd1970;
          var d = new Date(this.msSince1970);
          var year = d.getUTCFullYear();
          var month = d.getUTCMonth() + 1; // Note: month index starts at "0".
          var day = d.getUTCDate();
          var hour = d.getUTCHours();
          var minute = d.getUTCMinutes();
          var second = d.getUTCSeconds();
          var formatString = "{0:00}/{1:00}/{2:0000} {3:00}:{4:00}:{5:00}";
          return System.String.Format(
            formatString,
            month, day, year, hour, minute, second
          );
      }
    );

    $.Method({ Static: false, Public: true }, "ToShortDateString",
      (new JSIL.MethodSignature($.String, [], [])),
      function ToShortDateString() {
          if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              this.msSince1970 = -msBetweenTimeOriginAnd1970;
          var d = new Date(this.msSince1970);
          var year = d.getUTCFullYear();
          var month = d.getUTCMonth() + 1; // Note: month index starts at "0".
          var day = d.getUTCDate();
          var formatString = "{0:00}/{1:00}/{2:0000}";
          return System.String.Format(
            formatString,
            month, day, year
          );
      }
    );

    $.Method({ Static: false, Public: true }, "ToShortTimeString",
      (new JSIL.MethodSignature($.String, [], [])),
      function ToShortTimeString() {
          if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              this.msSince1970 = -msBetweenTimeOriginAnd1970;
          var d = new Date(this.msSince1970);
          var hour = d.getUTCHours();
          var minute = d.getUTCMinutes();
          var formatString = "{0:00}:{1:00}";
          return System.String.Format(
            formatString,
            hour, minute
          );
      }
    );

    $.Method({ Static: false, Public: true }, "ToLongTimeString",
      (new JSIL.MethodSignature($.String, [], [])),
      function ToLongTimeString() {
          if (typeof this.msSince1970 === 'undefined') // This block ensures that default(DateTime) is equal to 01/01/0001 at 00:00:00.
              this.msSince1970 = -msBetweenTimeOriginAnd1970;
          var d = new Date(this.msSince1970);
          var hour = d.getUTCHours();
          var minute = d.getUTCMinutes();
          var second = d.getUTCSeconds();
          var formatString = "{0:00}:{1:00}:{2:00}";
          return System.String.Format(
            formatString,
            hour, minute, second
          );
      }
    );

    //$.Method({Static:false, Public:true }, "Substract",
    //  (new JSIL.MethodSignature($jsilcore.TypeRef("System.TimeSpan"), [$jsilcore.TypeRef("System.DateTime")], [])),
    //  function Substract(t1) {
    //      return System.DateTime.op_Subtraction(this.MemberwiseClone(), t1);
    //  }
    //);

    $.Method({ Static: false, Public: true }, "get_Date",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [], [])),
      function get_Date() {
          var copy = this.MemberwiseClone();
          var year = copy.get_Year();
          var month = copy.get_Month();
          var day = copy.get_Day();
          var ticksWithoutTimeOfDay = ymdToTicks(year, month, day).valueOf() - ymdToTicks(1970, 1, 1).valueOf();
          ////todo: see if we should compensate the time zone or not (following line):
          //if (copy.kind !== $jsilcore.System.DateTimeKind.Utc) {
          //    ticksWithoutTimeOfDay += new Date(copy.msSince1970).getTimezoneOffset() * 600000000; //60 000 0000 ticks per minute and getTimezoneOffset gives the amount of minutes to go from the hour in current time zone to the UTC hour 
          //}
          copy.msSince1970 = ticksWithoutTimeOfDay / 10000;//10000 ticks per millisecond
          copy.dateData = msSince1970ToDateData(copy.msSince1970);
          return copy;
      }
    );

    $.Method({ Static: false, Public: true }, "get_Kind",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTimeKind"), [], [])),
      function get_Kind() {
          if (typeof this.kind === 'undefined')
              this.kind = $jsilcore.System.DateTimeKind.Unspecified;
          return this.kind;
      }
    );

    $.Method({ Static: true, Public: true }, "get_Today",
      (new JSIL.MethodSignature($jsilcore.TypeRef("System.DateTime"), [], [])),
      function get_Today() {
          return $jsilcore.System.DateTime.get_Now().get_Date();
      }
    );

    $.Method({ Static: true, Public: true }, "TryParse",
    (new JSIL.MethodSignature($.Boolean, [$.String, $jsilcore.TypeRef("JSIL.Reference", [$.Type])])),
    function DateTime_TryParse(text, result) {
        var jsDate = new Date(text);
        var isValid = isValidDate(jsDate);
        if (isValid) {
            var year = jsDate.getFullYear();
            var month = jsDate.getMonth() + 1; // Note: month index starts at "0".
            var day = jsDate.getDate();
            var hour = jsDate.getHours();
            var minute = jsDate.getMinutes();
            var second = jsDate.getSeconds();
            var millisecond = jsDate.getMilliseconds();
            var date = new System.DateTime(year, month, day, hour, minute, second, millisecond);
            result.set(date);
        }
        else {
            result.set(new System.DateTime());
        }
        return isValid;
    }
    );

    $.Method({ Static: true, Public: true }, "Parse",
    (new JSIL.MethodSignature($.Type, [$.String], [])),
      function DateTime_Parse(text) {
          var jsDate = new Date(text);
          var isValid = isValidDate(jsDate);
          if (isValid) {
              var year = jsDate.getFullYear();
              var month = jsDate.getMonth() + 1; // Note: month index starts at "0".
              var day = jsDate.getDate();
              var hour = jsDate.getHours();
              var minute = jsDate.getMinutes();
              var second = jsDate.getSeconds();
              var millisecond = jsDate.getMilliseconds();
              var date = new System.DateTime(year, month, day, hour, minute, second, millisecond);
              return date;
          }
          else {
              throw new System.FormatException("Invalid date");
          }
      }
    );

    function isValidDate(d) {
        if (Object.prototype.toString.call(d) !== "[object Date]")
            return false;
        return !isNaN(d.getTime());
    }

});

JSIL.MakeStruct($jsilcore.TypeRef("System.ValueType"), "System.DateTime", true, [], function ($) {
});