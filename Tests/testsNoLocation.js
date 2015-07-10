//===================================================================================================================================================================================================================== //>
function each(array, callback) {
    var i;
    for (i = 0; i < array.length; i++) {
        callback(array[i], i, array);
    }
}
//===================================================================================================================================================================================================================== //>
module('add and subtract');

test('add short reverse args', function (assert) {
    var a = moment(), b, c, d;
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    assert.equal(a.add({ms: 50}).milliseconds(), 550, 'Add milliseconds');
    assert.equal(a.add({s: 1}).seconds(), 9, 'Add seconds');
    assert.equal(a.add({m: 1}).minutes(), 8, 'Add minutes');
    assert.equal(a.add({h: 1}).hours(), 7, 'Add hours');
    assert.equal(a.add({d: 1}).date(), 13, 'Add date');
    assert.equal(a.add({w: 1}).date(), 20, 'Add week');
    assert.equal(a.add({M: 1}).month(), 10, 'Add month');
    assert.equal(a.add({y: 1}).year(), 2012, 'Add year');
    assert.equal(a.add({Q: 1}).month(), 1, 'Add quarter');

    b = moment([2010, 0, 31]).add({M: 1});
    c = moment([2010, 1, 28]).subtract({M: 1});
    d = moment([2010, 1, 28]).subtract({Q: 1});

    assert.equal(b.month(), 1, 'add month, jan 31st to feb 28th');
    assert.equal(b.date(), 28, 'add month, jan 31st to feb 28th');
    assert.equal(c.month(), 0, 'subtract month, feb 28th to jan 28th');
    assert.equal(c.date(), 28, 'subtract month, feb 28th to jan 28th');
    assert.equal(d.month(), 10, 'subtract quarter, feb 28th 2010 to nov 28th 2009');
    assert.equal(d.date(), 28, 'subtract quarter, feb 28th 2010 to nov 28th 2009');
    assert.equal(d.year(), 2009, 'subtract quarter, feb 28th 2010 to nov 28th 2009');
});

test('add long reverse args', function (assert) {
    var a = moment();
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    assert.equal(a.add({milliseconds: 50}).milliseconds(), 550, 'Add milliseconds');
    assert.equal(a.add({seconds: 1}).seconds(), 9, 'Add seconds');
    assert.equal(a.add({minutes: 1}).minutes(), 8, 'Add minutes');
    assert.equal(a.add({hours: 1}).hours(), 7, 'Add hours');
    assert.equal(a.add({days: 1}).date(), 13, 'Add date');
    assert.equal(a.add({weeks: 1}).date(), 20, 'Add week');
    assert.equal(a.add({months: 1}).month(), 10, 'Add month');
    assert.equal(a.add({years: 1}).year(), 2012, 'Add year');
    assert.equal(a.add({quarters: 1}).month(), 1, 'Add quarter');
});

test('add long singular reverse args', function (assert) {
    var a = moment();
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    assert.equal(a.add({millisecond: 50}).milliseconds(), 550, 'Add milliseconds');
    assert.equal(a.add({second: 1}).seconds(), 9, 'Add seconds');
    assert.equal(a.add({minute: 1}).minutes(), 8, 'Add minutes');
    assert.equal(a.add({hour: 1}).hours(), 7, 'Add hours');
    assert.equal(a.add({day: 1}).date(), 13, 'Add date');
    assert.equal(a.add({week: 1}).date(), 20, 'Add week');
    assert.equal(a.add({month: 1}).month(), 10, 'Add month');
    assert.equal(a.add({year: 1}).year(), 2012, 'Add year');
    assert.equal(a.add({quarter: 1}).month(), 1, 'Add quarter');
});

test('add string long reverse args', function (assert) {
    var a = moment(), b;
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    b = a.clone();

    assert.equal(a.add('millisecond', 50).milliseconds(), 550, 'Add milliseconds');
    assert.equal(a.add('second', 1).seconds(), 9, 'Add seconds');
    assert.equal(a.add('minute', 1).minutes(), 8, 'Add minutes');
    assert.equal(a.add('hour', 1).hours(), 7, 'Add hours');
    assert.equal(a.add('day', 1).date(), 13, 'Add date');
    assert.equal(a.add('week', 1).date(), 20, 'Add week');
    assert.equal(a.add('month', 1).month(), 10, 'Add month');
    assert.equal(a.add('year', 1).year(), 2012, 'Add year');
    assert.equal(b.add('day', '01').date(), 13, 'Add date');
    assert.equal(a.add('quarter', 1).month(), 1, 'Add quarter');
});

test('add string long singular reverse args', function (assert) {
    var a = moment(), b;
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    b = a.clone();

    assert.equal(a.add('milliseconds', 50).milliseconds(), 550, 'Add milliseconds');
    assert.equal(a.add('seconds', 1).seconds(), 9, 'Add seconds');
    assert.equal(a.add('minutes', 1).minutes(), 8, 'Add minutes');
    assert.equal(a.add('hours', 1).hours(), 7, 'Add hours');
    assert.equal(a.add('days', 1).date(), 13, 'Add date');
    assert.equal(a.add('weeks', 1).date(), 20, 'Add week');
    assert.equal(a.add('months', 1).month(), 10, 'Add month');
    assert.equal(a.add('years', 1).year(), 2012, 'Add year');
    assert.equal(b.add('days', '01').date(), 13, 'Add date');
    assert.equal(a.add('quarters', 1).month(), 1, 'Add quarter');
});

test('add string short reverse args', function (assert) {
    var a = moment();
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    assert.equal(a.add('ms', 50).milliseconds(), 550, 'Add milliseconds');
    assert.equal(a.add('s', 1).seconds(), 9, 'Add seconds');
    assert.equal(a.add('m', 1).minutes(), 8, 'Add minutes');
    assert.equal(a.add('h', 1).hours(), 7, 'Add hours');
    assert.equal(a.add('d', 1).date(), 13, 'Add date');
    assert.equal(a.add('w', 1).date(), 20, 'Add week');
    assert.equal(a.add('M', 1).month(), 10, 'Add month');
    assert.equal(a.add('y', 1).year(), 2012, 'Add year');
    assert.equal(a.add('Q', 1).month(), 1, 'Add quarter');
});

test('add string long', function (assert) {
    var a = moment();
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    assert.equal(a.add(50, 'millisecond').milliseconds(), 550, 'Add milliseconds');
    assert.equal(a.add(1, 'second').seconds(), 9, 'Add seconds');
    assert.equal(a.add(1, 'minute').minutes(), 8, 'Add minutes');
    assert.equal(a.add(1, 'hour').hours(), 7, 'Add hours');
    assert.equal(a.add(1, 'day').date(), 13, 'Add date');
    assert.equal(a.add(1, 'week').date(), 20, 'Add week');
    assert.equal(a.add(1, 'month').month(), 10, 'Add month');
    assert.equal(a.add(1, 'year').year(), 2012, 'Add year');
    assert.equal(a.add(1, 'quarter').month(), 1, 'Add quarter');
});

test('add string long singular', function (assert) {
    var a = moment();
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    assert.equal(a.add(50, 'milliseconds').milliseconds(), 550, 'Add milliseconds');
    assert.equal(a.add(1, 'seconds').seconds(), 9, 'Add seconds');
    assert.equal(a.add(1, 'minutes').minutes(), 8, 'Add minutes');
    assert.equal(a.add(1, 'hours').hours(), 7, 'Add hours');
    assert.equal(a.add(1, 'days').date(), 13, 'Add date');
    assert.equal(a.add(1, 'weeks').date(), 20, 'Add week');
    assert.equal(a.add(1, 'months').month(), 10, 'Add month');
    assert.equal(a.add(1, 'years').year(), 2012, 'Add year');
    assert.equal(a.add(1, 'quarters').month(), 1, 'Add quarter');
});

test('add string short', function (assert) {
    var a = moment();
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    assert.equal(a.add(50, 'ms').milliseconds(), 550, 'Add milliseconds');
    assert.equal(a.add(1, 's').seconds(), 9, 'Add seconds');
    assert.equal(a.add(1, 'm').minutes(), 8, 'Add minutes');
    assert.equal(a.add(1, 'h').hours(), 7, 'Add hours');
    assert.equal(a.add(1, 'd').date(), 13, 'Add date');
    assert.equal(a.add(1, 'w').date(), 20, 'Add week');
    assert.equal(a.add(1, 'M').month(), 10, 'Add month');
    assert.equal(a.add(1, 'y').year(), 2012, 'Add year');
    assert.equal(a.add(1, 'Q').month(), 1, 'Add quarter');
});

test('add strings string short args', function (assert) {
    var a = moment();
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    assert.equal(a.add('ms', '50').milliseconds(), 550, 'Add milliseconds');
    assert.equal(a.add('s', '1').seconds(), 9, 'Add seconds');
    assert.equal(a.add('m', '1').minutes(), 8, 'Add minutes');
    assert.equal(a.add('h', '1').hours(), 7, 'Add hours');
    assert.equal(a.add('d', '1').date(), 13, 'Add date');
    assert.equal(a.add('w', '1').date(), 20, 'Add week');
    assert.equal(a.add('M', '1').month(), 10, 'Add month');
    assert.equal(a.add('y', '1').year(), 2012, 'Add year');
    assert.equal(a.add('Q', '1').month(), 1, 'Add quarter');
});

test('subtract strings string short args', function (assert) {
    var a = moment();
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    assert.equal(a.subtract('ms', '50').milliseconds(), 450, 'Subtract milliseconds');
    assert.equal(a.subtract('s', '1').seconds(), 7, 'Subtract seconds');
    assert.equal(a.subtract('m', '1').minutes(), 6, 'Subtract minutes');
    assert.equal(a.subtract('h', '1').hours(), 5, 'Subtract hours');
    assert.equal(a.subtract('d', '1').date(), 11, 'Subtract date');
    assert.equal(a.subtract('w', '1').date(), 4, 'Subtract week');
    assert.equal(a.subtract('M', '1').month(), 8, 'Subtract month');
    assert.equal(a.subtract('y', '1').year(), 2010, 'Subtract year');
    assert.equal(a.subtract('Q', '1').month(), 5, 'Subtract quarter');
});

test('add strings string short', function (assert) {
    var a = moment();
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    assert.equal(a.add('50', 'ms').milliseconds(), 550, 'Add milliseconds');
    assert.equal(a.add('1', 's').seconds(), 9, 'Add seconds');
    assert.equal(a.add('1', 'm').minutes(), 8, 'Add minutes');
    assert.equal(a.add('1', 'h').hours(), 7, 'Add hours');
    assert.equal(a.add('1', 'd').date(), 13, 'Add date');
    assert.equal(a.add('1', 'w').date(), 20, 'Add week');
    assert.equal(a.add('1', 'M').month(), 10, 'Add month');
    assert.equal(a.add('1', 'y').year(), 2012, 'Add year');
    assert.equal(a.add('1', 'Q').month(), 1, 'Add quarter');
});

test('subtract strings string short', function (assert) {
    var a = moment();
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(500);

    assert.equal(a.subtract('50', 'ms').milliseconds(), 450, 'Subtract milliseconds');
    assert.equal(a.subtract('1', 's').seconds(), 7, 'Subtract seconds');
    assert.equal(a.subtract('1', 'm').minutes(), 6, 'Subtract minutes');
    assert.equal(a.subtract('1', 'h').hours(), 5, 'Subtract hours');
    assert.equal(a.subtract('1', 'd').date(), 11, 'Subtract date');
    assert.equal(a.subtract('1', 'w').date(), 4, 'Subtract week');
    assert.equal(a.subtract('1', 'M').month(), 8, 'Subtract month');
    assert.equal(a.subtract('1', 'y').year(), 2010, 'Subtract year');
    assert.equal(a.subtract('1', 'Q').month(), 5, 'Subtract quarter');
});

test('add across DST', function (assert) {
    // Detect Safari bug and bail. Hours on 13th March 2011 are shifted
    // with 1 ahead.
    if (new Date(2011, 2, 13, 5, 0, 0).getHours() !== 5) {
        return;
    }

    var a = moment(new Date(2011, 2, 12, 5, 0, 0)),
        b = moment(new Date(2011, 2, 12, 5, 0, 0)),
        c = moment(new Date(2011, 2, 12, 5, 0, 0)),
        d = moment(new Date(2011, 2, 12, 5, 0, 0)),
        e = moment(new Date(2011, 2, 12, 5, 0, 0));
    a.add(1, 'days');
    b.add(24, 'hours');
    c.add(1, 'months');
    e.add(1, 'quarter');

    assert.equal(a.hours(), 5, 'adding days over DST difference should result in the same hour');
    if (b.isDST() && !d.isDST()) {
        assert.equal(b.hours(), 6, 'adding hours over DST difference should result in a different hour');
    } else if (!b.isDST() && d.isDST()) {
        assert.equal(b.hours(), 4, 'adding hours over DST difference should result in a different hour');
    } else {
        assert.equal(b.hours(), 5, 'adding hours over DST difference should result in a same hour if the timezone does not have daylight savings time');
    }
    assert.equal(c.hours(), 5, 'adding months over DST difference should result in the same hour');
    assert.equal(e.hours(), 5, 'adding quarters over DST difference should result in the same hour');
});
//===================================================================================================================================================================================================================== //>
module('create');

test('array', function (assert) {
    assert.ok(moment([2010]).toDate() instanceof Date, '[2010]');
    assert.ok(moment([2010, 1]).toDate() instanceof Date, '[2010, 1]');
    assert.ok(moment([2010, 1, 12]).toDate() instanceof Date, '[2010, 1, 12]');
    assert.ok(moment([2010, 1, 12, 1]).toDate() instanceof Date, '[2010, 1, 12, 1]');
    assert.ok(moment([2010, 1, 12, 1, 1]).toDate() instanceof Date, '[2010, 1, 12, 1, 1]');
    assert.ok(moment([2010, 1, 12, 1, 1, 1]).toDate() instanceof Date, '[2010, 1, 12, 1, 1, 1]');
    assert.ok(moment([2010, 1, 12, 1, 1, 1, 1]).toDate() instanceof Date, '[2010, 1, 12, 1, 1, 1, 1]');
    assert.equal(+moment(new Date(2010, 1, 14, 15, 25, 50, 125)), +moment([2010, 1, 14, 15, 25, 50, 125]), 'constructing with array === constructing with new Date()');
});

test('array copying', function (assert) {
    var importantArray = [2009, 11];
    moment(importantArray);
    assert.deepEqual(importantArray, [2009, 11], 'initializer should not mutate the original array');
});

test('object', function (assert) {
    var fmt = 'YYYY-MM-DD HH:mm:ss.SSS',
        tests = [
            [{year: 2010}, '2010-01-01 00:00:00.000'],
            [{year: 2010, month: 1}, '2010-02-01 00:00:00.000'],
            [{year: 2010, month: 1, day: 12}, '2010-02-12 00:00:00.000'],
            [{year: 2010, month: 1, date: 12}, '2010-02-12 00:00:00.000'],
            [{year: 2010, month: 1, day: 12, hours: 1}, '2010-02-12 01:00:00.000'],
            [{year: 2010, month: 1, date: 12, hours: 1}, '2010-02-12 01:00:00.000'],
            [{year: 2010, month: 1, day: 12, hours: 1, minutes: 1}, '2010-02-12 01:01:00.000'],
            [{year: 2010, month: 1, date: 12, hours: 1, minutes: 1}, '2010-02-12 01:01:00.000'],
            [{year: 2010, month: 1, day: 12, hours: 1, minutes: 1, seconds: 1}, '2010-02-12 01:01:01.000'],
            [{year: 2010, month: 1, day: 12, hours: 1, minutes: 1, seconds: 1, milliseconds: 1}, '2010-02-12 01:01:01.001'],
            [{years: 2010, months: 1, days: 14, hours: 15, minutes: 25, seconds: 50, milliseconds: 125}, '2010-02-14 15:25:50.125'],
            [{year: 2010, month: 1, day: 14, hour: 15, minute: 25, second: 50, millisecond: 125}, '2010-02-14 15:25:50.125'],
            [{y: 2010, M: 1, d: 14, h: 15, m: 25, s: 50, ms: 125}, '2010-02-14 15:25:50.125']
        ], i;
    for (i = 0; i < tests.length; ++i) {
        assert.equal(moment(tests[i][0]).format(fmt), tests[i][1]);
    }
});

test('multi format array copying', function (assert) {
    var importantArray = ['MM/DD/YYYY', 'YYYY-MM-DD', 'MM-DD-YYYY'];
    moment('1999-02-13', importantArray);
    assert.deepEqual(importantArray, ['MM/DD/YYYY', 'YYYY-MM-DD', 'MM-DD-YYYY'], 'initializer should not mutate the original array');
});

test('number', function (assert) {
    assert.ok(moment(1000).toDate() instanceof Date, '1000');
    assert.equal(moment(1000).valueOf(), 1000, 'asserting valueOf');
    assert.equal(moment.utc(1000).valueOf(), 1000, 'asserting valueOf');
});
/* REMOVED: .Net não suporta datas em formato unix
test('unix', function (assert) {
    assert.equal(moment.unix(1).valueOf(), 1000, '1 unix timestamp == 1000 Date.valueOf');
    assert.equal(moment(1000).unix(), 1, '1000 Date.valueOf == 1 unix timestamp');
    assert.equal(moment.unix(1000).valueOf(), 1000000, '1000 unix timestamp == 1000000 Date.valueOf');
    assert.equal(moment(1500).unix(), 1, '1500 Date.valueOf == 1 unix timestamp');
    assert.equal(moment(1900).unix(), 1, '1900 Date.valueOf == 1 unix timestamp');
    assert.equal(moment(2100).unix(), 2, '2100 Date.valueOf == 2 unix timestamp');
    assert.equal(moment(1333129333524).unix(), 1333129333, '1333129333524 Date.valueOf == 1333129333 unix timestamp');
    assert.equal(moment(1333129333524000).unix(), 1333129333524, '1333129333524000 Date.valueOf == 1333129333524 unix timestamp');
});
*/

test('date', function (assert) {
    assert.ok(moment(new Date()).toDate() instanceof Date, 'new Date()');
});

test('date mutation', function (assert) {
    var a = new Date();
    assert.ok(moment(a).toDate() !== a, 'the date moment uses should not be the date passed in');
});

test('moment', function (assert) {
    assert.ok(moment(moment()).toDate() instanceof Date, 'moment(moment())');
    assert.ok(moment(moment(moment())).toDate() instanceof Date, 'moment(moment(moment()))');
});

test('cloning moment should only copy own properties', function (assert) {
    assert.ok(!moment().clone().hasOwnProperty('month'), 'Should not clone prototype methods');
});

test('cloning moment works with weird clones', function (assert) {
    var extend = function (a, b) {
        var i;
        for (i in b) {
            a[i] = b[i];
        }
        return a;
    },
    now = moment(),
    nowu = moment.utc();

    assert.equal(+extend({}, now).clone(), +now, 'cloning extend-ed now is now');
    assert.equal(+extend({}, nowu).clone(), +nowu, 'cloning extend-ed utc now is utc now');
});

test('cloning respects moment.momentProperties', function (assert) {
    var m = moment();

    assert.equal(m.clone()._special, undefined, 'cloning ignores extra properties');
    m._special = 'bacon';
    moment.momentProperties.push('_special');
    assert.equal(m.clone()._special, 'bacon', 'cloning respects momentProperties');
    moment.momentProperties.pop();
});

test('undefined', function (assert) {
    assert.ok(moment().toDate() instanceof Date, 'undefined');
});

test('iso format 24hrs', function (assert) {
    assert.equal(moment('2014-01-01T24:00:00.000').format('YYYY-MM-DD[T]HH:mm:ss.SSS'),
            '2014-01-02T00:00:00.000', 'iso format with 24:00 localtime');
    assert.equal(moment.utc('2014-01-01T24:00:00.000').format('YYYY-MM-DD[T]HH:mm:ss.SSS'),
            '2014-01-02T00:00:00.000', 'iso format with 24:00 utc');
});

test('string without format - json', function (assert) {
    assert.equal(moment('Date(1325132654000)').valueOf(), 1325132654000, 'Date(1325132654000)');
    assert.equal(moment('Date(-1325132654000)').valueOf(), -1325132654000, 'Date(-1325132654000)');
    assert.equal(moment('/Date(1325132654000)/').valueOf(), 1325132654000, '/Date(1325132654000)/');
    assert.equal(moment('/Date(1325132654000+0700)/').valueOf(), 1325132654000, '/Date(1325132654000+0700)/');
    assert.equal(moment('/Date(1325132654000-0700)/').valueOf(), 1325132654000, '/Date(1325132654000-0700)/');
});

test('string with format dropped am/pm bug', function (assert) {
    moment.locale('en');

    assert.equal(moment('05/1/2012 12:25:00', 'MM/DD/YYYY h:m:s a').format('MM/DD/YYYY'), '05/01/2012', 'should not break if am/pm is left off from the parsing tokens');
    assert.equal(moment('05/1/2012 12:25:00 am', 'MM/DD/YYYY h:m:s a').format('MM/DD/YYYY'), '05/01/2012', 'should not break if am/pm is left off from the parsing tokens');
    assert.equal(moment('05/1/2012 12:25:00 pm', 'MM/DD/YYYY h:m:s a').format('MM/DD/YYYY'), '05/01/2012', 'should not break if am/pm is left off from the parsing tokens');

    assert.ok(moment('05/1/2012 12:25:00', 'MM/DD/YYYY h:m:s a').isValid());
    assert.ok(moment('05/1/2012 12:25:00 am', 'MM/DD/YYYY h:m:s a').isValid());
    assert.ok(moment('05/1/2012 12:25:00 pm', 'MM/DD/YYYY h:m:s a').isValid());
});

test('empty string with formats', function (assert) {
    assert.equal(moment('', 'MM').format('YYYY-MM-DD HH:mm:ss'), 'Invalid date');
    assert.equal(moment(' ', 'MM').format('YYYY-MM-DD HH:mm:ss'), 'Invalid date');
    assert.equal(moment(' ', 'DD').format('YYYY-MM-DD HH:mm:ss'), 'Invalid date');
    assert.equal(moment(' ', ['MM', 'DD']).format('YYYY-MM-DD HH:mm:ss'), 'Invalid date');

    assert.ok(!moment('', 'MM').isValid());
    assert.ok(!moment(' ', 'MM').isValid());
    assert.ok(!moment(' ', 'DD').isValid());
    assert.ok(!moment(' ', ['MM', 'DD']).isValid());
});

test('defaulting to current date', function (assert) {
    var now = moment();
    assert.equal(moment('12:13:14', 'hh:mm:ss').format('YYYY-MM-DD hh:mm:ss'),
                 now.clone().hour(12).minute(13).second(14).format('YYYY-MM-DD hh:mm:ss'),
                 'given only time default to current date');
    assert.equal(moment('05', 'DD').format('YYYY-MM-DD'),
                 now.clone().date(5).format('YYYY-MM-DD'),
                 'given day of month default to current month, year');
    assert.equal(moment('05', 'MM').format('YYYY-MM-DD'),
                 now.clone().month(4).date(1).format('YYYY-MM-DD'),
                 'given month default to current year');
    assert.equal(moment('1996', 'YYYY').format('YYYY-MM-DD'),
                 now.clone().year(1996).month(0).date(1).format('YYYY-MM-DD'),
                 'given year do not default');
});

test('matching am/pm', function (assert) {
    assert.equal(moment('2012-09-03T03:00PM',   'YYYY-MM-DDThh:mmA').format('YYYY-MM-DDThh:mmA'), '2012-09-03T03:00PM', 'am/pm should parse correctly for PM');
    assert.equal(moment('2012-09-03T03:00P.M.', 'YYYY-MM-DDThh:mmA').format('YYYY-MM-DDThh:mmA'), '2012-09-03T03:00PM', 'am/pm should parse correctly for P.M.');
    assert.equal(moment('2012-09-03T03:00P',    'YYYY-MM-DDThh:mmA').format('YYYY-MM-DDThh:mmA'), '2012-09-03T03:00PM', 'am/pm should parse correctly for P');
    assert.equal(moment('2012-09-03T03:00pm',   'YYYY-MM-DDThh:mmA').format('YYYY-MM-DDThh:mmA'), '2012-09-03T03:00PM', 'am/pm should parse correctly for pm');
    assert.equal(moment('2012-09-03T03:00p.m.', 'YYYY-MM-DDThh:mmA').format('YYYY-MM-DDThh:mmA'), '2012-09-03T03:00PM', 'am/pm should parse correctly for p.m.');
    assert.equal(moment('2012-09-03T03:00p',    'YYYY-MM-DDThh:mmA').format('YYYY-MM-DDThh:mmA'), '2012-09-03T03:00PM', 'am/pm should parse correctly for p');

    assert.equal(moment('2012-09-03T03:00AM',   'YYYY-MM-DDThh:mmA').format('YYYY-MM-DDThh:mmA'), '2012-09-03T03:00AM', 'am/pm should parse correctly for AM');
    assert.equal(moment('2012-09-03T03:00A.M.', 'YYYY-MM-DDThh:mmA').format('YYYY-MM-DDThh:mmA'), '2012-09-03T03:00AM', 'am/pm should parse correctly for A.M.');
    assert.equal(moment('2012-09-03T03:00A',    'YYYY-MM-DDThh:mmA').format('YYYY-MM-DDThh:mmA'), '2012-09-03T03:00AM', 'am/pm should parse correctly for A');
    assert.equal(moment('2012-09-03T03:00am',   'YYYY-MM-DDThh:mmA').format('YYYY-MM-DDThh:mmA'), '2012-09-03T03:00AM', 'am/pm should parse correctly for am');
    assert.equal(moment('2012-09-03T03:00a.m.', 'YYYY-MM-DDThh:mmA').format('YYYY-MM-DDThh:mmA'), '2012-09-03T03:00AM', 'am/pm should parse correctly for a.m.');
    assert.equal(moment('2012-09-03T03:00a',    'YYYY-MM-DDThh:mmA').format('YYYY-MM-DDThh:mmA'), '2012-09-03T03:00AM', 'am/pm should parse correctly for a');

    assert.equal(moment('5:00p.m.March 4 2012', 'h:mmAMMMM D YYYY').format('YYYY-MM-DDThh:mmA'), '2012-03-04T05:00PM', 'am/pm should parse correctly before month names');
});

test('string with format', function (assert) {
    moment.locale('en');
    var a = [
        ['YYYY-Q',              '2014-4'],
        ['MM-DD-YYYY',          '12-02-1999'],
        ['DD-MM-YYYY',          '12-02-1999'],
        ['DD/MM/YYYY',          '12/02/1999'],
        ['DD_MM_YYYY',          '12_02_1999'],
        ['DD:MM:YYYY',          '12:02:1999'],
        ['D-M-YY',              '2-2-99'],
        ['YY',                  '99'],
        ['DDD-YYYY',            '300-1999'],
        ['DD-MM-YYYY h:m:s',    '12-02-1999 2:45:10'],
        ['DD-MM-YYYY h:m:s a',  '12-02-1999 2:45:10 am'],
        ['DD-MM-YYYY h:m:s a',  '12-02-1999 2:45:10 pm'],
        ['h:mm a',              '12:00 pm'],
        ['h:mm a',              '12:30 pm'],
        ['h:mm a',              '12:00 am'],
        ['h:mm a',              '12:30 am'],
        ['HH:mm',               '12:00'],
        ['YYYY-MM-DDTHH:mm:ss', '2011-11-11T11:11:11'],
        ['MM-DD-YYYY [M]',      '12-02-1999 M'],
        ['ddd MMM DD HH:mm:ss YYYY', 'Tue Apr 07 22:52:51 2009'],
        ['HH:mm:ss',            '12:00:00'],
        ['HH:mm:ss',            '12:30:00'],
        ['HH:mm:ss',            '00:00:00'],
        ['HH:mm:ss S',          '00:30:00 1'],
        ['HH:mm:ss SS',         '00:30:00 12'],
        ['HH:mm:ss SSS',        '00:30:00 123'],
        ['HH:mm:ss S',          '00:30:00 7'],
        ['HH:mm:ss SS',         '00:30:00 78'],
        ['HH:mm:ss SSS',        '00:30:00 789'],
        ['X',                   '1234567890'],
        ['x',                   '1234567890123'],
        ['LT',                  '12:30 AM'],
        ['LTS',                 '12:30:29 AM'],
        ['L',                   '09/02/1999'],
        ['l',                   '9/2/1999'],
        ['LL',                  'September 2, 1999'],
        ['ll',                  'Sep 2, 1999'],
        ['LLL',                 'September 2, 1999 12:30 AM'],
        ['lll',                 'Sep 2, 1999 12:30 AM'],
        ['LLLL',                'Thursday, September 2, 1999 12:30 AM'],
        ['llll',                'Thu, Sep 2, 1999 12:30 AM']
    ],
    m,
    i;

    for (i = 0; i < a.length; i++) {
        m = moment(a[i][1], a[i][0]);
        assert.ok(m.isValid());
        assert.equal(m.format(a[i][0]), a[i][1], a[i][0] + ' ---> ' + a[i][1]);
    }
});
/* REMOVED: .Net não suporta datas em formato unix
test('unix timestamp format', function (assert) {
    var formats = ['X', 'X.S', 'X.SS', 'X.SSS'], i, format;

    for (i = 0; i < formats.length; i++) {
        format = formats[i];
        assert.equal(moment('1234567890',     format).valueOf(), 1234567890 * 1000,       format + ' matches timestamp without milliseconds');
        assert.equal(moment('1234567890.1',   format).valueOf(), 1234567890 * 1000 + 100, format + ' matches timestamp with deciseconds');
        assert.equal(moment('1234567890.12',  format).valueOf(), 1234567890 * 1000 + 120, format + ' matches timestamp with centiseconds');
        assert.equal(moment('1234567890.123', format).valueOf(), 1234567890 * 1000 + 123, format + ' matches timestamp with milliseconds');
    }
});
*/
/* REMOVED: .Net não suporta datas em formato unix
test('unix offset milliseconds', function (assert) {
    assert.equal(moment('1234567890123', 'x').valueOf(), 1234567890123, 'x matches unix offset in milliseconds');
});
*/
test('milliseconds format', function (assert) {
    assert.equal(moment('1', 'S').get('ms'), 100, 'deciseconds');
    // assert.equal(moment('10', 'S', true).isValid(), false, 'deciseconds with two digits');
    // assert.equal(moment('1', 'SS', true).isValid(), false, 'centiseconds with one digits');
    assert.equal(moment('12', 'SS').get('ms'), 120, 'centiseconds');
    // assert.equal(moment('123', 'SS', true).isValid(), false, 'centiseconds with three digits');
    assert.equal(moment('123', 'SSS').get('ms'), 123, 'milliseconds');
    assert.equal(moment('1234', 'SSSS').get('ms'), 123, 'milliseconds with SSSS');
    assert.equal(moment('123456789101112', 'SSSS').get('ms'), 123, 'milliseconds with SSSS');
});

test('string with format no separators', function (assert) {
    moment.locale('en');
    var a = [
        ['MMDDYYYY',          '12021999'],
        ['DDMMYYYY',          '12021999'],
        ['YYYYMMDD',          '19991202'],
        ['DDMMMYYYY',         '10Sep2001']
    ], i;

    for (i = 0; i < a.length; i++) {
        assert.equal(moment(a[i][1], a[i][0]).format(a[i][0]), a[i][1], a[i][0] + ' ---> ' + a[i][1]);
    }
});

test('string with format (timezone)', function (assert) {
    assert.equal(moment('5 -0700', 'H ZZ').toDate().getUTCHours(), 12, 'parse hours \'5 -0700\' ---> \'H ZZ\'');
    assert.equal(moment('5 -07:00', 'H Z').toDate().getUTCHours(), 12, 'parse hours \'5 -07:00\' ---> \'H Z\'');
    assert.equal(moment('5 -0730', 'H ZZ').toDate().getUTCMinutes(), 30, 'parse hours \'5 -0730\' ---> \'H ZZ\'');
    assert.equal(moment('5 -07:30', 'H Z').toDate().getUTCMinutes(), 30, 'parse hours \'5 -07:0\' ---> \'H Z\'');
    assert.equal(moment('5 +0100', 'H ZZ').toDate().getUTCHours(), 4, 'parse hours \'5 +0100\' ---> \'H ZZ\'');
    assert.equal(moment('5 +01:00', 'H Z').toDate().getUTCHours(), 4, 'parse hours \'5 +01:00\' ---> \'H Z\'');
    assert.equal(moment('5 +0130', 'H ZZ').toDate().getUTCMinutes(), 30, 'parse hours \'5 +0130\' ---> \'H ZZ\'');
    assert.equal(moment('5 +01:30', 'H Z').toDate().getUTCMinutes(), 30, 'parse hours \'5 +01:30\' ---> \'H Z\'');
});

test('string with format (timezone offset)', function (assert) {
    var a, b, c, d, e, f;
    a = new Date(Date.UTC(2011, 0, 1, 1));
    b = moment('2011 1 1 0 -01:00', 'YYYY MM DD HH Z');
    assert.equal(a.getHours(), b.hours(), 'date created with utc == parsed string with timezone offset');
    assert.equal(+a, +b, 'date created with utc == parsed string with timezone offset');
    c = moment('2011 2 1 10 -05:00', 'YYYY MM DD HH Z');
    d = moment('2011 2 1 8 -07:00', 'YYYY MM DD HH Z');
    assert.equal(c.hours(), d.hours(), '10 am central time == 8 am pacific time');
    e = moment.utc('Fri, 20 Jul 2012 17:15:00', 'ddd, DD MMM YYYY HH:mm:ss');
    f = moment.utc('Fri, 20 Jul 2012 10:15:00 -0700', 'ddd, DD MMM YYYY HH:mm:ss ZZ');
    assert.equal(e.hours(), f.hours(), 'parse timezone offset in utc');
});

test('string with timezone around start of year', function (assert) {
    assert.equal(moment('2000-01-01T00:00:00.000+01:00').toISOString(), '1999-12-31T23:00:00.000Z', '+1:00 around 2000');
    assert.equal(moment('2000-01-01T00:00:00.000-01:00').toISOString(), '2000-01-01T01:00:00.000Z', '-1:00 around 2000');
    assert.equal(moment('1970-01-01T00:00:00.000+01:00').toISOString(), '1969-12-31T23:00:00.000Z', '+1:00 around 1970');
    assert.equal(moment('1970-01-01T00:00:00.000-01:00').toISOString(), '1970-01-01T01:00:00.000Z', '-1:00 around 1970');
    assert.equal(moment('1200-01-01T00:00:00.000+01:00').toISOString(), '1199-12-31T23:00:00.000Z', '+1:00 around 1200');
    assert.equal(moment('1200-01-01T00:00:00.000-01:00').toISOString(), '1200-01-01T01:00:00.000Z', '-1:00 around 1200');
});

test('string with array of formats', function (assert) {
    assert.equal(moment('11-02-1999', ['MM-DD-YYYY', 'DD-MM-YYYY']).format('MM DD YYYY'), '11 02 1999', 'switching month and day');
    assert.equal(moment('02-11-1999', ['MM/DD/YYYY', 'YYYY MM DD', 'MM-DD-YYYY']).format('MM DD YYYY'), '02 11 1999', 'year last');
    assert.equal(moment('1999-02-11', ['MM/DD/YYYY', 'YYYY MM DD', 'MM-DD-YYYY']).format('MM DD YYYY'), '02 11 1999', 'year first');

    assert.equal(moment('02-11-1999', ['MM/DD/YYYY', 'YYYY MM DD']).format('MM DD YYYY'), '02 11 1999', 'year last');
    assert.equal(moment('1999-02-11', ['MM/DD/YYYY', 'YYYY MM DD']).format('MM DD YYYY'), '02 11 1999', 'year first');
    assert.equal(moment('02-11-1999', ['YYYY MM DD', 'MM/DD/YYYY']).format('MM DD YYYY'), '02 11 1999', 'year last');
    assert.equal(moment('1999-02-11', ['YYYY MM DD', 'MM/DD/YYYY']).format('MM DD YYYY'), '02 11 1999', 'year first');

    assert.equal(moment('13-11-1999', ['MM/DD/YYYY', 'DD/MM/YYYY']).format('MM DD YYYY'), '11 13 1999', 'second must be month');
    assert.equal(moment('11-13-1999', ['MM/DD/YYYY', 'DD/MM/YYYY']).format('MM DD YYYY'), '11 13 1999', 'first must be month');
    assert.equal(moment('01-02-2000', ['MM/DD/YYYY', 'DD/MM/YYYY']).format('MM DD YYYY'), '01 02 2000', 'either can be a month, month first format');
    assert.equal(moment('02-01-2000', ['DD/MM/YYYY', 'MM/DD/YYYY']).format('MM DD YYYY'), '01 02 2000', 'either can be a month, day first format');

    assert.equal(moment('11-02-10', ['MM/DD/YY', 'YY MM DD', 'DD-MM-YY']).format('MM DD YYYY'), '02 11 2010', 'all unparsed substrings have influence on format penalty');
    assert.equal(moment('11-02-10', ['MM-DD-YY HH:mm', 'YY MM DD']).format('MM DD YYYY'), '02 10 2011', 'prefer formats without extra tokens');
    assert.equal(moment('11-02-10 junk', ['MM-DD-YY', 'YY.MM.DD junk']).format('MM DD YYYY'), '02 10 2011', 'prefer formats that dont result in extra characters');
    assert.equal(moment('11-22-10', ['YY-MM-DD', 'YY-DD-MM']).format('MM DD YYYY'), '10 22 2011', 'prefer valid results');

    assert.equal(moment('gibberish', ['YY-MM-DD', 'YY-DD-MM']).format('MM DD YYYY'), 'Invalid date', 'doest throw for invalid strings');
    assert.equal(moment('gibberish', []).format('MM DD YYYY'), 'Invalid date', 'doest throw for an empty array');

    //https://github.com/moment/moment/issues/1143
    assert.equal(moment(
        'System Administrator and Database Assistant (7/1/2011), System Administrator and Database Assistant (7/1/2011), Database Coordinator (7/1/2011), Vice President (7/1/2011), System Administrator and Database Assistant (5/31/2012), Database Coordinator (7/1/2012), System Administrator and Database Assistant (7/1/2013)',
        ['MM/DD/YYYY', 'MM-DD-YYYY', 'YYYY-MM-DD', 'YYYY-MM-DDTHH:mm:ssZ'])
        .format('YYYY-MM-DD'), '2011-07-01', 'Works for long strings');

    assert.equal(moment('11-02-10', ['MM.DD.YY', 'DD-MM-YY']).format('MM DD YYYY'), '02 11 2010', 'escape RegExp special characters on comparing');

    assert.equal(moment('13-10-98', ['DD MM YY', 'DD MM YYYY'])._f, 'DD MM YY', 'use two digit year');
    assert.equal(moment('13-10-1998', ['DD MM YY', 'DD MM YYYY'])._f, 'DD MM YYYY', 'use four digit year');

    assert.equal(moment('01', ['MM', 'DD'])._f, 'MM', 'Should use first valid format');
});

test('string with array of formats + ISO', function (assert) {
    assert.equal(moment('1994', [moment.ISO_8601, 'MM', 'HH:mm', 'YYYY']).year(), 1994, 'iso: assert parse YYYY');
    assert.equal(moment('17:15', [moment.ISO_8601, 'MM', 'HH:mm', 'YYYY']).hour(), 17, 'iso: assert parse HH:mm (1)');
    assert.equal(moment('17:15', [moment.ISO_8601, 'MM', 'HH:mm', 'YYYY']).minutes(), 15, 'iso: assert parse HH:mm (2)');
    assert.equal(moment('06', [moment.ISO_8601, 'MM', 'HH:mm', 'YYYY']).month(), 6 - 1, 'iso: assert parse MM');
    assert.equal(moment('2012-06-01', [moment.ISO_8601, 'MM', 'HH:mm', 'YYYY']).parsingFlags().iso, true, 'iso: assert parse iso');
    assert.equal(moment('2014-05-05', [moment.ISO_8601, 'YYYY-MM-DD']).parsingFlags().iso, true, 'iso: edge case array precedence iso');
    assert.equal(moment('2014-05-05', ['YYYY-MM-DD', moment.ISO_8601]).parsingFlags().iso, false, 'iso: edge case array precedence not iso');
});

test('string with format - years', function (assert) {
    assert.equal(moment('67', 'YY').format('YYYY'), '2067', '67 > 2067');
    assert.equal(moment('68', 'YY').format('YYYY'), '2068', '68 > 2068');
    assert.equal(moment('69', 'YY').format('YYYY'), '1969', '69 > 1969');
    assert.equal(moment('70', 'YY').format('YYYY'), '1970', '70 > 1970');
});

test('implicit cloning', function (assert) {
    var momentA = moment([2011, 10, 10]),
    momentB = moment(momentA);
    momentA.month(5);
    assert.equal(momentB.month(), 10, 'Calling moment() on a moment will create a clone');
    assert.equal(momentA.month(), 5, 'Calling moment() on a moment will create a clone');
});

test('explicit cloning', function (assert) {
    var momentA = moment([2011, 10, 10]),
    momentB = momentA.clone();
    momentA.month(5);
    assert.equal(momentB.month(), 10, 'Calling moment() on a moment will create a clone');
    assert.equal(momentA.month(), 5, 'Calling moment() on a moment will create a clone');
});

test('cloning carrying over utc mode', function (assert) {
    assert.equal(moment().local().clone()._isUTC, false, 'An explicit cloned local moment should have _isUTC == false');
    assert.equal(moment().utc().clone()._isUTC, true, 'An cloned utc moment should have _isUTC == true');
    assert.equal(moment().clone()._isUTC, false, 'An explicit cloned local moment should have _isUTC == false');
    assert.equal(moment.utc().clone()._isUTC, true, 'An explicit cloned utc moment should have _isUTC == true');
    assert.equal(moment(moment().local())._isUTC, false, 'An implicit cloned local moment should have _isUTC == false');
    assert.equal(moment(moment().utc())._isUTC, true, 'An implicit cloned utc moment should have _isUTC == true');
    assert.equal(moment(moment())._isUTC, false, 'An implicit cloned local moment should have _isUTC == false');
    assert.equal(moment(moment.utc())._isUTC, true, 'An implicit cloned utc moment should have _isUTC == true');
});

test('parsing iso', function (assert) {
    var offset = moment([2011, 9, 8]).utcOffset(),
    pad = function (input) {
        if (input < 10) {
            return '0' + input;
        }
        return '' + input;
    },
    hourOffset = (offset > 0 ? Math.floor(offset / 60) : Math.ceil(offset / 60)),
    minOffset = offset - (hourOffset * 60),
    tz = (offset >= 0) ?
        '+' + pad(hourOffset) + ':' + pad(minOffset) :
        '-' + pad(-hourOffset) + ':' + pad(-minOffset),
    tz2 = tz.replace(':', ''),
    tz3 = tz2.slice(0, 3),
    formats = [
        ['2011-10-08',                    '2011-10-08T00:00:00.000' + tz],
        ['2011-10-08T18',                 '2011-10-08T18:00:00.000' + tz],
        ['2011-10-08T18:04',              '2011-10-08T18:04:00.000' + tz],
        ['2011-10-08T18:04:20',           '2011-10-08T18:04:20.000' + tz],
        ['2011-10-08T18:04' + tz,         '2011-10-08T18:04:00.000' + tz],
        ['2011-10-08T18:04:20' + tz,      '2011-10-08T18:04:20.000' + tz],
        ['2011-10-08T18:04' + tz2,        '2011-10-08T18:04:00.000' + tz],
        ['2011-10-08T18:04:20' + tz2,     '2011-10-08T18:04:20.000' + tz],
        ['2011-10-08T18:04' + tz3,        '2011-10-08T18:04:00.000' + tz],
        ['2011-10-08T18:04:20' + tz3,     '2011-10-08T18:04:20.000' + tz],
        ['2011-10-08T18:04:20.1' + tz2,   '2011-10-08T18:04:20.100' + tz],
        ['2011-10-08T18:04:20.11' + tz2,  '2011-10-08T18:04:20.110' + tz],
        ['2011-10-08T18:04:20.111' + tz2, '2011-10-08T18:04:20.111' + tz],
        ['2011-10-08 18',                 '2011-10-08T18:00:00.000' + tz],
        ['2011-10-08 18:04',              '2011-10-08T18:04:00.000' + tz],
        ['2011-10-08 18:04:20',           '2011-10-08T18:04:20.000' + tz],
        ['2011-10-08 18:04' + tz,         '2011-10-08T18:04:00.000' + tz],
        ['2011-10-08 18:04:20' + tz,      '2011-10-08T18:04:20.000' + tz],
        ['2011-10-08 18:04' + tz2,        '2011-10-08T18:04:00.000' + tz],
        ['2011-10-08 18:04:20' + tz2,     '2011-10-08T18:04:20.000' + tz],
        ['2011-10-08 18:04' + tz3,        '2011-10-08T18:04:00.000' + tz],
        ['2011-10-08 18:04:20' + tz3,     '2011-10-08T18:04:20.000' + tz],
        ['2011-10-08 18:04:20.1' + tz2,   '2011-10-08T18:04:20.100' + tz],
        ['2011-10-08 18:04:20.11' + tz2,  '2011-10-08T18:04:20.110' + tz],
        ['2011-10-08 18:04:20.111' + tz2, '2011-10-08T18:04:20.111' + tz],
        ['2011-W40',                      '2011-10-03T00:00:00.000' + tz],
        ['2011-W40-6',                    '2011-10-08T00:00:00.000' + tz],
        ['2011-W40-6T18',                 '2011-10-08T18:00:00.000' + tz],
        ['2011-W40-6T18:04',              '2011-10-08T18:04:00.000' + tz],
        ['2011-W40-6T18:04:20',           '2011-10-08T18:04:20.000' + tz],
        ['2011-W40-6T18:04' + tz,         '2011-10-08T18:04:00.000' + tz],
        ['2011-W40-6T18:04:20' + tz,      '2011-10-08T18:04:20.000' + tz],
        ['2011-W40-6T18:04' + tz2,        '2011-10-08T18:04:00.000' + tz],
        ['2011-W40-6T18:04:20' + tz2,     '2011-10-08T18:04:20.000' + tz],
        ['2011-W40-6T18:04' + tz3,        '2011-10-08T18:04:00.000' + tz],
        ['2011-W40-6T18:04:20' + tz3,     '2011-10-08T18:04:20.000' + tz],
        ['2011-W40-6T18:04:20.1' + tz2,   '2011-10-08T18:04:20.100' + tz],
        ['2011-W40-6T18:04:20.11' + tz2,  '2011-10-08T18:04:20.110' + tz],
        ['2011-W40-6T18:04:20.111' + tz2, '2011-10-08T18:04:20.111' + tz],
        ['2011-W40-6 18',                 '2011-10-08T18:00:00.000' + tz],
        ['2011-W40-6 18:04',              '2011-10-08T18:04:00.000' + tz],
        ['2011-W40-6 18:04:20',           '2011-10-08T18:04:20.000' + tz],
        ['2011-W40-6 18:04' + tz,         '2011-10-08T18:04:00.000' + tz],
        ['2011-W40-6 18:04:20' + tz,      '2011-10-08T18:04:20.000' + tz],
        ['2011-W40-6 18:04' + tz2,        '2011-10-08T18:04:00.000' + tz],
        ['2011-W40-6 18:04:20' + tz2,     '2011-10-08T18:04:20.000' + tz],
        ['2011-W40-6 18:04' + tz3,        '2011-10-08T18:04:00.000' + tz],
        ['2011-W40-6 18:04:20' + tz3,     '2011-10-08T18:04:20.000' + tz],
        ['2011-W40-6 18:04:20.1' + tz2,   '2011-10-08T18:04:20.100' + tz],
        ['2011-W40-6 18:04:20.11' + tz2,  '2011-10-08T18:04:20.110' + tz],
        ['2011-W40-6 18:04:20.111' + tz2, '2011-10-08T18:04:20.111' + tz],
        ['2011-281',                      '2011-10-08T00:00:00.000' + tz],
        ['2011-281T18',                   '2011-10-08T18:00:00.000' + tz],
        ['2011-281T18:04',                '2011-10-08T18:04:00.000' + tz],
        ['2011-281T18:04:20',             '2011-10-08T18:04:20.000' + tz],
        ['2011-281T18:04' + tz,           '2011-10-08T18:04:00.000' + tz],
        ['2011-281T18:04:20' + tz,        '2011-10-08T18:04:20.000' + tz],
        ['2011-281T18:04' + tz2,          '2011-10-08T18:04:00.000' + tz],
        ['2011-281T18:04:20' + tz2,       '2011-10-08T18:04:20.000' + tz],
        ['2011-281T18:04' + tz3,          '2011-10-08T18:04:00.000' + tz],
        ['2011-281T18:04:20' + tz3,       '2011-10-08T18:04:20.000' + tz],
        ['2011-281T18:04:20.1' + tz2,     '2011-10-08T18:04:20.100' + tz],
        ['2011-281T18:04:20.11' + tz2,    '2011-10-08T18:04:20.110' + tz],
        ['2011-281T18:04:20.111' + tz2,   '2011-10-08T18:04:20.111' + tz],
        ['2011-281 18',                   '2011-10-08T18:00:00.000' + tz],
        ['2011-281 18:04',                '2011-10-08T18:04:00.000' + tz],
        ['2011-281 18:04:20',             '2011-10-08T18:04:20.000' + tz],
        ['2011-281 18:04' + tz,           '2011-10-08T18:04:00.000' + tz],
        ['2011-281 18:04:20' + tz,        '2011-10-08T18:04:20.000' + tz],
        ['2011-281 18:04' + tz2,          '2011-10-08T18:04:00.000' + tz],
        ['2011-281 18:04:20' + tz2,       '2011-10-08T18:04:20.000' + tz],
        ['2011-281 18:04' + tz3,          '2011-10-08T18:04:00.000' + tz],
        ['2011-281 18:04:20' + tz3,       '2011-10-08T18:04:20.000' + tz],
        ['2011-281 18:04:20.1' + tz2,     '2011-10-08T18:04:20.100' + tz],
        ['2011-281 18:04:20.11' + tz2,    '2011-10-08T18:04:20.110' + tz],
        ['2011-281 18:04:20.111' + tz2,   '2011-10-08T18:04:20.111' + tz]
    ], i;
    for (i = 0; i < formats.length; i++) {
        assert.equal(moment(formats[i][0]).format('YYYY-MM-DDTHH:mm:ss.SSSZ'), formats[i][1], 'moment should be able to parse ISO ' + formats[i][0]);
    }
});

test('parsing iso week year/week/weekday', function (assert) {
    assert.equal(moment.utc('2007-W01').format(), '2007-01-01T00:00:00+00:00', '2008 week 1 (1st Jan Mon)');
    assert.equal(moment.utc('2008-W01').format(), '2007-12-31T00:00:00+00:00', '2008 week 1 (1st Jan Tue)');
    assert.equal(moment.utc('2003-W01').format(), '2002-12-30T00:00:00+00:00', '2008 week 1 (1st Jan Wed)');
    assert.equal(moment.utc('2009-W01').format(), '2008-12-29T00:00:00+00:00', '2009 week 1 (1st Jan Thu)');
    assert.equal(moment.utc('2010-W01').format(), '2010-01-04T00:00:00+00:00', '2010 week 1 (1st Jan Fri)');
    assert.equal(moment.utc('2011-W01').format(), '2011-01-03T00:00:00+00:00', '2011 week 1 (1st Jan Sat)');
    assert.equal(moment.utc('2012-W01').format(), '2012-01-02T00:00:00+00:00', '2012 week 1 (1st Jan Sun)');
});

test('parsing week year/week/weekday (dow 1, doy 4)', function (assert) {
    moment.locale('dow:1,doy:4', {week: {dow: 1, doy: 4}});

    assert.equal(moment.utc('2007-01', 'gggg-ww').format(), '2007-01-01T00:00:00+00:00', '2007 week 1 (1st Jan Mon)');
    assert.equal(moment.utc('2008-01', 'gggg-ww').format(), '2007-12-31T00:00:00+00:00', '2008 week 1 (1st Jan Tue)');
    assert.equal(moment.utc('2003-01', 'gggg-ww').format(), '2002-12-30T00:00:00+00:00', '2003 week 1 (1st Jan Wed)');
    assert.equal(moment.utc('2009-01', 'gggg-ww').format(), '2008-12-29T00:00:00+00:00', '2009 week 1 (1st Jan Thu)');
    assert.equal(moment.utc('2010-01', 'gggg-ww').format(), '2010-01-04T00:00:00+00:00', '2010 week 1 (1st Jan Fri)');
    assert.equal(moment.utc('2011-01', 'gggg-ww').format(), '2011-01-03T00:00:00+00:00', '2011 week 1 (1st Jan Sat)');
    assert.equal(moment.utc('2012-01', 'gggg-ww').format(), '2012-01-02T00:00:00+00:00', '2012 week 1 (1st Jan Sun)');

    moment.defineLocale('dow:1,doy:4', null);
});

test('parsing week year/week/weekday (dow 1, doy 7)', function (assert) {
    moment.locale('dow:1,doy:7', {week: {dow: 1, doy: 7}});

    assert.equal(moment.utc('2007-01', 'gggg-ww').format(), '2007-01-01T00:00:00+00:00', '2007 week 1 (1st Jan Mon)');
    assert.equal(moment.utc('2008-01', 'gggg-ww').format(), '2007-12-31T00:00:00+00:00', '2008 week 1 (1st Jan Tue)');
    assert.equal(moment.utc('2003-01', 'gggg-ww').format(), '2002-12-30T00:00:00+00:00', '2003 week 1 (1st Jan Wed)');
    assert.equal(moment.utc('2009-01', 'gggg-ww').format(), '2008-12-29T00:00:00+00:00', '2009 week 1 (1st Jan Thu)');
    assert.equal(moment.utc('2010-01', 'gggg-ww').format(), '2009-12-28T00:00:00+00:00', '2010 week 1 (1st Jan Fri)');
    assert.equal(moment.utc('2011-01', 'gggg-ww').format(), '2010-12-27T00:00:00+00:00', '2011 week 1 (1st Jan Sat)');
    assert.equal(moment.utc('2012-01', 'gggg-ww').format(), '2011-12-26T00:00:00+00:00', '2012 week 1 (1st Jan Sun)');
    moment.defineLocale('dow:1,doy:7', null);
});

test('parsing week year/week/weekday (dow 0, doy 6)', function (assert) {
    moment.locale('dow:0,doy:6', {week: {dow: 0, doy: 6}});

    assert.equal(moment.utc('2007-01', 'gggg-ww').format(), '2006-12-31T00:00:00+00:00', '2007 week 1 (1st Jan Mon)');
    assert.equal(moment.utc('2008-01', 'gggg-ww').format(), '2007-12-30T00:00:00+00:00', '2008 week 1 (1st Jan Tue)');
    assert.equal(moment.utc('2003-01', 'gggg-ww').format(), '2002-12-29T00:00:00+00:00', '2003 week 1 (1st Jan Wed)');
    assert.equal(moment.utc('2009-01', 'gggg-ww').format(), '2008-12-28T00:00:00+00:00', '2009 week 1 (1st Jan Thu)');
    assert.equal(moment.utc('2010-01', 'gggg-ww').format(), '2009-12-27T00:00:00+00:00', '2010 week 1 (1st Jan Fri)');
    assert.equal(moment.utc('2011-01', 'gggg-ww').format(), '2010-12-26T00:00:00+00:00', '2011 week 1 (1st Jan Sat)');
    assert.equal(moment.utc('2012-01', 'gggg-ww').format(), '2012-01-01T00:00:00+00:00', '2012 week 1 (1st Jan Sun)');
    moment.defineLocale('dow:0,doy:6', null);
});

test('parsing week year/week/weekday (dow 6, doy 12)', function (assert) {
    moment.locale('dow:6,doy:12', {week: {dow: 6, doy: 12}});

    assert.equal(moment.utc('2007-01', 'gggg-ww').format(), '2006-12-30T00:00:00+00:00', '2007 week 1 (1st Jan Mon)');
    assert.equal(moment.utc('2008-01', 'gggg-ww').format(), '2007-12-29T00:00:00+00:00', '2008 week 1 (1st Jan Tue)');
    assert.equal(moment.utc('2003-01', 'gggg-ww').format(), '2002-12-28T00:00:00+00:00', '2003 week 1 (1st Jan Wed)');
    assert.equal(moment.utc('2009-01', 'gggg-ww').format(), '2008-12-27T00:00:00+00:00', '2009 week 1 (1st Jan Thu)');
    assert.equal(moment.utc('2010-01', 'gggg-ww').format(), '2009-12-26T00:00:00+00:00', '2010 week 1 (1st Jan Fri)');
    assert.equal(moment.utc('2011-01', 'gggg-ww').format(), '2011-01-01T00:00:00+00:00', '2011 week 1 (1st Jan Sat)');
    assert.equal(moment.utc('2012-01', 'gggg-ww').format(), '2011-12-31T00:00:00+00:00', '2012 week 1 (1st Jan Sun)');
});

test('parsing ISO with Z', function (assert) {
    var i, mom, formats = [
        ['2011-10-08T18:04',             '2011-10-08T18:04:00.000'],
        ['2011-10-08T18:04:20',          '2011-10-08T18:04:20.000'],
        ['2011-10-08T18:04:20.1',        '2011-10-08T18:04:20.100'],
        ['2011-10-08T18:04:20.11',       '2011-10-08T18:04:20.110'],
        ['2011-10-08T18:04:20.111',      '2011-10-08T18:04:20.111'],
        ['2011-W40-6T18',                '2011-10-08T18:00:00.000'],
        ['2011-W40-6T18:04',             '2011-10-08T18:04:00.000'],
        ['2011-W40-6T18:04:20',          '2011-10-08T18:04:20.000'],
        ['2011-W40-6T18:04:20.1',        '2011-10-08T18:04:20.100'],
        ['2011-W40-6T18:04:20.11',       '2011-10-08T18:04:20.110'],
        ['2011-W40-6T18:04:20.111',      '2011-10-08T18:04:20.111'],
        ['2011-281T18',                  '2011-10-08T18:00:00.000'],
        ['2011-281T18:04',               '2011-10-08T18:04:00.000'],
        ['2011-281T18:04:20',            '2011-10-08T18:04:20.000'],
        ['2011-281T18:04:20',            '2011-10-08T18:04:20.000'],
        ['2011-281T18:04:20.1',          '2011-10-08T18:04:20.100'],
        ['2011-281T18:04:20.11',         '2011-10-08T18:04:20.110'],
        ['2011-281T18:04:20.111',        '2011-10-08T18:04:20.111']
    ];

    for (i = 0; i < formats.length; i++) {
        mom = moment(formats[i][0] + 'Z').utc();
        assert.equal(mom.format('YYYY-MM-DDTHH:mm:ss.SSS'), formats[i][1], 'moment should be able to parse ISO in UTC ' + formats[i][0] + 'Z');

        mom = moment(formats[i][0] + ' Z').utc();
        assert.equal(mom.format('YYYY-MM-DDTHH:mm:ss.SSS'), formats[i][1], 'moment should be able to parse ISO in UTC ' + formats[i][0] + ' Z');
    }
});

test('parsing iso with T', function (assert) {
    assert.equal(moment('2011-10-08T18')._f, 'YYYY-MM-DDTHH', 'should include \'T\' in the format');
    assert.equal(moment('2011-10-08T18:20')._f, 'YYYY-MM-DDTHH:mm', 'should include \'T\' in the format');
    assert.equal(moment('2011-10-08T18:20:13')._f, 'YYYY-MM-DDTHH:mm:ss', 'should include \'T\' in the format');
    assert.equal(moment('2011-10-08T18:20:13.321')._f, 'YYYY-MM-DDTHH:mm:ss.SSSS', 'should include \'T\' in the format');

    assert.equal(moment('2011-10-08 18')._f, 'YYYY-MM-DD HH', 'should not include \'T\' in the format');
    assert.equal(moment('2011-10-08 18:20')._f, 'YYYY-MM-DD HH:mm', 'should not include \'T\' in the format');
    assert.equal(moment('2011-10-08 18:20:13')._f, 'YYYY-MM-DD HH:mm:ss', 'should not include \'T\' in the format');
    assert.equal(moment('2011-10-08 18:20:13.321')._f, 'YYYY-MM-DD HH:mm:ss.SSSS', 'should not include \'T\' in the format');
});

test('parsing iso Z timezone', function (assert) {
    var i,
    formats = [
        ['2011-10-08T18:04Z',             '2011-10-08T18:04:00.000+00:00'],
        ['2011-10-08T18:04:20Z',          '2011-10-08T18:04:20.000+00:00'],
        ['2011-10-08T18:04:20.111Z',      '2011-10-08T18:04:20.111+00:00']
    ];
    for (i = 0; i < formats.length; i++) {
        assert.equal(moment.utc(formats[i][0]).format('YYYY-MM-DDTHH:mm:ss.SSSZ'), formats[i][1], 'moment should be able to parse ISO ' + formats[i][0]);
    }
});

test('parsing iso Z timezone into local', function (assert) {
    var m = moment('2011-10-08T18:04:20.111Z');

    assert.equal(m.utc().format('YYYY-MM-DDTHH:mm:ss.SSS'), '2011-10-08T18:04:20.111', 'moment should be able to parse ISO 2011-10-08T18:04:20.111Z');
});

test('parsing iso with more subsecond precision digits', function (assert) {
    assert.equal(moment.utc('2013-07-31T22:00:00.0000000Z').format(), '2013-07-31T22:00:00+00:00', 'more than 3 subsecond digits');
});

test('null or empty', function (assert) {
    assert.equal(moment('').isValid(), false, 'moment(\'\') is not valid');
    assert.equal(moment(null).isValid(), false, 'moment(null) is not valid');
    assert.equal(moment(null, 'YYYY-MM-DD').isValid(), false, 'moment(\'\', \'format\') is not valid');
    assert.equal(moment('', 'YYYY-MM-DD').isValid(), false, 'moment(\'\', \'format\') is not valid');
    assert.equal(moment.utc('').isValid(), false, 'moment.utc(\'\') is not valid');
    assert.equal(moment.utc(null).isValid(), false, 'moment.utc(null) is not valid');
    assert.equal(moment.utc(null, 'YYYY-MM-DD').isValid(), false, 'moment.utc(null) is not valid');
    assert.equal(moment.utc('', 'YYYY-MM-DD').isValid(), false, 'moment.utc(\'\', \'YYYY-MM-DD\') is not valid');
});
/* REMOVED: .Net não suporta datas negativas
test('first century', function (assert) {
    assert.equal(moment([0, 0, 1]).format('YYYY-MM-DD'), '0000-01-01', 'Year AD 0');
    assert.equal(moment([99, 0, 1]).format('YYYY-MM-DD'), '0099-01-01', 'Year AD 99');
    assert.equal(moment([999, 0, 1]).format('YYYY-MM-DD'), '0999-01-01', 'Year AD 999');
    assert.equal(moment('0 1 1', 'YYYY MM DD').format('YYYY-MM-DD'), '0000-01-01', 'Year AD 0');
    assert.equal(moment('99 1 1', 'YYYY MM DD').format('YYYY-MM-DD'), '0099-01-01', 'Year AD 99');
    assert.equal(moment('999 1 1', 'YYYY MM DD').format('YYYY-MM-DD'), '0999-01-01', 'Year AD 999');
    assert.equal(moment('0 1 1', 'YYYYY MM DD').format('YYYYY-MM-DD'), '00000-01-01', 'Year AD 0');
    assert.equal(moment('99 1 1', 'YYYYY MM DD').format('YYYYY-MM-DD'), '00099-01-01', 'Year AD 99');
    assert.equal(moment('999 1 1', 'YYYYY MM DD').format('YYYYY-MM-DD'), '00999-01-01', 'Year AD 999');
});
*/
/* REMOVED: .Net não suporta datas negativas
test('six digit years', function (assert) {
    assert.equal(moment([-270000, 0, 1]).format('YYYYY-MM-DD'), '-270000-01-01', 'format BC 270,001');
    assert.equal(moment([270000, 0, 1]).format('YYYYY-MM-DD'), '270000-01-01', 'format AD 270,000');
    assert.equal(moment('-270000-01-01', 'YYYYY-MM-DD').toDate().getFullYear(), -270000, 'parse BC 270,001');
    assert.equal(moment('270000-01-01',  'YYYYY-MM-DD').toDate().getFullYear(), 270000, 'parse AD 270,000');
    assert.equal(moment('+270000-01-01', 'YYYYY-MM-DD').toDate().getFullYear(), 270000, 'parse AD +270,000');
    assert.equal(moment.utc('-270000-01-01', 'YYYYY-MM-DD').toDate().getUTCFullYear(), -270000, 'parse utc BC 270,001');
    assert.equal(moment.utc('270000-01-01',  'YYYYY-MM-DD').toDate().getUTCFullYear(), 270000, 'parse utc AD 270,000');
    assert.equal(moment.utc('+270000-01-01', 'YYYYY-MM-DD').toDate().getUTCFullYear(), 270000, 'parse utc AD +270,000');
});
*/
/* REMOVED: .Net não suporta datas negativas
test('negative four digit years', function (assert) {
    assert.equal(moment('-1000-01-01', 'YYYYY-MM-DD').toDate().getFullYear(), -1000, 'parse BC 1,001');
    assert.equal(moment.utc('-1000-01-01', 'YYYYY-MM-DD').toDate().getUTCFullYear(), -1000, 'parse utc BC 1,001');
});
*/
test('strict parsing', function (assert) {
    assert.equal(moment('2014-', 'YYYY-Q', true).isValid(), false, 'fail missing quarter');

    assert.equal(moment('2012-05', 'YYYY-MM', true).format('YYYY-MM'), '2012-05', 'parse correct string');
    assert.equal(moment(' 2012-05', 'YYYY-MM', true).isValid(), false, 'fail on extra whitespace');
    assert.equal(moment('foo 2012-05', '[foo] YYYY-MM', true).format('YYYY-MM'), '2012-05', 'handle fixed text');
    assert.equal(moment('2012 05', 'YYYY-MM', true).isValid(), false, 'fail on different separator');
    assert.equal(moment('2012 05', 'YYYY MM DD', true).isValid(), false, 'fail on too many tokens');

    assert.equal(moment('05 30 2010', ['DD MM YYYY', 'MM DD YYYY'], true).format('MM DD YYYY'), '05 30 2010', 'array with bad date');
    assert.equal(moment('05 30 2010', ['', 'MM DD YYYY'], true).format('MM DD YYYY'), '05 30 2010', 'array with invalid format');
    assert.equal(moment('05 30 2010', [' DD MM YYYY', 'MM DD YYYY'], true).format('MM DD YYYY'), '05 30 2010', 'array with non-matching format');

    assert.equal(moment('2010.*...', 'YYYY.*', true).isValid(), false, 'invalid format with regex chars');
    assert.equal(moment('2010.*', 'YYYY.*', true).year(), 2010, 'valid format with regex chars');
    assert.equal(moment('.*2010.*', '.*YYYY.*', true).year(), 2010, 'valid format with regex chars on both sides');

    //strict tokens
    //assert.equal(moment('-5-05-25', 'YYYY-MM-DD', true).isValid(), false, 'invalid negative year');
    assert.equal(moment('2-05-25', 'YYYY-MM-DD', true).isValid(), false, 'invalid one-digit year');
    assert.equal(moment('20-05-25', 'YYYY-MM-DD', true).isValid(), false, 'invalid two-digit year');
    assert.equal(moment('201-05-25', 'YYYY-MM-DD', true).isValid(), false, 'invalid three-digit year');
    assert.equal(moment('2010-05-25', 'YYYY-MM-DD', true).isValid(), true, 'valid four-digit year');
    assert.equal(moment('22010-05-25', 'YYYY-MM-DD', true).isValid(), false, 'invalid five-digit year');

    assert.equal(moment('12-05-25', 'YY-MM-DD', true).isValid(), true, 'valid two-digit year');
    assert.equal(moment('2012-05-25', 'YY-MM-DD', true).isValid(), false, 'invalid four-digit year');

    //assert.equal(moment('-5-05-25', 'Y-MM-DD', true).isValid(), true, 'valid negative year');
    assert.equal(moment('2-05-25', 'Y-MM-DD', true).isValid(), true, 'valid one-digit year');
    assert.equal(moment('20-05-25', 'Y-MM-DD', true).isValid(), true, 'valid two-digit year');
    assert.equal(moment('201-05-25', 'Y-MM-DD', true).isValid(), true, 'valid three-digit year');

    assert.equal(moment('2012-5-25', 'YYYY-M-DD', true).isValid(), true, 'valid one-digit month');
    assert.equal(moment('2012-5-25', 'YYYY-MM-DD', true).isValid(), false, 'invalid one-digit month');
    assert.equal(moment('2012-05-25', 'YYYY-M-DD', true).isValid(), true, 'valid one-digit month');
    assert.equal(moment('2012-05-25', 'YYYY-MM-DD', true).isValid(), true, 'valid one-digit month');

    assert.equal(moment('2012-05-2', 'YYYY-MM-D', true).isValid(), true, 'valid one-digit day');
    assert.equal(moment('2012-05-2', 'YYYY-MM-DD', true).isValid(), false, 'invalid one-digit day');
    assert.equal(moment('2012-05-02', 'YYYY-MM-D', true).isValid(), true, 'valid two-digit day');
    assert.equal(moment('2012-05-02', 'YYYY-MM-DD', true).isValid(), true, 'valid two-digit day');

    assert.equal(moment('+002012-05-25', 'YYYYY-MM-DD', true).isValid(), true, 'valid six-digit year');
    assert.equal(moment('+2012-05-25', 'YYYYY-MM-DD', true).isValid(), false, 'invalid four-digit year');

    //thse are kinda pointless, but they should work as expected
    assert.equal(moment('1', 'S', true).isValid(), true, 'valid one-digit milisecond');
    assert.equal(moment('12', 'S', true).isValid(), false, 'invalid two-digit milisecond');
    assert.equal(moment('123', 'S', true).isValid(), false, 'invalid three-digit milisecond');

    assert.equal(moment('1', 'SS', true).isValid(), false, 'invalid one-digit milisecond');
    assert.equal(moment('12', 'SS', true).isValid(), true, 'valid two-digit milisecond');
    assert.equal(moment('123', 'SS', true).isValid(), false, 'invalid three-digit milisecond');

    assert.equal(moment('1', 'SSS', true).isValid(), false, 'invalid one-digit milisecond');
    assert.equal(moment('12', 'SSS', true).isValid(), false, 'invalid two-digit milisecond');
    assert.equal(moment('123', 'SSS', true).isValid(), true, 'valid three-digit milisecond');

    // strict parsing respects month length
    assert.ok(moment('1 January 2000', 'D MMMM YYYY', true).isValid(), 'capital long-month + MMMM');
    assert.ok(!moment('1 January 2000', 'D MMM YYYY', true).isValid(), 'capital long-month + MMM');
    assert.ok(!moment('1 Jan 2000', 'D MMMM YYYY', true).isValid(), 'capital short-month + MMMM');
    assert.ok(moment('1 Jan 2000', 'D MMM YYYY', true).isValid(), 'capital short-month + MMM');
    assert.ok(moment('1 january 2000', 'D MMMM YYYY', true).isValid(), 'lower long-month + MMMM');
    assert.ok(!moment('1 january 2000', 'D MMM YYYY', true).isValid(), 'lower long-month + MMM');
    assert.ok(!moment('1 jan 2000', 'D MMMM YYYY', true).isValid(), 'lower short-month + MMMM');
    assert.ok(moment('1 jan 2000', 'D MMM YYYY', true).isValid(), 'lower short-month + MMM');
});

test('parsing into a locale', function (assert) {
    moment.defineLocale('parselocale', {
        months : 'one_two_three_four_five_six_seven_eight_nine_ten_eleven_twelve'.split('_'),
        monthsShort : 'one_two_three_four_five_six_seven_eight_nine_ten_eleven_twelve'.split('_')
    });

    moment.locale('en');

    assert.equal(moment('2012 seven', 'YYYY MMM', 'parselocale').month(), 6, 'should be able to parse in a specific locale');

    moment.locale('parselocale');

    assert.equal(moment('2012 july', 'YYYY MMM', 'en').month(), 6, 'should be able to parse in a specific locale');

    moment.defineLocale('parselocale', null);
});

function getVerifier(test) {
    return function (input, format, expected, description, asymetrical) {
        var m = moment(input, format);
        test.equal(m.format('YYYY MM DD'), expected, 'compare: ' + description);

        //test round trip
        if (!asymetrical) {
            test.equal(m.format(format), input, 'round trip: ' + description);
        }
    };
}

test('parsing week and weekday information', function (assert) {
    var ver = getVerifier(assert);

    // year
    ver('12', 'gg', '2012 01 01', 'week-year two digits');
    ver('2012', 'gggg', '2012 01 01', 'week-year four digits');

    ver('99', 'gg', '1998 12 27', 'week-year two digits previous year');
    ver('1999', 'gggg', '1998 12 27', 'week-year four digits previous year');

    ver('99', 'GG', '1999 01 04', 'iso week-year two digits');
    ver('1999', 'GGGG', '1999 01 04', 'iso week-year four digits');

    ver('13', 'GG', '2012 12 31', 'iso week-year two digits previous year');
    ver('2013', 'GGGG', '2012 12 31', 'iso week-year four digits previous year');

    // year + week
    ver('1999 37', 'gggg w', '1999 09 05', 'week');
    ver('1999 37', 'gggg ww', '1999 09 05', 'week double');
    ver('1999 37', 'GGGG W', '1999 09 13', 'iso week');
    ver('1999 37', 'GGGG WW', '1999 09 13', 'iso week double');

    ver('1999 37 4', 'GGGG WW E', '1999 09 16', 'iso day');
    ver('1999 37 04', 'GGGG WW E', '1999 09 16', 'iso day wide', true);

    ver('1999 37 4', 'gggg ww e', '1999 09 09', 'day');
    ver('1999 37 04', 'gggg ww e', '1999 09 09', 'day wide', true);

    // year + week + day
    ver('1999 37 4', 'gggg ww d', '1999 09 09', 'd');
    ver('1999 37 Th', 'gggg ww dd', '1999 09 09', 'dd');
    ver('1999 37 Thu', 'gggg ww ddd', '1999 09 09', 'ddd');
    ver('1999 37 Thursday', 'gggg ww dddd', '1999 09 09', 'dddd');

    // lower-order only
    assert.equal(moment('22', 'ww').week(), 22, 'week sets the week by itself');
    assert.equal(moment('22', 'ww').weekYear(), moment().weekYear(), 'week keeps this year');
    assert.equal(moment('2012 22', 'YYYY ww').weekYear(), 2012, 'week keeps parsed year');

    assert.equal(moment('22', 'WW').isoWeek(), 22, 'iso week sets the week by itself');
    assert.equal(moment('2012 22', 'YYYY WW').weekYear(), 2012, 'iso week keeps parsed year');
    assert.equal(moment('22', 'WW').isoWeekYear(), moment().isoWeekYear(), 'iso week keeps this year');

    // order
    ver('6 2013 2', 'e gggg w', '2013 01 12', 'order doesn\'t matter');
    ver('6 2013 2', 'E GGGG W', '2013 01 12', 'iso order doesn\'t matter');

    //can parse other stuff too
    assert.equal(moment('1999-W37-4 3:30', 'GGGG-[W]WW-E HH:mm').format('YYYY MM DD HH:mm'), '1999 09 16 03:30', 'parsing weeks and hours');

    // In safari, all years before 1300 are shifted back with one day.
    // http://stackoverflow.com/questions/20768975/safari-subtracts-1-day-from-dates-before-1300
/*	.Net não suporta formato de Data com ano pequeno (05 = 2005)
    if (new Date('1300-01-01').getUTCFullYear() === 1300) {
        // Years less than 100
        ver('0098-06', 'GGGG-WW', '0098 02 03', 'small years work', true);
    }
*/	
});

test('parsing localized weekdays', function (assert) {
    var ver = getVerifier(assert);
    try {
        moment.locale('dow:1,doy:4', {
            weekdays : 'dimanche_lundi_mardi_mercredi_jeudi_vendredi_samedi'.split('_'),
            weekdaysShort : 'dim._lun._mar._mer._jeu._ven._sam.'.split('_'),
            weekdaysMin : 'Di_Lu_Ma_Me_Je_Ve_Sa'.split('_'),
            week: {dow: 1, doy: 4}
        });
        ver('1999 37 4', 'GGGG WW E', '1999 09 16', 'iso ignores locale');
        ver('1999 37 7', 'GGGG WW E', '1999 09 19', 'iso ignores locale');

        ver('1999 37 0', 'gggg ww e', '1999 09 13', 'localized e uses local doy and dow: 0 = monday');
        ver('1999 37 4', 'gggg ww e', '1999 09 17', 'localized e uses local doy and dow: 4 = friday');

        ver('1999 37 1', 'gggg ww d', '1999 09 13', 'localized d uses 0-indexed days: 1 = monday');
        ver('1999 37 Lu', 'gggg ww dd', '1999 09 13', 'localized d uses 0-indexed days: Mo');
        ver('1999 37 lun.', 'gggg ww ddd', '1999 09 13', 'localized d uses 0-indexed days: Mon');
        ver('1999 37 lundi', 'gggg ww dddd', '1999 09 13', 'localized d uses 0-indexed days: Monday');
        ver('1999 37 4', 'gggg ww d', '1999 09 16', 'localized d uses 0-indexed days: 4');

        //sunday goes at the end of the week
        ver('1999 37 0', 'gggg ww d', '1999 09 19', 'localized d uses 0-indexed days: 0 = sund');
        ver('1999 37 Di', 'gggg ww dd', '1999 09 19', 'localized d uses 0-indexed days: 0 = sund');
    }
    finally {
        moment.locale('en');
    }
});

test('parsing with customized two-digit year', function (assert) {
    var original = moment.parseTwoDigitYear;
    try {
        assert.equal(moment('68', 'YY').year(), 2068);
        assert.equal(moment('69', 'YY').year(), 1969);
        moment.parseTwoDigitYear = function (input) {
            return +input + (+input > 30 ? 1900 : 2000);
        };
        assert.equal(moment('68', 'YY').year(), 1968);
        assert.equal(moment('67', 'YY').year(), 1967);
        assert.equal(moment('31', 'YY').year(), 1931);
        assert.equal(moment('30', 'YY').year(), 2030);
    }
    finally {
        moment.parseTwoDigitYear = original;
    }
});

test('array with strings', function (assert) {
    assert.equal(moment(['2014', '7', '31']).isValid(), true, 'string array + isValid');
});

test('utc with array of formats', function (assert) {
    assert.equal(moment.utc('2014-01-01', ['YYYY-MM-DD', 'YYYY-MM']).format(), '2014-01-01T00:00:00+00:00', 'moment.utc works with array of formats');
});
//===================================================================================================================================================================================================================== //>
module('days in month');
test('days in month', function (assert) {
    each([31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31], function (days, i) {
        var firstDay = moment([2012, i]),
            lastDay  = moment([2012, i, days]);
        assert.equal(firstDay.daysInMonth(), days, firstDay.format('L') + ' should have ' + days + ' days.');
        assert.equal(lastDay.daysInMonth(), days, lastDay.format('L') + ' should have ' + days + ' days.');
    });
});

test('days in month leap years', function (assert) {
    assert.equal(moment([2010, 1]).daysInMonth(), 28, 'Feb 2010 should have 28 days');
    assert.equal(moment([2100, 1]).daysInMonth(), 28, 'Feb 2100 should have 28 days');
    assert.equal(moment([2008, 1]).daysInMonth(), 29, 'Feb 2008 should have 29 days');
    assert.equal(moment([2000, 1]).daysInMonth(), 29, 'Feb 2000 should have 29 days');
});
//===================================================================================================================================================================================================================== //>
function equal(assert, a, b, message) {
    assert.ok(Math.abs(a - b) < 0.00000001, '(' + a + ' === ' + b + ') ' + message);
}

function dstForYear(year) {
    var start = moment([year]),
        end = moment([year + 1]),
        current = start.clone(),
        last;

    while (current < end) {
        last = current.clone();
        current.add(24, 'hour');
        if (last.utcOffset() !== current.utcOffset()) {
            end = current.clone();
            current = last.clone();
            break;
        }
    }

    while (current < end) {
        last = current.clone();
        current.add(1, 'hour');
        if (last.utcOffset() !== current.utcOffset()) {
            return {
                moment : last,
                diff : -(current.utcOffset() - last.utcOffset()) / 60
            };
        }
    }
}

module('diff');

test('diff', function (assert) {
    assert.equal(moment(1000).diff(0), 1000, '1 second - 0 = 1000');
    assert.equal(moment(1000).diff(500), 500, '1 second - 0.5 seconds = 500');
    assert.equal(moment(0).diff(1000), -1000, '0 - 1 second = -1000');
    assert.equal(moment(new Date(1000)).diff(1000), 0, '1 second - 1 second = 0');
    var oneHourDate = new Date(),
    nowDate = new Date(+oneHourDate);
    oneHourDate.setHours(oneHourDate.getHours() + 1);
    assert.equal(moment(oneHourDate).diff(nowDate), 60 * 60 * 1000, '1 hour from now = 3600000');
});

test('diff key after', function (assert) {
    assert.equal(moment([2010]).diff([2011], 'years'), -1, 'year diff');
    assert.equal(moment([2010]).diff([2010, 2], 'months'), -2, 'month diff');
    assert.equal(moment([2010]).diff([2010, 0, 7], 'weeks'), 0, 'week diff');
    assert.equal(moment([2010]).diff([2010, 0, 8], 'weeks'), -1, 'week diff');
    assert.equal(moment([2010]).diff([2010, 0, 21], 'weeks'), -2, 'week diff');
    assert.equal(moment([2010]).diff([2010, 0, 22], 'weeks'), -3, 'week diff');
    assert.equal(moment([2010]).diff([2010, 0, 4], 'days'), -3, 'day diff');
    assert.equal(moment([2010]).diff([2010, 0, 1, 4], 'hours'), -4, 'hour diff');
    assert.equal(moment([2010]).diff([2010, 0, 1, 0, 5], 'minutes'), -5, 'minute diff');
    assert.equal(moment([2010]).diff([2010, 0, 1, 0, 0, 6], 'seconds'), -6, 'second diff');
});

test('diff key before', function (assert) {
    assert.equal(moment([2011]).diff([2010], 'years'), 1, 'year diff');
    assert.equal(moment([2010, 2]).diff([2010], 'months'), 2, 'month diff');
    assert.equal(moment([2010, 0, 4]).diff([2010], 'days'), 3, 'day diff');
    assert.equal(moment([2010, 0, 7]).diff([2010], 'weeks'), 0, 'week diff');
    assert.equal(moment([2010, 0, 8]).diff([2010], 'weeks'), 1, 'week diff');
    assert.equal(moment([2010, 0, 21]).diff([2010], 'weeks'), 2, 'week diff');
    assert.equal(moment([2010, 0, 22]).diff([2010], 'weeks'), 3, 'week diff');
    assert.equal(moment([2010, 0, 1, 4]).diff([2010], 'hours'), 4, 'hour diff');
    assert.equal(moment([2010, 0, 1, 0, 5]).diff([2010], 'minutes'), 5, 'minute diff');
    assert.equal(moment([2010, 0, 1, 0, 0, 6]).diff([2010], 'seconds'), 6, 'second diff');
});

test('diff key before singular', function (assert) {
    assert.equal(moment([2011]).diff([2010], 'year'), 1, 'year diff singular');
    assert.equal(moment([2010, 2]).diff([2010], 'month'), 2, 'month diff singular');
    assert.equal(moment([2010, 0, 4]).diff([2010], 'day'), 3, 'day diff singular');
    assert.equal(moment([2010, 0, 7]).diff([2010], 'week'), 0, 'week diff singular');
    assert.equal(moment([2010, 0, 8]).diff([2010], 'week'), 1, 'week diff singular');
    assert.equal(moment([2010, 0, 21]).diff([2010], 'week'), 2, 'week diff singular');
    assert.equal(moment([2010, 0, 22]).diff([2010], 'week'), 3, 'week diff singular');
    assert.equal(moment([2010, 0, 1, 4]).diff([2010], 'hour'), 4, 'hour diff singular');
    assert.equal(moment([2010, 0, 1, 0, 5]).diff([2010], 'minute'), 5, 'minute diff singular');
    assert.equal(moment([2010, 0, 1, 0, 0, 6]).diff([2010], 'second'), 6, 'second diff singular');
});

test('diff key before abbreviated', function (assert) {
    assert.equal(moment([2011]).diff([2010], 'y'), 1, 'year diff abbreviated');
    assert.equal(moment([2010, 2]).diff([2010], 'M'), 2, 'month diff abbreviated');
    assert.equal(moment([2010, 0, 4]).diff([2010], 'd'), 3, 'day diff abbreviated');
    assert.equal(moment([2010, 0, 7]).diff([2010], 'w'), 0, 'week diff abbreviated');
    assert.equal(moment([2010, 0, 8]).diff([2010], 'w'), 1, 'week diff abbreviated');
    assert.equal(moment([2010, 0, 21]).diff([2010], 'w'), 2, 'week diff abbreviated');
    assert.equal(moment([2010, 0, 22]).diff([2010], 'w'), 3, 'week diff abbreviated');
    assert.equal(moment([2010, 0, 1, 4]).diff([2010], 'h'), 4, 'hour diff abbreviated');
    assert.equal(moment([2010, 0, 1, 0, 5]).diff([2010], 'm'), 5, 'minute diff abbreviated');
    assert.equal(moment([2010, 0, 1, 0, 0, 6]).diff([2010], 's'), 6, 'second diff abbreviated');
});

test('diff month', function (assert) {
    assert.equal(moment([2011, 0, 31]).diff([2011, 2, 1], 'months'), -1, 'month diff');
});

test('diff across DST', function (assert) {
    var dst = dstForYear(2012), a, b, daysInMonth;
    if (!dst) {
        assert.equal(42, 42, 'at least one assertion');
        return;
    }

    a = dst.moment;
    b = a.clone().utc().add(12, 'hours').local();
    daysInMonth = (a.daysInMonth() + b.daysInMonth()) / 2;
    assert.equal(b.diff(a, 'milliseconds', true), 12 * 60 * 60 * 1000,
            'ms diff across DST');
    assert.equal(b.diff(a, 'seconds', true), 12 * 60 * 60,
            'second diff across DST');
    assert.equal(b.diff(a, 'minutes', true), 12 * 60,
            'minute diff across DST');
    assert.equal(b.diff(a, 'hours', true), 12,
            'hour diff across DST');
    assert.equal(b.diff(a, 'days', true), (12 - dst.diff) / 24,
            'day diff across DST');
    equal(assert, b.diff(a, 'weeks', true),  (12 - dst.diff) / 24 / 7,
            'week diff across DST');
    assert.ok(0.95 / (2 * 31) < b.diff(a, 'months', true),
            'month diff across DST, lower bound');
    assert.ok(b.diff(a, 'month', true) < 1.05 / (2 * 28),
            'month diff across DST, upper bound');
    assert.ok(0.95 / (2 * 31 * 12) < b.diff(a, 'years', true),
            'year diff across DST, lower bound');
    assert.ok(b.diff(a, 'year', true) < 1.05 / (2 * 28 * 12),
            'year diff across DST, upper bound');

    a = dst.moment;
    b = a.clone().utc().add(12 + dst.diff, 'hours').local();
    daysInMonth = (a.daysInMonth() + b.daysInMonth()) / 2;

    assert.equal(b.diff(a, 'milliseconds', true),
            (12 + dst.diff) * 60 * 60 * 1000,
            'ms diff across DST');
    assert.equal(b.diff(a, 'seconds', true),  (12 + dst.diff) * 60 * 60,
            'second diff across DST');
    assert.equal(b.diff(a, 'minutes', true),  (12 + dst.diff) * 60,
            'minute diff across DST');
    assert.equal(b.diff(a, 'hours', true),  (12 + dst.diff),
            'hour diff across DST');
    assert.equal(b.diff(a, 'days', true),  12 / 24, 'day diff across DST');
    equal(assert, b.diff(a, 'weeks', true),  12 / 24 / 7,
            'week diff across DST');
    assert.ok(0.95 / (2 * 31) < b.diff(a, 'months', true),
            'month diff across DST, lower bound');
    assert.ok(b.diff(a, 'month', true) < 1.05 / (2 * 28),
            'month diff across DST, upper bound');
    assert.ok(0.95 / (2 * 31 * 12) < b.diff(a, 'years', true),
            'year diff across DST, lower bound');
    assert.ok(b.diff(a, 'year', true) < 1.05 / (2 * 28 * 12),
            'year diff across DST, upper bound');
});

test('diff overflow', function (assert) {
    assert.equal(moment([2011]).diff([2010], 'months'), 12, 'month diff');
    assert.equal(moment([2010, 0, 2]).diff([2010], 'hours'), 24, 'hour diff');
    assert.equal(moment([2010, 0, 1, 2]).diff([2010], 'minutes'), 120, 'minute diff');
    assert.equal(moment([2010, 0, 1, 0, 4]).diff([2010], 'seconds'), 240, 'second diff');
});

test('diff between utc and local', function (assert) {
    if (moment([2012]).utcOffset() === moment([2011]).utcOffset()) {
        // Russia's utc offset on 1st of Jan 2012 vs 2011 is different
        assert.equal(moment([2012]).utc().diff([2011], 'years'), 1, 'year diff');
    }
    assert.equal(moment([2010, 2, 2]).utc().diff([2010, 0, 2], 'months'), 2, 'month diff');
    assert.equal(moment([2010, 0, 4]).utc().diff([2010], 'days'), 3, 'day diff');
    assert.equal(moment([2010, 0, 22]).utc().diff([2010], 'weeks'), 3, 'week diff');
    assert.equal(moment([2010, 0, 1, 4]).utc().diff([2010], 'hours'), 4, 'hour diff');
    assert.equal(moment([2010, 0, 1, 0, 5]).utc().diff([2010], 'minutes'), 5, 'minute diff');
    assert.equal(moment([2010, 0, 1, 0, 0, 6]).utc().diff([2010], 'seconds'), 6, 'second diff');
});

test('diff floored', function (assert) {
    assert.equal(moment([2010, 0, 1, 23]).diff([2010], 'day'), 0, '23 hours = 0 days');
    assert.equal(moment([2010, 0, 1, 23, 59]).diff([2010], 'day'), 0, '23:59 hours = 0 days');
    assert.equal(moment([2010, 0, 1, 24]).diff([2010], 'day'), 1, '24 hours = 1 day');
    assert.equal(moment([2010, 0, 2]).diff([2011, 0, 1], 'year'), 0, 'year rounded down');
    assert.equal(moment([2011, 0, 1]).diff([2010, 0, 2], 'year'), 0, 'year rounded down');
    assert.equal(moment([2010, 0, 2]).diff([2011, 0, 2], 'year'), -1, 'year rounded down');
    assert.equal(moment([2011, 0, 2]).diff([2010, 0, 2], 'year'), 1, 'year rounded down');
});

test('year diffs include dates', function (assert) {
    assert.ok(moment([2012, 1, 19]).diff(moment([2002, 1, 20]), 'years', true) < 10, 'year diff should include date of month');
});

test('month diffs', function (assert) {
    // due to floating point math errors, these tests just need to be accurate within 0.00000001
    assert.equal(moment([2012, 0, 1]).diff([2012, 1, 1], 'months', true), -1, 'Jan 1 to Feb 1 should be 1 month');
    equal(assert, moment([2012, 0, 1]).diff([2012, 0, 1, 12], 'months', true), -0.5 / 31, 'Jan 1 to Jan 1 noon should be 0.5 / 31 months');
    assert.equal(moment([2012, 0, 15]).diff([2012, 1, 15], 'months', true), -1, 'Jan 15 to Feb 15 should be 1 month');
    assert.equal(moment([2012, 0, 28]).diff([2012, 1, 28], 'months', true), -1, 'Jan 28 to Feb 28 should be 1 month');
    assert.ok(moment([2012, 0, 31]).diff([2012, 1, 29], 'months', true), -1, 'Jan 31 to Feb 29 should be 1 month');
    assert.ok(-1 > moment([2012, 0, 31]).diff([2012, 2, 1], 'months', true), 'Jan 31 to Mar 1 should be more than 1 month');
    assert.ok(-30 / 28 < moment([2012, 0, 31]).diff([2012, 2, 1], 'months', true), 'Jan 31 to Mar 1 should be less than 1 month and 1 day');
    equal(assert, moment([2012, 0, 1]).diff([2012, 0, 31], 'months', true), -(30 / 31), 'Jan 1 to Jan 31 should be 30 / 31 months');
    assert.ok(0 < moment('2014-02-01').diff(moment('2014-01-31'), 'months', true), 'jan-31 to feb-1 diff is positive');
});

test('exact month diffs', function (assert) {
    // generate all pairs of months and compute month diff, with fixed day
    // of month = 15.

    var m1, m2;
    for (m1 = 0; m1 < 12; ++m1) {
        for (m2 = m1; m2 < 12; ++m2) {
            assert.equal(moment([2013, m2, 15]).diff(moment([2013, m1, 15]), 'months', true), m2 - m1,
                         'month diff from 2013-' + m1 + '-15 to 2013-' + m2 + '-15');
        }
    }
});

test('year diffs', function (assert) {
    // due to floating point math errors, these tests just need to be accurate within 0.00000001
    equal(assert, moment([2012, 0, 1]).diff([2013, 0, 1], 'years', true), -1, 'Jan 1 2012 to Jan 1 2013 should be 1 year');
    equal(assert, moment([2012, 1, 28]).diff([2013, 1, 28], 'years', true), -1, 'Feb 28 2012 to Feb 28 2013 should be 1 year');
    equal(assert, moment([2012, 2, 1]).diff([2013, 2, 1], 'years', true), -1, 'Mar 1 2012 to Mar 1 2013 should be 1 year');
    equal(assert, moment([2012, 11, 1]).diff([2013, 11, 1], 'years', true), -1, 'Dec 1 2012 to Dec 1 2013 should be 1 year');
    equal(assert, moment([2012, 11, 31]).diff([2013, 11, 31], 'years', true), -1, 'Dec 31 2012 to Dec 31 2013 should be 1 year');
    equal(assert, moment([2012, 0, 1]).diff([2013, 6, 1], 'years', true), -1.5, 'Jan 1 2012 to Jul 1 2013 should be 1.5 years');
    equal(assert, moment([2012, 0, 31]).diff([2013, 6, 31], 'years', true), -1.5, 'Jan 31 2012 to Jul 31 2013 should be 1.5 years');
    equal(assert, moment([2012, 0, 1]).diff([2013, 0, 1, 12], 'years', true), -1 - (0.5 / 31) / 12, 'Jan 1 2012 to Jan 1 2013 noon should be 1+(0.5 / 31) / 12 years');
    equal(assert, moment([2012, 0, 1]).diff([2013, 6, 1, 12], 'years', true), -1.5 - (0.5 / 31) / 12, 'Jan 1 2012 to Jul 1 2013 noon should be 1.5+(0.5 / 31) / 12 years');
    equal(assert, moment([2012, 1, 29]).diff([2013, 1, 28], 'years', true), -1, 'Feb 29 2012 to Feb 28 2013 should be 1-(1 / 28.5) / 12 years');
});
//===================================================================================================================================================================================================================== //>
module('duration');

test('object instantiation', function (assert) {
    var d = moment.duration({
        years: 2,
        months: 3,
        weeks: 2,
        days: 1,
        hours: 8,
        minutes: 9,
        seconds: 20,
        milliseconds: 12
    });

    assert.equal(d.years(),        2,  'years');
    assert.equal(d.months(),       3,  'months');
    assert.equal(d.weeks(),        2,  'weeks');
    assert.equal(d.days(),         15, 'days'); // two weeks + 1 day
    assert.equal(d.hours(),        8,  'hours');
    assert.equal(d.minutes(),      9,  'minutes');
    assert.equal(d.seconds(),      20, 'seconds');
    assert.equal(d.milliseconds(), 12, 'milliseconds');
});

test('object instantiation with strings', function (assert) {
    var d = moment.duration({
        years: '2',
        months: '3',
        weeks: '2',
        days: '1',
        hours: '8',
        minutes: '9',
        seconds: '20',
        milliseconds: '12'
    });

    assert.equal(d.years(),        2,  'years');
    assert.equal(d.months(),       3,  'months');
    assert.equal(d.weeks(),        2,  'weeks');
    assert.equal(d.days(),         15, 'days'); // two weeks + 1 day
    assert.equal(d.hours(),        8,  'hours');
    assert.equal(d.minutes(),      9,  'minutes');
    assert.equal(d.seconds(),      20, 'seconds');
    assert.equal(d.milliseconds(), 12, 'milliseconds');
});

test('milliseconds instantiation', function (assert) {
    assert.equal(moment.duration(72).milliseconds(), 72, 'milliseconds');
});

test('undefined instantiation', function (assert) {
    assert.equal(moment.duration(undefined).milliseconds(), 0, 'milliseconds');
});

test('null instantiation', function (assert) {
    assert.equal(moment.duration(null).milliseconds(), 0, 'milliseconds');
});

test('instantiation by type', function (assert) {
    assert.equal(moment.duration(1, 'years').years(),                 1, 'years');
    assert.equal(moment.duration(1, 'y').years(),                     1, 'y');
    assert.equal(moment.duration(2, 'months').months(),               2, 'months');
    assert.equal(moment.duration(2, 'M').months(),                    2, 'M');
    assert.equal(moment.duration(3, 'weeks').weeks(),                 3, 'weeks');
    assert.equal(moment.duration(3, 'w').weeks(),                     3, 'weeks');
    assert.equal(moment.duration(4, 'days').days(),                   4, 'days');
    assert.equal(moment.duration(4, 'd').days(),                      4, 'd');
    assert.equal(moment.duration(5, 'hours').hours(),                 5, 'hours');
    assert.equal(moment.duration(5, 'h').hours(),                     5, 'h');
    assert.equal(moment.duration(6, 'minutes').minutes(),             6, 'minutes');
    assert.equal(moment.duration(6, 'm').minutes(),                   6, 'm');
    assert.equal(moment.duration(7, 'seconds').seconds(),             7, 'seconds');
    assert.equal(moment.duration(7, 's').seconds(),                   7, 's');
    assert.equal(moment.duration(8, 'milliseconds').milliseconds(),   8, 'milliseconds');
    assert.equal(moment.duration(8, 'ms').milliseconds(),             8, 'ms');
});

test('shortcuts', function (assert) {
    assert.equal(moment.duration({y: 1}).years(),         1, 'years = y');
    assert.equal(moment.duration({M: 2}).months(),        2, 'months = M');
    assert.equal(moment.duration({w: 3}).weeks(),         3, 'weeks = w');
    assert.equal(moment.duration({d: 4}).days(),          4, 'days = d');
    assert.equal(moment.duration({h: 5}).hours(),         5, 'hours = h');
    assert.equal(moment.duration({m: 6}).minutes(),       6, 'minutes = m');
    assert.equal(moment.duration({s: 7}).seconds(),       7, 'seconds = s');
    assert.equal(moment.duration({ms: 8}).milliseconds(), 8, 'milliseconds = ms');
});

test('generic getter', function (assert) {
    assert.equal(moment.duration(1, 'years').get('years'),                1, 'years');
    assert.equal(moment.duration(1, 'years').get('year'),                 1, 'years = year');
    assert.equal(moment.duration(1, 'years').get('y'),                    1, 'years = y');
    assert.equal(moment.duration(2, 'months').get('months'),              2, 'months');
    assert.equal(moment.duration(2, 'months').get('month'),               2, 'months = month');
    assert.equal(moment.duration(2, 'months').get('M'),                   2, 'months = M');
    assert.equal(moment.duration(3, 'weeks').get('weeks'),                3, 'weeks');
    assert.equal(moment.duration(3, 'weeks').get('week'),                 3, 'weeks = week');
    assert.equal(moment.duration(3, 'weeks').get('w'),                    3, 'weeks = w');
    assert.equal(moment.duration(4, 'days').get('days'),                  4, 'days');
    assert.equal(moment.duration(4, 'days').get('day'),                   4, 'days = day');
    assert.equal(moment.duration(4, 'days').get('d'),                     4, 'days = d');
    assert.equal(moment.duration(5, 'hours').get('hours'),                5, 'hours');
    assert.equal(moment.duration(5, 'hours').get('hour'),                 5, 'hours = hour');
    assert.equal(moment.duration(5, 'hours').get('h'),                    5, 'hours = h');
    assert.equal(moment.duration(6, 'minutes').get('minutes'),            6, 'minutes');
    assert.equal(moment.duration(6, 'minutes').get('minute'),             6, 'minutes = minute');
    assert.equal(moment.duration(6, 'minutes').get('m'),                  6, 'minutes = m');
    assert.equal(moment.duration(7, 'seconds').get('seconds'),            7, 'seconds');
    assert.equal(moment.duration(7, 'seconds').get('second'),             7, 'seconds = second');
    assert.equal(moment.duration(7, 'seconds').get('s'),                  7, 'seconds = s');
    assert.equal(moment.duration(8, 'milliseconds').get('milliseconds'),  8, 'milliseconds');
    assert.equal(moment.duration(8, 'milliseconds').get('millisecond'),   8, 'milliseconds = millisecond');
    assert.equal(moment.duration(8, 'milliseconds').get('ms'),            8, 'milliseconds = ms');
});

test('instantiation from another duration', function (assert) {
    var simple = moment.duration(1234),
        lengthy = moment.duration(60 * 60 * 24 * 360 * 1e3),
        complicated = moment.duration({
            years: 2,
            months: 3,
            weeks: 4,
            days: 1,
            hours: 8,
            minutes: 9,
            seconds: 20,
            milliseconds: 12
        }),
        modified = moment.duration(1, 'day').add(moment.duration(1, 'day'));

    assert.deepEqual(moment.duration(simple), simple, 'simple clones are equal');
    assert.deepEqual(moment.duration(lengthy), lengthy, 'lengthy clones are equal');
    assert.deepEqual(moment.duration(complicated), complicated, 'complicated clones are equal');
    assert.deepEqual(moment.duration(modified), modified, 'cloning modified duration works');
});

test('instantiation from 24-hour time zero', function (assert) {
    assert.equal(moment.duration('00:00').years(), 0, '0 years');
    assert.equal(moment.duration('00:00').days(), 0, '0 days');
    assert.equal(moment.duration('00:00').hours(), 0, '0 hours');
    assert.equal(moment.duration('00:00').minutes(), 0, '0 minutes');
    assert.equal(moment.duration('00:00').seconds(), 0, '0 seconds');
    assert.equal(moment.duration('00:00').milliseconds(), 0, '0 milliseconds');
});

test('instantiation from 24-hour time <24 hours', function (assert) {
    assert.equal(moment.duration('06:45').years(), 0, '0 years');
    assert.equal(moment.duration('06:45').days(), 0, '0 days');
    assert.equal(moment.duration('06:45').hours(), 6, '6 hours');
    assert.equal(moment.duration('06:45').minutes(), 45, '45 minutes');
    assert.equal(moment.duration('06:45').seconds(), 0, '0 seconds');
    assert.equal(moment.duration('06:45').milliseconds(), 0, '0 milliseconds');
});

test('instantiation from 24-hour time >24 hours', function (assert) {
    assert.equal(moment.duration('26:45').years(), 0, '0 years');
    assert.equal(moment.duration('26:45').days(), 1, '0 days');
    assert.equal(moment.duration('26:45').hours(), 2, '2 hours');
    assert.equal(moment.duration('26:45').minutes(), 45, '45 minutes');
    assert.equal(moment.duration('26:45').seconds(), 0, '0 seconds');
    assert.equal(moment.duration('26:45').milliseconds(), 0, '0 milliseconds');
});

test('instatiation from serialized C# TimeSpan zero', function (assert) {
    assert.equal(moment.duration('00:00:00').years(), 0, '0 years');
    assert.equal(moment.duration('00:00:00').days(), 0, '0 days');
    assert.equal(moment.duration('00:00:00').hours(), 0, '0 hours');
    assert.equal(moment.duration('00:00:00').minutes(), 0, '0 minutes');
    assert.equal(moment.duration('00:00:00').seconds(), 0, '0 seconds');
    assert.equal(moment.duration('00:00:00').milliseconds(), 0, '0 milliseconds');
});

test('instatiation from serialized C# TimeSpan with days', function (assert) {
    assert.equal(moment.duration('1.02:03:04.9999999').years(), 0, '0 years');
    assert.equal(moment.duration('1.02:03:04.9999999').days(), 1, '1 day');
    assert.equal(moment.duration('1.02:03:04.9999999').hours(), 2, '2 hours');
    assert.equal(moment.duration('1.02:03:04.9999999').minutes(), 3, '3 minutes');
    assert.equal(moment.duration('1.02:03:04.9999999').seconds(), 4, '4 seconds');
    assert.equal(moment.duration('1.02:03:04.9999999').milliseconds(), 999, '999 milliseconds');
});

test('instatiation from serialized C# TimeSpan without days', function (assert) {
    assert.equal(moment.duration('01:02:03.9999999').years(), 0, '0 years');
    assert.equal(moment.duration('01:02:03.9999999').days(), 0, '0 days');
    assert.equal(moment.duration('01:02:03.9999999').hours(), 1, '1 hour');
    assert.equal(moment.duration('01:02:03.9999999').minutes(), 2, '2 minutes');
    assert.equal(moment.duration('01:02:03.9999999').seconds(), 3, '3 seconds');
    assert.equal(moment.duration('01:02:03.9999999').milliseconds(), 999, '999 milliseconds');

    assert.equal(moment.duration('23:59:59.9999999').days(), 0, '0 days');
    assert.equal(moment.duration('23:59:59.9999999').hours(), 23, '23 hours');

    assert.equal(moment.duration('500:59:59.9999999').days(), 20, '500 hours overflows to 20 days');
    assert.equal(moment.duration('500:59:59.9999999').hours(), 20, '500 hours overflows to 20 hours');
});

test('instatiation from serialized C# TimeSpan without days or milliseconds', function (assert) {
    assert.equal(moment.duration('01:02:03').years(), 0, '0 years');
    assert.equal(moment.duration('01:02:03').days(), 0, '0 days');
    assert.equal(moment.duration('01:02:03').hours(), 1, '1 hour');
    assert.equal(moment.duration('01:02:03').minutes(), 2, '2 minutes');
    assert.equal(moment.duration('01:02:03').seconds(), 3, '3 seconds');
    assert.equal(moment.duration('01:02:03').milliseconds(), 0, '0 milliseconds');
});

test('instatiation from serialized C# TimeSpan without milliseconds', function (assert) {
    assert.equal(moment.duration('1.02:03:04').years(), 0, '0 years');
    assert.equal(moment.duration('1.02:03:04').days(), 1, '1 day');
    assert.equal(moment.duration('1.02:03:04').hours(), 2, '2 hours');
    assert.equal(moment.duration('1.02:03:04').minutes(), 3, '3 minutes');
    assert.equal(moment.duration('1.02:03:04').seconds(), 4, '4 seconds');
    assert.equal(moment.duration('1.02:03:04').milliseconds(), 0, '0 milliseconds');
});

test('instatiation from serialized C# TimeSpan maxValue', function (assert) {
    var d = moment.duration('10675199.02:48:05.4775807');

    assert.equal(d.years(), 29227, '29227 years');
    assert.equal(d.months(), 8, '8 months');
    assert.equal(d.days(), 17, '17 day');  // this should be 13

    assert.equal(d.hours(), 2, '2 hours');
    assert.equal(d.minutes(), 48, '48 minutes');
    assert.equal(d.seconds(), 5, '5 seconds');
    assert.equal(d.milliseconds(), 477, '477 milliseconds');
});

test('instatiation from serialized C# TimeSpan minValue', function (assert) {
    var d = moment.duration('-10675199.02:48:05.4775808');

    assert.equal(d.years(), -29227, '29653 years');
    assert.equal(d.months(), -8, '8 day');
    assert.equal(d.days(), -17, '17 day'); // this should be 13

    assert.equal(d.hours(), -2, '2 hours');
    assert.equal(d.minutes(), -48, '48 minutes');
    assert.equal(d.seconds(), -5, '5 seconds');
    assert.equal(d.milliseconds(), -477, '477 milliseconds');
});

test('instantiation from ISO 8601 duration', function (assert) {
    assert.equal(moment.duration('P1Y2M3DT4H5M6S').asSeconds(), moment.duration({y: 1, M: 2, d: 3, h: 4, m: 5, s: 6}).asSeconds(), 'all fields');
    assert.equal(moment.duration('P1M').asSeconds(), moment.duration({M: 1}).asSeconds(), 'single month field');
    assert.equal(moment.duration('PT1M').asSeconds(), moment.duration({m: 1}).asSeconds(), 'single minute field');
    assert.equal(moment.duration('P1MT2H').asSeconds(), moment.duration({M: 1, h: 2}).asSeconds(), 'random fields missing');
    //assert.equal(moment.duration('-P60D').asSeconds(), moment.duration({d: -60}).asSeconds(), 'negative days');
    assert.equal(moment.duration('PT0.5S').asSeconds(), moment.duration({s: 0.5}).asSeconds(), 'fractional seconds');
    assert.equal(moment.duration('PT0,5S').asSeconds(), moment.duration({s: 0.5}).asSeconds(), 'fractional seconds (comma)');
});

test('serialization to ISO 8601 duration strings', function (assert) {
    assert.equal(moment.duration({y: 1, M: 2, d: 3, h: 4, m: 5, s: 6}).toISOString(), 'P1Y2M3DT4H5M6S', 'all fields');
    assert.equal(moment.duration({M: -1}).toISOString(), '-P1M', 'one month ago');
    assert.equal(moment.duration({m: -1}).toISOString(), '-PT1M', 'one minute ago');
    assert.equal(moment.duration({s: -0.5}).toISOString(), '-PT0.5S', 'one half second ago');
    assert.equal(moment.duration({y: -0.5, M: 1}).toISOString(), '-P5M', 'a month after half a year ago');
    assert.equal(moment.duration({}).toISOString(), 'P0D', 'zero duration');
});

test('toString acts as toISOString', function (assert) {
    assert.equal(moment.duration({y: 1, M: 2, d: 3, h: 4, m: 5, s: 6}).toString(), 'P1Y2M3DT4H5M6S', 'all fields');
    assert.equal(moment.duration({M: -1}).toString(), '-P1M', 'one month ago');
    assert.equal(moment.duration({m: -1}).toString(), '-PT1M', 'one minute ago');
    assert.equal(moment.duration({s: -0.5}).toString(), '-PT0.5S', 'one half second ago');
    assert.equal(moment.duration({y: -0.5, M: 1}).toString(), '-P5M', 'a month after half a year ago');
    assert.equal(moment.duration({}).toString(), 'P0D', 'zero duration');
});

test('toIsoString deprecation', function (assert) {
    assert.equal(moment.duration({}).toIsoString(), moment.duration({}).toISOString(), 'toIsoString delegates to toISOString');
});

test('`isodate` (python) test cases', function (assert) {
    assert.equal(moment.duration('P18Y9M4DT11H9M8S').asSeconds(), moment.duration({y: 18, M: 9, d: 4, h: 11, m: 9, s: 8}).asSeconds(), 'python isodate 1');
    assert.equal(moment.duration('P2W').asSeconds(), moment.duration({w: 2}).asSeconds(), 'python isodate 2');
    assert.equal(moment.duration('P3Y6M4DT12H30M5S').asSeconds(), moment.duration({y: 3, M: 6, d: 4, h: 12, m: 30, s: 5}).asSeconds(), 'python isodate 3');
    assert.equal(moment.duration('P23DT23H').asSeconds(), moment.duration({d: 23, h: 23}).asSeconds(), 'python isodate 4');
    assert.equal(moment.duration('P4Y').asSeconds(), moment.duration({y: 4}).asSeconds(), 'python isodate 5');
    assert.equal(moment.duration('P1M').asSeconds(), moment.duration({M: 1}).asSeconds(), 'python isodate 6');
    assert.equal(moment.duration('PT1M').asSeconds(), moment.duration({m: 1}).asSeconds(), 'python isodate 7');
    assert.equal(moment.duration('P0.5Y').asSeconds(), moment.duration({y: 0.5}).asSeconds(), 'python isodate 8');
    assert.equal(moment.duration('PT36H').asSeconds(), moment.duration({h: 36}).asSeconds(), 'python isodate 9');
    assert.equal(moment.duration('P1DT12H').asSeconds(), moment.duration({d: 1, h: 12}).asSeconds(), 'python isodate 10');
    assert.equal(moment.duration('-P2W').asSeconds(), moment.duration({w: -2}).asSeconds(), 'python isodate 11');
    assert.equal(moment.duration('-P2.2W').asSeconds(), moment.duration({w: -2.2}).asSeconds(), 'python isodate 12');
    assert.equal(moment.duration('P1DT2H3M4S').asSeconds(), moment.duration({d: 1, h: 2, m: 3, s: 4}).asSeconds(), 'python isodate 13');
    assert.equal(moment.duration('P1DT2H3M').asSeconds(), moment.duration({d: 1, h: 2, m: 3}).asSeconds(), 'python isodate 14');
    assert.equal(moment.duration('P1DT2H').asSeconds(), moment.duration({d: 1, h: 2}).asSeconds(), 'python isodate 15');
    assert.equal(moment.duration('PT2H').asSeconds(), moment.duration({h: 2}).asSeconds(), 'python isodate 16');
    assert.equal(moment.duration('PT2.3H').asSeconds(), moment.duration({h: 2.3}).asSeconds(), 'python isodate 17');
    assert.equal(moment.duration('PT2H3M4S').asSeconds(), moment.duration({h: 2, m: 3, s: 4}).asSeconds(), 'python isodate 18');
    assert.equal(moment.duration('PT3M4S').asSeconds(), moment.duration({m: 3, s: 4}).asSeconds(), 'python isodate 19');
    assert.equal(moment.duration('PT22S').asSeconds(), moment.duration({s: 22}).asSeconds(), 'python isodate 20');
    assert.equal(moment.duration('PT22.22S').asSeconds(), moment.duration({s: 22.22}).asSeconds(), 'python isodate 21');
    assert.equal(moment.duration('-P2Y').asSeconds(), moment.duration({y: -2}).asSeconds(), 'python isodate 22');
    assert.equal(moment.duration('-P3Y6M4DT12H30M5S').asSeconds(), moment.duration({y: -3, M: -6, d: -4, h: -12, m: -30, s: -5}).asSeconds(), 'python isodate 23');
    assert.equal(moment.duration('-P1DT2H3M4S').asSeconds(), moment.duration({d: -1, h: -2, m: -3, s: -4}).asSeconds(), 'python isodate 24');
});

test('ISO 8601 misuse cases', function (assert) {
    assert.equal(moment.duration('P').asSeconds(), 0, 'lonely P');
    assert.equal(moment.duration('PT').asSeconds(), 0, 'just P and T');
    assert.equal(moment.duration('P1H').asSeconds(), 0, 'missing T');
    assert.equal(moment.duration('P1D1Y').asSeconds(), 0, 'out of order');
    assert.equal(moment.duration('PT.5S').asSeconds(), 0.5, 'accept no leading zero for decimal');
    assert.equal(moment.duration('PT1,S').asSeconds(), 1, 'accept trailing decimal separator');
    assert.equal(moment.duration('PT1M0,,5S').asSeconds(), 60, 'extra decimal separators are ignored as 0');
    //assert.equal(moment.duration('P-1DS').asSeconds(), 0, 'wrong position of negative');
});

test('humanize', function (assert) {
    moment.locale('en');
    assert.equal(moment.duration({seconds: 44}).humanize(),  'a few seconds', '44 seconds = a few seconds');
    assert.equal(moment.duration({seconds: 45}).humanize(),  'a minute',      '45 seconds = a minute');
    assert.equal(moment.duration({seconds: 89}).humanize(),  'a minute',      '89 seconds = a minute');
    assert.equal(moment.duration({seconds: 90}).humanize(),  '2 minutes',     '90 seconds = 2 minutes');
    assert.equal(moment.duration({minutes: 44}).humanize(),  '44 minutes',    '44 minutes = 44 minutes');
    assert.equal(moment.duration({minutes: 45}).humanize(),  'an hour',       '45 minutes = an hour');
    assert.equal(moment.duration({minutes: 89}).humanize(),  'an hour',       '89 minutes = an hour');
    assert.equal(moment.duration({minutes: 90}).humanize(),  '2 hours',       '90 minutes = 2 hours');
    assert.equal(moment.duration({hours: 5}).humanize(),     '5 hours',       '5 hours = 5 hours');
    assert.equal(moment.duration({hours: 21}).humanize(),    '21 hours',      '21 hours = 21 hours');
    assert.equal(moment.duration({hours: 22}).humanize(),    'a day',         '22 hours = a day');
    assert.equal(moment.duration({hours: 35}).humanize(),    'a day',         '35 hours = a day');
    assert.equal(moment.duration({hours: 36}).humanize(),    '2 days',        '36 hours = 2 days');
    assert.equal(moment.duration({days: 1}).humanize(),      'a day',         '1 day = a day');
    assert.equal(moment.duration({days: 5}).humanize(),      '5 days',        '5 days = 5 days');
    assert.equal(moment.duration({weeks: 1}).humanize(),     '7 days',        '1 week = 7 days');
    assert.equal(moment.duration({days: 25}).humanize(),     '25 days',       '25 days = 25 days');
    assert.equal(moment.duration({days: 26}).humanize(),     'a month',       '26 days = a month');
    assert.equal(moment.duration({days: 30}).humanize(),     'a month',       '30 days = a month');
    assert.equal(moment.duration({days: 45}).humanize(),     'a month',       '45 days = a month');
    assert.equal(moment.duration({days: 46}).humanize(),     '2 months',      '46 days = 2 months');
    assert.equal(moment.duration({days: 74}).humanize(),     '2 months',      '74 days = 2 months');
    assert.equal(moment.duration({days: 77}).humanize(),     '3 months',      '77 days = 3 months');
    assert.equal(moment.duration({months: 1}).humanize(),    'a month',       '1 month = a month');
    assert.equal(moment.duration({months: 5}).humanize(),    '5 months',      '5 months = 5 months');
    assert.equal(moment.duration({days: 344}).humanize(),    'a year',        '344 days = a year');
    assert.equal(moment.duration({days: 345}).humanize(),    'a year',        '345 days = a year');
    assert.equal(moment.duration({days: 547}).humanize(),    'a year',        '547 days = a year');
    assert.equal(moment.duration({days: 548}).humanize(),    '2 years',       '548 days = 2 years');
    assert.equal(moment.duration({years: 1}).humanize(),     'a year',        '1 year = a year');
    assert.equal(moment.duration({years: 5}).humanize(),     '5 years',       '5 years = 5 years');
    assert.equal(moment.duration(7200000).humanize(),        '2 hours',       '7200000 = 2 minutes');
});

test('humanize duration with suffix', function (assert) {
    moment.locale('en');
    assert.equal(moment.duration({seconds:  44}).humanize(true),  'in a few seconds', '44 seconds = a few seconds');
    assert.equal(moment.duration({seconds: -44}).humanize(true),  'a few seconds ago', '44 seconds = a few seconds');
});

test('bubble value up', function (assert) {
    assert.equal(moment.duration({milliseconds: 61001}).milliseconds(), 1, '61001 milliseconds has 1 millisecond left over');
    assert.equal(moment.duration({milliseconds: 61001}).seconds(),      1, '61001 milliseconds has 1 second left over');
    assert.equal(moment.duration({milliseconds: 61001}).minutes(),      1, '61001 milliseconds has 1 minute left over');

    assert.equal(moment.duration({minutes: 350}).minutes(), 50, '350 minutes has 50 minutes left over');
    assert.equal(moment.duration({minutes: 350}).hours(),   5,  '350 minutes has 5 hours left over');
});

test('clipping', function (assert) {
    assert.equal(moment.duration({months: 11}).months(), 11, '11 months is 11 months');
    assert.equal(moment.duration({months: 11}).years(),  0,  '11 months makes no year');
    assert.equal(moment.duration({months: 12}).months(), 0,  '12 months is 0 months left over');
    assert.equal(moment.duration({months: 12}).years(),  1,  '12 months makes 1 year');
    assert.equal(moment.duration({months: 13}).months(), 1,  '13 months is 1 month left over');
    assert.equal(moment.duration({months: 13}).years(),  1,  '13 months makes 1 year');

    assert.equal(moment.duration({days: 29}).days(),   29, '29 days is 29 days');
    assert.equal(moment.duration({days: 29}).months(), 0,  '29 days makes no month');
    assert.equal(moment.duration({days: 30}).days(),   0,  '30 days is 0 days left over');
    assert.equal(moment.duration({days: 30}).months(), 1,  '30 days is a month');
    assert.equal(moment.duration({days: 31}).days(),   1,  '31 days is 1 day left over');
    assert.equal(moment.duration({days: 31}).months(), 1,  '31 days is a month');

    assert.equal(moment.duration({hours: 23}).hours(), 23, '23 hours is 23 hours');
    assert.equal(moment.duration({hours: 23}).days(),  0,  '23 hours makes no day');
    assert.equal(moment.duration({hours: 24}).hours(), 0,  '24 hours is 0 hours left over');
    assert.equal(moment.duration({hours: 24}).days(),  1,  '24 hours makes 1 day');
    assert.equal(moment.duration({hours: 25}).hours(), 1,  '25 hours is 1 hour left over');
    assert.equal(moment.duration({hours: 25}).days(),  1,  '25 hours makes 1 day');
});

test('effective equivalency', function (assert) {
    assert.deepEqual(moment.duration({seconds: 1})._data,  moment.duration({milliseconds: 1000})._data, '1 second is the same as 1000 milliseconds');
    assert.deepEqual(moment.duration({seconds: 60})._data, moment.duration({minutes: 1})._data,         '1 minute is the same as 60 seconds');
    assert.deepEqual(moment.duration({minutes: 60})._data, moment.duration({hours: 1})._data,           '1 hour is the same as 60 minutes');
    assert.deepEqual(moment.duration({hours: 24})._data,   moment.duration({days: 1})._data,            '1 day is the same as 24 hours');
    assert.deepEqual(moment.duration({days: 7})._data,     moment.duration({weeks: 1})._data,           '1 week is the same as 7 days');
    assert.deepEqual(moment.duration({days: 30})._data,    moment.duration({months: 1})._data,          '1 month is the same as 30 days');
    assert.deepEqual(moment.duration({months: 12})._data,  moment.duration({years: 1})._data,           '1 years is the same as 12 months');
});

test('asGetters', function (assert) {
    // 400 years have exactly 146097 days

    // years
    assert.equal(moment.duration(1, 'year').asYears(),            1,           '1 year as years');
    assert.equal(moment.duration(1, 'year').asMonths(),           12,          '1 year as months');
    assert.equal(moment.duration(400, 'year').asMonths(),         4800,        '400 years as months');
    assert.equal(moment.duration(1, 'year').asWeeks().toFixed(3), 52.143,      '1 year as weeks');
    assert.equal(moment.duration(1, 'year').asDays(),             365,         '1 year as days');
    assert.equal(moment.duration(2, 'year').asDays(),             730,         '2 years as days');
    assert.equal(moment.duration(3, 'year').asDays(),             1096,        '3 years as days');
    assert.equal(moment.duration(4, 'year').asDays(),             1461,        '4 years as days');
    assert.equal(moment.duration(400, 'year').asDays(),           146097,      '400 years as days');
    assert.equal(moment.duration(1, 'year').asHours(),            8760,        '1 year as hours');
    assert.equal(moment.duration(1, 'year').asMinutes(),          525600,      '1 year as minutes');
    assert.equal(moment.duration(1, 'year').asSeconds(),          31536000,    '1 year as seconds');
    assert.equal(moment.duration(1, 'year').asMilliseconds(),     31536000000, '1 year as milliseconds');

    // months
    assert.equal(moment.duration(1, 'month').asYears().toFixed(4), 0.0833,     '1 month as years');
    assert.equal(moment.duration(1, 'month').asMonths(),           1,          '1 month as months');
    assert.equal(moment.duration(1, 'month').asWeeks().toFixed(3), 4.286,      '1 month as weeks');
    assert.equal(moment.duration(1, 'month').asDays(),             30,         '1 month as days');
    assert.equal(moment.duration(2, 'month').asDays(),             61,         '2 months as days');
    assert.equal(moment.duration(3, 'month').asDays(),             91,         '3 months as days');
    assert.equal(moment.duration(4, 'month').asDays(),             122,        '4 months as days');
    assert.equal(moment.duration(5, 'month').asDays(),             152,        '5 months as days');
    assert.equal(moment.duration(6, 'month').asDays(),             183,        '6 months as days');
    assert.equal(moment.duration(7, 'month').asDays(),             213,        '7 months as days');
    assert.equal(moment.duration(8, 'month').asDays(),             243,        '8 months as days');
    assert.equal(moment.duration(9, 'month').asDays(),             274,        '9 months as days');
    assert.equal(moment.duration(10, 'month').asDays(),            304,        '10 months as days');
    assert.equal(moment.duration(11, 'month').asDays(),            335,        '11 months as days');
    assert.equal(moment.duration(12, 'month').asDays(),            365,        '12 months as days');
    assert.equal(moment.duration(24, 'month').asDays(),            730,        '24 months as days');
    assert.equal(moment.duration(36, 'month').asDays(),            1096,       '36 months as days');
    assert.equal(moment.duration(48, 'month').asDays(),            1461,       '48 months as days');
    assert.equal(moment.duration(4800, 'month').asDays(),          146097,     '4800 months as days');
    assert.equal(moment.duration(1, 'month').asHours(),            720,        '1 month as hours');
    assert.equal(moment.duration(1, 'month').asMinutes(),          43200,      '1 month as minutes');
    assert.equal(moment.duration(1, 'month').asSeconds(),          2592000,    '1 month as seconds');
    assert.equal(moment.duration(1, 'month').asMilliseconds(),     2592000000, '1 month as milliseconds');

    // weeks
    assert.equal(moment.duration(1, 'week').asYears().toFixed(4),  0.0192,    '1 week as years');
    assert.equal(moment.duration(1, 'week').asMonths().toFixed(3), 0.230,     '1 week as months');
    assert.equal(moment.duration(1, 'week').asWeeks(),             1,         '1 week as weeks');
    assert.equal(moment.duration(1, 'week').asDays(),              7,         '1 week as days');
    assert.equal(moment.duration(1, 'week').asHours(),             168,       '1 week as hours');
    assert.equal(moment.duration(1, 'week').asMinutes(),           10080,     '1 week as minutes');
    assert.equal(moment.duration(1, 'week').asSeconds(),           604800,    '1 week as seconds');
    assert.equal(moment.duration(1, 'week').asMilliseconds(),      604800000, '1 week as milliseconds');

    // days
    assert.equal(moment.duration(1, 'day').asYears().toFixed(4),  0.0027,   '1 day as years');
    assert.equal(moment.duration(1, 'day').asMonths().toFixed(3), 0.033,    '1 day as months');
    assert.equal(moment.duration(1, 'day').asWeeks().toFixed(3),  0.143,    '1 day as weeks');
    assert.equal(moment.duration(1, 'day').asDays(),              1,        '1 day as days');
    assert.equal(moment.duration(1, 'day').asHours(),             24,       '1 day as hours');
    assert.equal(moment.duration(1, 'day').asMinutes(),           1440,     '1 day as minutes');
    assert.equal(moment.duration(1, 'day').asSeconds(),           86400,    '1 day as seconds');
    assert.equal(moment.duration(1, 'day').asMilliseconds(),      86400000, '1 day as milliseconds');

    // hours
    assert.equal(moment.duration(1, 'hour').asYears().toFixed(6),  0.000114, '1 hour as years');
    assert.equal(moment.duration(1, 'hour').asMonths().toFixed(5), 0.00137,  '1 hour as months');
    assert.equal(moment.duration(1, 'hour').asWeeks().toFixed(5),  0.00595,  '1 hour as weeks');
    assert.equal(moment.duration(1, 'hour').asDays().toFixed(4),   0.0417,   '1 hour as days');
    assert.equal(moment.duration(1, 'hour').asHours(),             1,        '1 hour as hours');
    assert.equal(moment.duration(1, 'hour').asMinutes(),           60,       '1 hour as minutes');
    assert.equal(moment.duration(1, 'hour').asSeconds(),           3600,     '1 hour as seconds');
    assert.equal(moment.duration(1, 'hour').asMilliseconds(),      3600000,  '1 hour as milliseconds');

    // minutes
    assert.equal(moment.duration(1, 'minute').asYears().toFixed(8),  0.00000190, '1 minute as years');
    assert.equal(moment.duration(1, 'minute').asMonths().toFixed(7), 0.0000228,  '1 minute as months');
    assert.equal(moment.duration(1, 'minute').asWeeks().toFixed(7),  0.0000992,  '1 minute as weeks');
    assert.equal(moment.duration(1, 'minute').asDays().toFixed(6),   0.000694,   '1 minute as days');
    assert.equal(moment.duration(1, 'minute').asHours().toFixed(4),  0.0167,     '1 minute as hours');
    assert.equal(moment.duration(1, 'minute').asMinutes(),           1,          '1 minute as minutes');
    assert.equal(moment.duration(1, 'minute').asSeconds(),           60,         '1 minute as seconds');
    assert.equal(moment.duration(1, 'minute').asMilliseconds(),      60000,      '1 minute as milliseconds');

    // seconds
    assert.equal(moment.duration(1, 'second').asYears().toFixed(10),  0.0000000317, '1 second as years');
    assert.equal(moment.duration(1, 'second').asMonths().toFixed(9),  0.000000380,  '1 second as months');
    assert.equal(moment.duration(1, 'second').asWeeks().toFixed(8),   0.00000165,   '1 second as weeks');
    assert.equal(moment.duration(1, 'second').asDays().toFixed(7),    0.0000116,    '1 second as days');
    assert.equal(moment.duration(1, 'second').asHours().toFixed(6),   0.000278,     '1 second as hours');
    assert.equal(moment.duration(1, 'second').asMinutes().toFixed(4), 0.0167,       '1 second as minutes');
    assert.equal(moment.duration(1, 'second').asSeconds(),            1,            '1 second as seconds');
    assert.equal(moment.duration(1, 'second').asMilliseconds(),       1000,         '1 second as milliseconds');

    // milliseconds
    assert.equal(moment.duration(1, 'millisecond').asYears().toFixed(13),  0.0000000000317, '1 millisecond as years');
    assert.equal(moment.duration(1, 'millisecond').asMonths().toFixed(12), 0.000000000380,  '1 millisecond as months');
    assert.equal(moment.duration(1, 'millisecond').asWeeks().toFixed(11),  0.00000000165,   '1 millisecond as weeks');
    assert.equal(moment.duration(1, 'millisecond').asDays().toFixed(10),   0.0000000116,    '1 millisecond as days');
    assert.equal(moment.duration(1, 'millisecond').asHours().toFixed(9),   0.000000278,     '1 millisecond as hours');
    assert.equal(moment.duration(1, 'millisecond').asMinutes().toFixed(7), 0.0000167,       '1 millisecond as minutes');
    assert.equal(moment.duration(1, 'millisecond').asSeconds(),            0.001,           '1 millisecond as seconds');
    assert.equal(moment.duration(1, 'millisecond').asMilliseconds(),       1,               '1 millisecond as milliseconds');
});

test('as getters for small units', function (assert) {
    var dS = moment.duration(1, 'milliseconds'),
        ds = moment.duration(3, 'seconds'),
        dm = moment.duration(13, 'minutes');

    // Tests for issue #1867.
    // Floating point errors for small duration units were introduced in version 2.8.0.
    assert.equal(dS.as('milliseconds'), 1, 'as("milliseconds")');
    assert.equal(dS.asMilliseconds(),   1, 'asMilliseconds()');
    assert.equal(ds.as('seconds'),      3, 'as("seconds")');
    assert.equal(ds.asSeconds(),        3, 'asSeconds()');
    assert.equal(dm.as('minutes'),      13, 'as("minutes")');
    assert.equal(dm.asMinutes(),        13, 'asMinutes()');
});

test('isDuration', function (assert) {
    assert.ok(moment.isDuration(moment.duration(12345678)), 'correctly says true');
    assert.ok(!moment.isDuration(moment()), 'moment object is not a duration');
    assert.ok(!moment.isDuration({milliseconds: 1}), 'plain object is not a duration');
});

test('add', function (assert) {
    var d = moment.duration({months: 4, weeks: 3, days: 2});
    // for some reason, d._data._months does not get updated; use d._months instead.
    assert.equal(d.add(1, 'month')._months, 5, 'Add months');
    assert.equal(d.add(5, 'days')._days, 28, 'Add days');
    assert.equal(d.add(10000)._milliseconds, 10000, 'Add milliseconds');
    assert.equal(d.add({h: 23, m: 59})._milliseconds, 23 * 60 * 60 * 1000 + 59 * 60 * 1000 + 10000, 'Add hour:minute');
});

test('add and bubble', function (assert) {
    assert.equal(moment.duration(1, 'second').add(1000, 'milliseconds').seconds(), 2, 'Adding milliseconds should bubble up to seconds');
    assert.equal(moment.duration(1, 'minute').add(60, 'second').minutes(), 2, 'Adding seconds should bubble up to minutes');
    assert.equal(moment.duration(1, 'hour').add(60, 'minutes').hours(), 2, 'Adding minutes should bubble up to hours');
    assert.equal(moment.duration(1, 'day').add(24, 'hours').days(), 2, 'Adding hours should bubble up to days');
});

test('subtract and bubble', function (assert) {
    assert.equal(moment.duration(2, 'second').subtract(1000, 'milliseconds').seconds(), 1, 'Subtracting milliseconds should bubble up to seconds');
    assert.equal(moment.duration(2, 'minute').subtract(60, 'second').minutes(), 1, 'Subtracting seconds should bubble up to minutes');
    assert.equal(moment.duration(2, 'hour').subtract(60, 'minutes').hours(), 1, 'Subtracting minutes should bubble up to hours');
    assert.equal(moment.duration(2, 'day').subtract(24, 'hours').days(), 1, 'Subtracting hours should bubble up to days');
});

test('subtract', function (assert) {
    var d = moment.duration({months: 2, weeks: 2, days: 0, hours: 5});
    // for some reason, d._data._months does not get updated; use d._months instead.
    assert.equal(d.subtract(1, 'months')._months, 1, 'Subtract months');
    assert.equal(d.subtract(14, 'days')._days, 0, 'Subtract days');
    assert.equal(d.subtract(10000)._milliseconds, 5 * 60 * 60 * 1000 - 10000, 'Subtract milliseconds');
    assert.equal(d.subtract({h: 1, m: 59})._milliseconds, 3 * 60 * 60 * 1000 + 1 * 60 * 1000 - 10000, 'Subtract hour:minute');
});

test('JSON.stringify duration', function (assert) {
    var d = moment.duration(1024, 'h');

    assert.equal(JSON.stringify(d), '"' + d.toISOString() + '"', 'JSON.stringify on duration should return ISO string');
});

test('duration plugins', function (assert) {
    var durationObject = moment.duration();
    moment.duration.fn.foo = function (arg) {
        assert.equal(this, durationObject);
        assert.equal(arg, 5);
    };
    durationObject.foo(5);
});
//===================================================================================================================================================================================================================== //>
module('duration from moments');

test('pure year diff', function (assert) {
    var m1 = moment('2012-01-01T00:00:00.000Z'),
        m2 = moment('2013-01-01T00:00:00.000Z');

    assert.equal(moment.duration({from: m1, to: m2}).as('years'), 1, 'year moment difference');
    //assert.equal(moment.duration({from: m2, to: m1}).as('years'), -1, 'negative year moment difference');
});

test('month and day diff', function (assert) {
    var m1 = moment('2012-01-15T00:00:00.000Z'),
        m2 = moment('2012-02-17T00:00:00.000Z'),
        d = moment.duration({from: m1, to: m2});

    assert.equal(d.get('days'), 2);
    assert.equal(d.get('months'), 1);
});

test('day diff, separate months', function (assert) {
    var m1 = moment('2012-01-15T00:00:00.000Z'),
        m2 = moment('2012-02-13T00:00:00.000Z'),
        d = moment.duration({from: m1, to: m2});

    assert.equal(d.as('days'), 29);
});

test('hour diff', function (assert) {
    var m1 = moment('2012-01-15T17:00:00.000Z'),
        m2 = moment('2012-01-16T03:00:00.000Z'),
        d = moment.duration({from: m1, to: m2});

    assert.equal(d.as('hours'), 10);
});

test('minute diff', function (assert) {
    var m1 = moment('2012-01-15T17:45:00.000Z'),
        m2 = moment('2012-01-16T03:15:00.000Z'),
        d = moment.duration({from: m1, to: m2});

    assert.equal(d.as('hours'), 9.5);
});
//===================================================================================================================================================================================================================== //>
module('format');

test('format YY', function (assert) {
    var b = moment(new Date(2009, 1, 14, 15, 25, 50, 125));
    assert.equal(b.format('YY'), '09', 'YY ---> 09');
});

test('format escape brackets', function (assert) {
    moment.locale('en');

    var b = moment(new Date(2009, 1, 14, 15, 25, 50, 125));
    assert.equal(b.format('[day]'), 'day', 'Single bracket');
    assert.equal(b.format('[day] YY [YY]'), 'day 09 YY', 'Double bracket');
    assert.equal(b.format('[YY'), '[09', 'Un-ended bracket');
    assert.equal(b.format('[[YY]]'), '[YY]', 'Double nested brackets');
    assert.equal(b.format('[[]'), '[', 'Escape open bracket');
    assert.equal(b.format('[Last]'), 'Last', 'localized tokens');
    assert.equal(b.format('[L] L'), 'L 02/14/2009', 'localized tokens with escaped localized tokens');
    assert.equal(b.format('[L LL LLL LLLL aLa]'), 'L LL LLL LLLL aLa', 'localized tokens with escaped localized tokens');
    assert.equal(b.format('[LLL] LLL'), 'LLL February 14, 2009 3:25 PM', 'localized tokens with escaped localized tokens (recursion)');
    assert.equal(b.format('YYYY[\n]DD[\n]'), '2009\n14\n', 'Newlines');
});
/* REMOVED .Net não suporta anos negativos
test('handle negative years', function (assert) {
    moment.locale('en');
    assert.equal(moment.utc().year(-1).format('YY'), '-01', 'YY with negative year');
    assert.equal(moment.utc().year(-1).format('YYYY'), '-0001', 'YYYY with negative year');
    assert.equal(moment.utc().year(-12).format('YY'), '-12', 'YY with negative year');
    assert.equal(moment.utc().year(-12).format('YYYY'), '-0012', 'YYYY with negative year');
    assert.equal(moment.utc().year(-123).format('YY'), '-23', 'YY with negative year');
    assert.equal(moment.utc().year(-123).format('YYYY'), '-0123', 'YYYY with negative year');
    assert.equal(moment.utc().year(-1234).format('YY'), '-34', 'YY with negative year');
    assert.equal(moment.utc().year(-1234).format('YYYY'), '-1234', 'YYYY with negative year');
    assert.equal(moment.utc().year(-12345).format('YY'), '-45', 'YY with negative year');
    assert.equal(moment.utc().year(-12345).format('YYYY'), '-12345', 'YYYY with negative year');
});
*/
test('format milliseconds', function (assert) {
    var b = moment(new Date(2009, 1, 14, 15, 25, 50, 123));
    assert.equal(b.format('S'), '1', 'Deciseconds');
    assert.equal(b.format('SS'), '12', 'Centiseconds');
    assert.equal(b.format('SSS'), '123', 'Milliseconds');
    b.milliseconds(789);
    assert.equal(b.format('S'), '7', 'Deciseconds');
    assert.equal(b.format('SS'), '78', 'Centiseconds');
    assert.equal(b.format('SSS'), '789', 'Milliseconds');
});

test('format timezone', function (assert) {
    var b = moment(new Date(2010, 1, 14, 15, 25, 50, 125));
    assert.ok(b.format('Z').match(/^[\+\-]\d\d:\d\d$/), b.format('Z') + ' should be something like \'+07:30\'');
    assert.ok(b.format('ZZ').match(/^[\+\-]\d{4}$/), b.format('ZZ') + ' should be something like \'+0700\'');
});

test('format multiple with utc offset', function (assert) {
    var b = moment('2012-10-08 -1200', ['YYYY-MM-DD HH:mm ZZ', 'YYYY-MM-DD ZZ', 'YYYY-MM-DD']);
    assert.equal(b.format('YYYY-MM'), '2012-10', 'Parsing multiple formats should not crash with different sized formats');
});

test('isDST', function (assert) {
    var janOffset = new Date(2011, 0, 1).getTimezoneOffset(),
        julOffset = new Date(2011, 6, 1).getTimezoneOffset(),
        janIsDst = janOffset < julOffset,
        julIsDst = julOffset < janOffset,
        jan1 = moment([2011]),
        jul1 = moment([2011, 6]);

    if (janIsDst && julIsDst) {
        assert.ok(0, 'January and July cannot both be in DST');
        assert.ok(0, 'January and July cannot both be in DST');
    } else if (janIsDst) {
        assert.ok(jan1.isDST(), 'January 1 is DST');
        assert.ok(!jul1.isDST(), 'July 1 is not DST');
    } else if (julIsDst) {
        assert.ok(!jan1.isDST(), 'January 1 is not DST');
        assert.ok(jul1.isDST(), 'July 1 is DST');
    } else {
        assert.ok(!jan1.isDST(), 'January 1 is not DST');
        assert.ok(!jul1.isDST(), 'July 1 is not DST');
    }
});
/* REMOVED .Net não suporta formato Unix
test('unix timestamp', function (assert) {
    var m = moment('1234567890.123', 'X');
    assert.equal(m.format('X'), '1234567890', 'unix timestamp without milliseconds');
    assert.equal(m.format('X.S'), '1234567890.1', 'unix timestamp with deciseconds');
    assert.equal(m.format('X.SS'), '1234567890.12', 'unix timestamp with centiseconds');
    assert.equal(m.format('X.SSS'), '1234567890.123', 'unix timestamp with milliseconds');

    m = moment(1234567890.123, 'X');
    assert.equal(m.format('X'), '1234567890', 'unix timestamp as integer');
});
*/
/* REMOVED .Net não suporta formato Unix
test('unix offset milliseconds', function (assert) {
    var m = moment('1234567890123', 'x');
    assert.equal(m.format('x'), '1234567890123', 'unix offset in milliseconds');

    m = moment(1234567890123, 'x');
    assert.equal(m.format('x'), '1234567890123', 'unix offset in milliseconds as integer');
});
*/
test('utcOffset sanity checks', function (assert) {
    assert.equal(moment().utcOffset() % 15, 0,
            'utc offset should be a multiple of 15 (was ' + moment().utcOffset() + ')');

    assert.equal(moment().utcOffset(), -(new Date()).getTimezoneOffset(),
        'utcOffset should return the opposite of getTimezoneOffset');
});

test('default format', function (assert) {
    var isoRegex = /\d{4}.\d\d.\d\dT\d\d.\d\d.\d\d[\+\-]\d\d:\d\d/;
    assert.ok(isoRegex.exec(moment().format()), 'default format (' + moment().format() + ') should match ISO');
});

test('toJSON', function (assert) {
    var supportsJson = typeof JSON !== 'undefined' && JSON.stringify && JSON.stringify.call,
        date = moment('2012-10-09T21:30:40.678+0100');

    assert.equal(date.toJSON(), '2012-10-09T20:30:40.678Z', 'should output ISO8601 on moment.fn.toJSON');

    if (supportsJson) {
        assert.equal(JSON.stringify({
            date : date
        }), '{"date":"2012-10-09T20:30:40.678Z"}', 'should output ISO8601 on JSON.stringify');
    }
});
/* REMOVED .Net não suporta anos negativos
test('toISOString', function (assert) {
    var date = moment.utc('2012-10-09T20:30:40.678');

    assert.equal(date.toISOString(), '2012-10-09T20:30:40.678Z', 'should output ISO8601 on moment.fn.toISOString');

     big years
    date = moment.utc('+020123-10-09T20:30:40.678');
    assert.equal(date.toISOString(), '+020123-10-09T20:30:40.678Z', 'ISO8601 format on big positive year');
     negative years
    date = moment.utc('-000001-10-09T20:30:40.678');
    assert.equal(date.toISOString(), '-000001-10-09T20:30:40.678Z', 'ISO8601 format on negative year');
     big negative years
    date = moment.utc('-020123-10-09T20:30:40.678');
    assert.equal(date.toISOString(), '-020123-10-09T20:30:40.678Z', 'ISO8601 format on big negative year');
});
*/
/* REMOVED .Net não suporta anos negativos
test('long years', function (assert) {
    assert.equal(moment.utc().year(2).format('YYYYYY'), '+000002', 'small year with YYYYYY');
    assert.equal(moment.utc().year(2012).format('YYYYYY'), '+002012', 'regular year with YYYYYY');
    assert.equal(moment.utc().year(20123).format('YYYYYY'), '+020123', 'big year with YYYYYY');

    assert.equal(moment.utc().year(-1).format('YYYYYY'), '-000001', 'small negative year with YYYYYY');
    assert.equal(moment.utc().year(-2012).format('YYYYYY'), '-002012', 'negative year with YYYYYY');
    assert.equal(moment.utc().year(-20123).format('YYYYYY'), '-020123', 'big negative year with YYYYYY');
});
*/
/* REMOVED .Net não suporta anos negativos
test('iso week formats', function (assert) {
    // http://en.wikipedia.org/wiki/ISO_week_date
    var cases = {
        '2005-01-02': '2004-53',
        '2005-12-31': '2005-52',
        '2007-01-01': '2007-01',
        '2007-12-30': '2007-52',
        '2007-12-31': '2008-01',
        '2008-01-01': '2008-01',
        '2008-12-28': '2008-52',
        '2008-12-29': '2009-01',
        '2008-12-30': '2009-01',
        '2008-12-31': '2009-01',
        '2009-01-01': '2009-01',
        '2009-12-31': '2009-53',
        '2010-01-01': '2009-53',
        '2010-01-02': '2009-53',
        '2010-01-03': '2009-53',
        '404-12-31': '0404-53',
        '405-12-31': '0405-52'
    }, i, isoWeek, formatted2, formatted1;

    for (i in cases) {
        isoWeek = cases[i].split('-').pop();
        formatted2 = moment(i, 'YYYY-MM-DD').format('WW');
        assert.equal(isoWeek, formatted2, i + ': WW should be ' + isoWeek + ', but ' + formatted2);
        isoWeek = isoWeek.replace(/^0+/, '');
        formatted1 = moment(i, 'YYYY-MM-DD').format('W');
        assert.equal(isoWeek, formatted1, i + ': W should be ' + isoWeek + ', but ' + formatted1);
    }
});
*/
/* REMOVED .Net não suporta anos negativos
test('iso week year formats', function (assert) {
    // http://en.wikipedia.org/wiki/ISO_week_date
    var cases = {
        '2005-01-02': '2004-53',
        '2005-12-31': '2005-52',
        '2007-01-01': '2007-01',
        '2007-12-30': '2007-52',
        '2007-12-31': '2008-01',
        '2008-01-01': '2008-01',
        '2008-12-28': '2008-52',
        '2008-12-29': '2009-01',
        '2008-12-30': '2009-01',
        '2008-12-31': '2009-01',
        '2009-01-01': '2009-01',
        '2009-12-31': '2009-53',
        '2010-01-01': '2009-53',
        '2010-01-02': '2009-53',
        '2010-01-03': '2009-53',
        '404-12-31': '0404-53',
        '405-12-31': '0405-52'
    }, i, isoWeekYear, formatted5, formatted4, formatted2;

    for (i in cases) {
        isoWeekYear = cases[i].split('-')[0];
        formatted5 = moment(i, 'YYYY-MM-DD').format('GGGGG');
        assert.equal('0' + isoWeekYear, formatted5, i + ': GGGGG should be ' + isoWeekYear + ', but ' + formatted5);
        formatted4 = moment(i, 'YYYY-MM-DD').format('GGGG');
        assert.equal(isoWeekYear, formatted4, i + ': GGGG should be ' + isoWeekYear + ', but ' + formatted4);
        formatted2 = moment(i, 'YYYY-MM-DD').format('GG');
        assert.equal(isoWeekYear.slice(2, 4), formatted2, i + ': GG should be ' + isoWeekYear + ', but ' + formatted2);
    }
});
*/
/* REMOVED .Net não suporta ano pequenos (05=2005)
test('week year formats', function (assert) {
    // http://en.wikipedia.org/wiki/ISO_week_date
    var cases = {
        '2005-01-02': '2004-53',
        '2005-12-31': '2005-52',
        '2007-01-01': '2007-01',
        '2007-12-30': '2007-52',
        '2007-12-31': '2008-01',
        '2008-01-01': '2008-01',
        '2008-12-28': '2008-52',
        '2008-12-29': '2009-01',
        '2008-12-30': '2009-01',
        '2008-12-31': '2009-01',
        '2009-01-01': '2009-01',
        '2009-12-31': '2009-53',
        '2010-01-01': '2009-53',
        '2010-01-02': '2009-53',
        '2010-01-03': '2009-53',
        '404-12-31': '0404-53',
        '405-12-31': '0405-52'
    }, i, isoWeekYear, formatted5, formatted4, formatted2;

    moment.locale('dow:1,doy:4', {week: {dow: 1, doy: 4}});

    for (i in cases) {
        isoWeekYear = cases[i].split('-')[0];
        formatted5 = moment(i, 'YYYY-MM-DD').format('ggggg');
        assert.equal('0' + isoWeekYear, formatted5, i + ': ggggg should be ' + isoWeekYear + ', but ' + formatted5);
        formatted4 = moment(i, 'YYYY-MM-DD').format('gggg');
        assert.equal(isoWeekYear, formatted4, i + ': gggg should be ' + isoWeekYear + ', but ' + formatted4);
        formatted2 = moment(i, 'YYYY-MM-DD').format('gg');
        assert.equal(isoWeekYear.slice(2, 4), formatted2, i + ': gg should be ' + isoWeekYear + ', but ' + formatted2);
    }
});
*/

test('iso weekday formats', function (assert) {
    assert.equal(moment([1985, 1,  4]).format('E'), '1', 'Feb  4 1985 is Monday    -- 1st day');
    assert.equal(moment([2029, 8, 18]).format('E'), '2', 'Sep 18 2029 is Tuesday   -- 2nd day');
    assert.equal(moment([2013, 3, 24]).format('E'), '3', 'Apr 24 2013 is Wednesday -- 3rd day');
    assert.equal(moment([2015, 2,  5]).format('E'), '4', 'Mar  5 2015 is Thursday  -- 4th day');
    assert.equal(moment([1970, 0,  2]).format('E'), '5', 'Jan  2 1970 is Friday    -- 5th day');
    assert.equal(moment([2001, 4, 12]).format('E'), '6', 'May 12 2001 is Saturday  -- 6th day');
    assert.equal(moment([2000, 0,  2]).format('E'), '7', 'Jan  2 2000 is Sunday    -- 7th day');
});

test('weekday formats', function (assert) {
    moment.locale('dow: 3,doy: 5', {week: {dow: 3, doy: 5}});
    assert.equal(moment([1985, 1,  6]).format('e'), '0', 'Feb  6 1985 is Wednesday -- 0th day');
    assert.equal(moment([2029, 8, 20]).format('e'), '1', 'Sep 20 2029 is Thursday  -- 1st day');
    assert.equal(moment([2013, 3, 26]).format('e'), '2', 'Apr 26 2013 is Friday    -- 2nd day');
    assert.equal(moment([2015, 2,  7]).format('e'), '3', 'Mar  7 2015 is Saturday  -- 3nd day');
    assert.equal(moment([1970, 0,  4]).format('e'), '4', 'Jan  4 1970 is Sunday    -- 4th day');
    assert.equal(moment([2001, 4, 14]).format('e'), '5', 'May 14 2001 is Monday    -- 5th day');
    assert.equal(moment([2000, 0,  4]).format('e'), '6', 'Jan  4 2000 is Tuesday   -- 6th day');
});

test('toString is just human readable format', function (assert) {
    var b = moment(new Date(2009, 1, 5, 15, 25, 50, 125));
    assert.equal(b.toString(), b.format('ddd MMM DD YYYY HH:mm:ss [GMT]ZZ'));
});

test('toJSON skips postformat', function (assert) {
    moment.locale('postformat', {
        postformat: function (s) {
            s.replace(/./g, 'X');
        }
    });
    assert.equal(moment.utc([2000, 0, 1]).toJSON(), '2000-01-01T00:00:00.000Z', 'toJSON doesn\'t postformat');
    moment.locale('postformat', null);
});

test('calendar day timezone', function (assert) {
    moment.locale('en');
    var zones = [60, -60, 90, -90, 360, -360, 720, -720],
        b = moment().utc().startOf('day').subtract({m : 1}),
        c = moment().local().startOf('day').subtract({m : 1}),
        d = moment().local().startOf('day').subtract({d : 2}),
        i, z, a;

    for (i = 0; i < zones.length; ++i) {
        z = zones[i];
        a = moment().utcOffset(z).startOf('day').subtract({m: 1});
        assert.equal(moment(a).utcOffset(z).calendar(), 'Yesterday at 11:59 PM',
                     'Yesterday at 11:59 PM, not Today, or the wrong time, tz = ' + z);
    }

    assert.equal(moment(b).utc().calendar(), 'Yesterday at 11:59 PM', 'Yesterday at 11:59 PM, not Today, or the wrong time');
    assert.equal(moment(c).local().calendar(), 'Yesterday at 11:59 PM', 'Yesterday at 11:59 PM, not Today, or the wrong time');
    assert.equal(moment(c).local().calendar(d), 'Tomorrow at 11:59 PM', 'Tomorrow at 11:59 PM, not Yesterday, or the wrong time');
});

test('invalid', function (assert) {
    assert.equal(moment.invalid().format(), 'Invalid date');
    assert.equal(moment.invalid().format('YYYY-MM-DD'), 'Invalid date');
});

test('quarter formats', function (assert) {
    assert.equal(moment([1985, 1,  4]).format('Q'), '1', 'Feb  4 1985 is Q1');
    assert.equal(moment([2029, 8, 18]).format('Q'), '3', 'Sep 18 2029 is Q3');
    assert.equal(moment([2013, 3, 24]).format('Q'), '2', 'Apr 24 2013 is Q2');
    assert.equal(moment([2015, 2,  5]).format('Q'), '1', 'Mar  5 2015 is Q1');
    assert.equal(moment([1970, 0,  2]).format('Q'), '1', 'Jan  2 1970 is Q1');
    assert.equal(moment([2001, 11, 12]).format('Q'), '4', 'Dec 12 2001 is Q4');
    assert.equal(moment([2000, 0,  2]).format('[Q]Q-YYYY'), 'Q1-2000', 'Jan  2 2000 is Q1');
});
//===================================================================================================================================================================================================================== //>
module('from_to');

test('from', function (assert) {
    var start = moment();
    moment.locale('en');
    assert.equal(start.from(start.clone().add(5, 'seconds')),  'a few seconds ago', '5 seconds = a few seconds ago');
    assert.equal(start.from(start.clone().add(1, 'minute')),  'a minute ago', '1 minute = a minute ago');
    assert.equal(start.from(start.clone().add(5, 'minutes')),  '5 minutes ago', '5 minutes = 5 minutes ago');

    assert.equal(start.from(start.clone().subtract(5, 'seconds')),  'in a few seconds', '5 seconds = in a few seconds');
    assert.equal(start.from(start.clone().subtract(1, 'minute')),  'in a minute', '1 minute = in a minute');
    assert.equal(start.from(start.clone().subtract(5, 'minutes')),  'in 5 minutes', '5 minutes = in 5 minutes');
});

test('from with absolute duration', function (assert) {
    var start = moment();
    moment.locale('en');
    assert.equal(start.from(start.clone().add(5, 'seconds'), true),  'a few seconds', '5 seconds = a few seconds');
    assert.equal(start.from(start.clone().add(1, 'minute'), true),  'a minute', '1 minute = a minute');
    assert.equal(start.from(start.clone().add(5, 'minutes'), true),  '5 minutes', '5 minutes = 5 minutes');

    assert.equal(start.from(start.clone().subtract(5, 'seconds'), true),  'a few seconds', '5 seconds = a few seconds');
    assert.equal(start.from(start.clone().subtract(1, 'minute'), true),  'a minute', '1 minute = a minute');
    assert.equal(start.from(start.clone().subtract(5, 'minutes'), true),  '5 minutes', '5 minutes = 5 minutes');
});

test('to', function (assert) {
    var start = moment();
    moment.locale('en');
    assert.equal(start.to(start.clone().subtract(5, 'seconds')),  'a few seconds ago', '5 seconds = a few seconds ago');
    assert.equal(start.to(start.clone().subtract(1, 'minute')),  'a minute ago', '1 minute = a minute ago');
    assert.equal(start.to(start.clone().subtract(5, 'minutes')),  '5 minutes ago', '5 minutes = 5 minutes ago');

    assert.equal(start.to(start.clone().add(5, 'seconds')),  'in a few seconds', '5 seconds = in a few seconds');
    assert.equal(start.to(start.clone().add(1, 'minute')),  'in a minute', '1 minute = in a minute');
    assert.equal(start.to(start.clone().add(5, 'minutes')),  'in 5 minutes', '5 minutes = in 5 minutes');
});

test('to with absolute duration', function (assert) {
    var start = moment();
    moment.locale('en');
    assert.equal(start.to(start.clone().add(5, 'seconds'), true),  'a few seconds', '5 seconds = a few seconds');
    assert.equal(start.to(start.clone().add(1, 'minute'), true),  'a minute', '1 minute = a minute');
    assert.equal(start.to(start.clone().add(5, 'minutes'), true),  '5 minutes', '5 minutes = 5 minutes');

    assert.equal(start.to(start.clone().subtract(5, 'seconds'), true),  'a few seconds', '5 seconds = a few seconds');
    assert.equal(start.to(start.clone().subtract(1, 'minute'), true),  'a minute', '1 minute = a minute');
    assert.equal(start.to(start.clone().subtract(5, 'minutes'), true),  '5 minutes', '5 minutes = 5 minutes');
});
//===================================================================================================================================================================================================================== //>
module('getters and setters');

test('getters', function (assert) {
    var a = moment([2011, 9, 12, 6, 7, 8, 9]);
    assert.equal(a.year(), 2011, 'year');
    assert.equal(a.month(), 9, 'month');
    assert.equal(a.date(), 12, 'date');
    assert.equal(a.day(), 3, 'day');
    assert.equal(a.hours(), 6, 'hour');
    assert.equal(a.minutes(), 7, 'minute');
    assert.equal(a.seconds(), 8, 'second');
    assert.equal(a.milliseconds(), 9, 'milliseconds');
});

test('getters programmatic', function (assert) {
    var a = moment([2011, 9, 12, 6, 7, 8, 9]);
    assert.equal(a.get('year'), 2011, 'year');
    assert.equal(a.get('month'), 9, 'month');
    assert.equal(a.get('date'), 12, 'date');
    assert.equal(a.get('day'), 3, 'day');
    assert.equal(a.get('hour'), 6, 'hour');
    assert.equal(a.get('minute'), 7, 'minute');
    assert.equal(a.get('second'), 8, 'second');
    assert.equal(a.get('milliseconds'), 9, 'milliseconds');

    //actual getters tested elsewhere
    assert.equal(a.get('weekday'), a.weekday(), 'weekday');
    assert.equal(a.get('isoWeekday'), a.isoWeekday(), 'isoWeekday');
    assert.equal(a.get('week'), a.week(), 'week');
    assert.equal(a.get('isoWeek'), a.isoWeek(), 'isoWeek');
    assert.equal(a.get('dayOfYear'), a.dayOfYear(), 'dayOfYear');
});

test('setters plural', function (assert) {
    var a = moment();
    a.years(2011);
    a.months(9);
    a.dates(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(9);
    assert.equal(a.years(), 2011, 'years');
    assert.equal(a.months(), 9, 'months');
    assert.equal(a.dates(), 12, 'dates');
    assert.equal(a.days(), 3, 'days');
    assert.equal(a.hours(), 6, 'hours');
    assert.equal(a.minutes(), 7, 'minutes');
    assert.equal(a.seconds(), 8, 'seconds');
    assert.equal(a.milliseconds(), 9, 'milliseconds');
});

test('setters singular', function (assert) {
    var a = moment();
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hour(6);
    a.minute(7);
    a.second(8);
    a.millisecond(9);
    assert.equal(a.year(), 2011, 'year');
    assert.equal(a.month(), 9, 'month');
    assert.equal(a.date(), 12, 'date');
    assert.equal(a.day(), 3, 'day');
    assert.equal(a.hour(), 6, 'hour');
    assert.equal(a.minute(), 7, 'minute');
    assert.equal(a.second(), 8, 'second');
    assert.equal(a.millisecond(), 9, 'milliseconds');
});

test('setters', function (assert) {
    var a = moment();
    a.year(2011);
    a.month(9);
    a.date(12);
    a.hours(6);
    a.minutes(7);
    a.seconds(8);
    a.milliseconds(9);
    assert.equal(a.year(), 2011, 'year');
    assert.equal(a.month(), 9, 'month');
    assert.equal(a.date(), 12, 'date');
    assert.equal(a.day(), 3, 'day');
    assert.equal(a.hours(), 6, 'hour');
    assert.equal(a.minutes(), 7, 'minute');
    assert.equal(a.seconds(), 8, 'second');
    assert.equal(a.milliseconds(), 9, 'milliseconds');

    // Test month() behavior. See https://github.com/timrwood/moment/pull/822
    a = moment('20130531', 'YYYYMMDD');
    a.month(3);
    assert.equal(a.month(), 3, 'month edge case');
});

test('setter programmatic', function (assert) {
    var a = moment();
    a.set('year', 2011);
    a.set('month', 9);
    a.set('date', 12);
    a.set('hours', 6);
    a.set('minutes', 7);
    a.set('seconds', 8);
    a.set('milliseconds', 9);
    assert.equal(a.year(), 2011, 'year');
    assert.equal(a.month(), 9, 'month');
    assert.equal(a.date(), 12, 'date');
    assert.equal(a.day(), 3, 'day');
    assert.equal(a.hours(), 6, 'hour');
    assert.equal(a.minutes(), 7, 'minute');
    assert.equal(a.seconds(), 8, 'second');
    assert.equal(a.milliseconds(), 9, 'milliseconds');

    // Test month() behavior. See https://github.com/timrwood/moment/pull/822
    a = moment('20130531', 'YYYYMMDD');
    a.month(3);
    assert.equal(a.month(), 3, 'month edge case');
});

// Disable this, until we weekYear setter is fixed.
// https://github.com/moment/moment/issues/1379
// test('setters programatic with weeks', function (assert) {
//     var a = moment();
//     a.set('weekYear', 2001);
//     a.set('week', 49);
//     a.set('day', 4);
//     assert.equals(a.weekYear(), 2001);
//     assert.equals(a.week(), 49);
//     assert.equals(a.day(), 4);

//     a.set('weekday', 1);
//     assert.equals(a.weekday(), 1);

//     assert.done();
//},

// I think this suffers from the same issue as the non-iso version.
// test('setters programatic with weeks ISO', function (assert) {
//     var a = moment();
//     a.set('isoWeekYear', 2001);
//     a.set('isoWeek', 49);
//     a.set('isoWeekday', 4);

//     assert.equals(a.weekYear(), 2001);
//     assert.equals(a.week(), 49);
//     assert.equals(a.day(), 4);

//     assert.done();
//},

test('setters strings', function (assert) {
    var a = moment([2012]).locale('en');
    assert.equal(a.clone().day(0).day('Wednesday').day(), 3, 'day full name');
    assert.equal(a.clone().day(0).day('Wed').day(), 3, 'day short name');
    assert.equal(a.clone().day(0).day('We').day(), 3, 'day minimal name');
    assert.equal(a.clone().day(0).day('invalid').day(), 0, 'invalid day name');
    assert.equal(a.clone().month(0).month('April').month(), 3, 'month full name');
    assert.equal(a.clone().month(0).month('Apr').month(), 3, 'month short name');
    assert.equal(a.clone().month(0).month('invalid').month(), 0, 'invalid month name');
});

test('setters - falsey values', function (assert) {
    var a = moment();
    // ensure minutes wasn't coincidentally 0 already
    a.minutes(1);
    a.minutes(0);
    assert.equal(a.minutes(), 0, 'falsey value');
});

test('chaining setters', function (assert) {
    var a = moment();
    a.year(2011)
     .month(9)
     .date(12)
     .hours(6)
     .minutes(7)
     .seconds(8);
    assert.equal(a.year(), 2011, 'year');
    assert.equal(a.month(), 9, 'month');
    assert.equal(a.date(), 12, 'date');
    assert.equal(a.day(), 3, 'day');
    assert.equal(a.hours(), 6, 'hour');
    assert.equal(a.minutes(), 7, 'minute');
    assert.equal(a.seconds(), 8, 'second');
});

test('setter with multiple unit values', function (assert) {
    var a = moment();
    a.set({
        year: 2011,
        month: 9,
        date: 12,
        hours: 6,
        minutes: 7,
        seconds: 8,
        milliseconds: 9
    });
    assert.equal(a.year(), 2011, 'year');
    assert.equal(a.month(), 9, 'month');
    assert.equal(a.date(), 12, 'date');
    assert.equal(a.day(), 3, 'day');
    assert.equal(a.hours(), 6, 'hour');
    assert.equal(a.minutes(), 7, 'minute');
    assert.equal(a.seconds(), 8, 'second');
    assert.equal(a.milliseconds(), 9, 'milliseconds');
});

test('day setter', function (assert) {
    var a = moment([2011, 0, 15]);
    assert.equal(moment(a).day(0).date(), 9, 'set from saturday to sunday');
    assert.equal(moment(a).day(6).date(), 15, 'set from saturday to saturday');
    assert.equal(moment(a).day(3).date(), 12, 'set from saturday to wednesday');

    a = moment([2011, 0, 9]);
    assert.equal(moment(a).day(0).date(), 9, 'set from sunday to sunday');
    assert.equal(moment(a).day(6).date(), 15, 'set from sunday to saturday');
    assert.equal(moment(a).day(3).date(), 12, 'set from sunday to wednesday');

    a = moment([2011, 0, 12]);
    assert.equal(moment(a).day(0).date(), 9, 'set from wednesday to sunday');
    assert.equal(moment(a).day(6).date(), 15, 'set from wednesday to saturday');
    assert.equal(moment(a).day(3).date(), 12, 'set from wednesday to wednesday');

    assert.equal(moment(a).day(-7).date(), 2, 'set from wednesday to last sunday');
    assert.equal(moment(a).day(-1).date(), 8, 'set from wednesday to last saturday');
    assert.equal(moment(a).day(-4).date(), 5, 'set from wednesday to last wednesday');

    assert.equal(moment(a).day(7).date(), 16, 'set from wednesday to next sunday');
    assert.equal(moment(a).day(13).date(), 22, 'set from wednesday to next saturday');
    assert.equal(moment(a).day(10).date(), 19, 'set from wednesday to next wednesday');

    assert.equal(moment(a).day(14).date(), 23, 'set from wednesday to second next sunday');
    assert.equal(moment(a).day(20).date(), 29, 'set from wednesday to second next saturday');
    assert.equal(moment(a).day(17).date(), 26, 'set from wednesday to second next wednesday');
});
//===================================================================================================================================================================================================================== //>
module('invalid');

test('invalid', function (assert) {
    var m = moment.invalid();
    assert.equal(m.isValid(), false);
    assert.equal(m.parsingFlags().userInvalidated, true);
    assert.ok(isNaN(m.valueOf()));
});

test('invalid with existing flag', function (assert) {
    var m = moment.invalid({invalidMonth : 'whatchamacallit'});
    assert.equal(m.isValid(), false);
    assert.equal(m.parsingFlags().userInvalidated, false);
    assert.equal(m.parsingFlags().invalidMonth, 'whatchamacallit');
    assert.ok(isNaN(m.valueOf()));
});

test('invalid with custom flag', function (assert) {
    var m = moment.invalid({tooBusyWith : 'reiculating splines'});
    assert.equal(m.isValid(), false);
    assert.equal(m.parsingFlags().userInvalidated, false);
    assert.equal(m.parsingFlags().tooBusyWith, 'reiculating splines');
    assert.ok(isNaN(m.valueOf()));
});
//===================================================================================================================================================================================================================== //>
module('is after');

test('is after without units', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 10)), mCopy = moment(m);
    assert.equal(m.isAfter(moment(new Date(2012, 3, 2, 3, 5, 5, 10))), false, 'year is later');
    assert.equal(m.isAfter(moment(new Date(2010, 3, 2, 3, 3, 5, 10))), true, 'year is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 4, 2, 3, 4, 5, 10))), false, 'month is later');
    assert.equal(m.isAfter(moment(new Date(2011, 2, 2, 3, 4, 5, 10))), true, 'month is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 3, 3, 4, 5, 10))), false, 'day is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 1, 3, 4, 5, 10))), true, 'day is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 4, 4, 5, 10))), false, 'hour is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 2, 4, 5, 10))), true, 'hour is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 5, 5, 10))), false, 'minute is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 3, 5, 10))), true, 'minute is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 6, 10))), false, 'second is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 4, 11))), true, 'second is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 5, 10))), false, 'millisecond match');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 5, 11))), false, 'millisecond is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 5, 9))), true, 'millisecond is earlier');
    assert.equal(m.isAfter(m), false, 'moments are not after themselves');
    assert.equal(+m, +mCopy, 'isAfter second should not change moment');
});

test('is after year', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isAfter(moment(new Date(2011, 5, 6, 7, 8, 9, 10)), 'year'), false, 'year match');
    assert.equal(m.isAfter(moment(new Date(2010, 5, 6, 7, 8, 9, 10)), 'years'), true, 'plural should work');
    assert.equal(m.isAfter(moment(new Date(2013, 5, 6, 7, 8, 9, 10)), 'year'), false, 'year is later');
    assert.equal(m.isAfter(moment(new Date(2010, 5, 6, 7, 8, 9, 10)), 'year'), true, 'year is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 0, 1, 0, 0, 0, 0)), 'year'), false, 'exact start of year');
    assert.equal(m.isAfter(moment(new Date(2011, 11, 31, 23, 59, 59, 999)), 'year'), false, 'exact end of year');
    assert.equal(m.isAfter(moment(new Date(2012, 0, 1, 0, 0, 0, 0)), 'year'), false, 'start of next year');
    assert.equal(m.isAfter(moment(new Date(2010, 11, 31, 23, 59, 59, 999)), 'year'), true, 'end of previous year');
    assert.equal(m.isAfter(moment(new Date(1980, 11, 31, 23, 59, 59, 999)), 'year'), true, 'end of year far before');
    assert.equal(m.isAfter(m, 'year'), false, 'same moments are not after the same year');
    assert.equal(+m, +mCopy, 'isAfter year should not change moment');
});

test('is after month', function (assert) {
    var m = moment(new Date(2011, 2, 3, 4, 5, 6, 7)), mCopy = moment(m);
    assert.equal(m.isAfter(moment(new Date(2011, 2, 6, 7, 8, 9, 10)), 'month'), false, 'month match');
    assert.equal(m.isAfter(moment(new Date(2010, 2, 6, 7, 8, 9, 10)), 'months'), true, 'plural should work');
    assert.equal(m.isAfter(moment(new Date(2012, 2, 6, 7, 8, 9, 10)), 'month'), false, 'year is later');
    assert.equal(m.isAfter(moment(new Date(2010, 2, 6, 7, 8, 9, 10)), 'month'), true, 'year is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 5, 6, 7, 8, 9, 10)), 'month'), false, 'month is later');
    assert.equal(m.isAfter(moment(new Date(2011, 1, 6, 7, 8, 9, 10)), 'month'), true, 'month is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 2, 1, 0, 0, 0, 0)), 'month'), false, 'exact start of month');
    assert.equal(m.isAfter(moment(new Date(2011, 2, 31, 23, 59, 59, 999)), 'month'), false, 'exact end of month');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 1, 0, 0, 0, 0)), 'month'), false, 'start of next month');
    assert.equal(m.isAfter(moment(new Date(2011, 1, 27, 23, 59, 59, 999)), 'month'), true, 'end of previous month');
    assert.equal(m.isAfter(moment(new Date(2010, 12, 31, 23, 59, 59, 999)), 'month'), true, 'later month but earlier year');
    assert.equal(m.isAfter(m, 'month'), false, 'same moments are not after the same month');
    assert.equal(+m, +mCopy, 'isAfter month should not change moment');
});

test('is after day', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 7, 8, 9, 10)), 'day'), false, 'day match');
    assert.equal(m.isAfter(moment(new Date(2010, 3, 2, 7, 8, 9, 10)), 'days'), true, 'plural should work');
    assert.equal(m.isAfter(moment(new Date(2012, 3, 2, 7, 8, 9, 10)), 'day'), false, 'year is later');
    assert.equal(m.isAfter(moment(new Date(2010, 3, 2, 7, 8, 9, 10)), 'day'), true, 'year is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 4, 2, 7, 8, 9, 10)), 'day'), false, 'month is later');
    assert.equal(m.isAfter(moment(new Date(2011, 2, 2, 7, 8, 9, 10)), 'day'), true, 'month is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 3, 7, 8, 9, 10)), 'day'), false, 'day is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 1, 7, 8, 9, 10)), 'day'), true, 'day is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 0, 0, 0, 0)), 'day'), false, 'exact start of day');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 23, 59, 59, 999)), 'day'), false, 'exact end of day');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 3, 0, 0, 0, 0)), 'day'), false, 'start of next day');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 1, 23, 59, 59, 999)), 'day'), true, 'end of previous day');
    assert.equal(m.isAfter(moment(new Date(2010, 3, 10, 0, 0, 0, 0)), 'day'), true, 'later day but earlier year');
    assert.equal(m.isAfter(m, 'day'), false, 'same moments are not after the same day');
    assert.equal(+m, +mCopy, 'isAfter day should not change moment');
});

test('is after hour', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 8, 9, 10)), 'hour'), false, 'hour match');
    assert.equal(m.isAfter(moment(new Date(2010, 3, 2, 3, 8, 9, 10)), 'hours'), true, 'plural should work');
    assert.equal(m.isAfter(moment(new Date(2012, 3, 2, 3, 8, 9, 10)), 'hour'), false, 'year is later');
    assert.equal(m.isAfter(moment(new Date(2010, 3, 2, 3, 8, 9, 10)), 'hour'), true, 'year is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 4, 2, 3, 8, 9, 10)), 'hour'), false, 'month is later');
    assert.equal(m.isAfter(moment(new Date(2011, 1, 2, 3, 8, 9, 10)), 'hour'), true, 'month is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 3, 3, 8, 9, 10)), 'hour'), false, 'day is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 1, 3, 8, 9, 10)), 'hour'), true, 'day is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 4, 8, 9, 10)), 'hour'), false, 'hour is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 8, 9, 10)), 'hour'), false, 'hour is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 0, 0, 0)), 'hour'), false, 'exact start of hour');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 59, 59, 999)), 'hour'), false, 'exact end of hour');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 4, 0, 0, 0)), 'hour'), false, 'start of next hour');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 2, 59, 59, 999)), 'hour'), true, 'end of previous hour');
    assert.equal(m.isAfter(m, 'hour'), false, 'same moments are not after the same hour');
    assert.equal(+m, +mCopy, 'isAfter hour should not change moment');
});

test('is after minute', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 9, 10)), 'minute'), false, 'minute match');
    assert.equal(m.isAfter(moment(new Date(2010, 3, 2, 3, 4, 9, 10)), 'minutes'), true, 'plural should work');
    assert.equal(m.isAfter(moment(new Date(2012, 3, 2, 3, 4, 9, 10)), 'minute'), false, 'year is later');
    assert.equal(m.isAfter(moment(new Date(2010, 3, 2, 3, 4, 9, 10)), 'minute'), true, 'year is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 4, 2, 3, 4, 9, 10)), 'minute'), false, 'month is later');
    assert.equal(m.isAfter(moment(new Date(2011, 2, 2, 3, 4, 9, 10)), 'minute'), true, 'month is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 3, 3, 4, 9, 10)), 'minute'), false, 'day is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 1, 3, 4, 9, 10)), 'minute'), true, 'day is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 4, 4, 9, 10)), 'minute'), false, 'hour is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 2, 4, 9, 10)), 'minute'), true, 'hour is earler');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 5, 9, 10)), 'minute'), false, 'minute is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 3, 9, 10)), 'minute'), true, 'minute is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 0, 0)), 'minute'), false, 'exact start of minute');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 59, 999)), 'minute'), false, 'exact end of minute');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 5, 0, 0)), 'minute'), false, 'start of next minute');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 3, 59, 999)), 'minute'), true, 'end of previous minute');
    assert.equal(m.isAfter(m, 'minute'), false, 'same moments are not after the same minute');
    assert.equal(+m, +mCopy, 'isAfter minute should not change moment');
});

test('is after second', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 10)), mCopy = moment(m);
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 5, 10)), 'second'), false, 'second match');
    assert.equal(m.isAfter(moment(new Date(2010, 3, 2, 3, 4, 5, 10)), 'seconds'), true, 'plural should work');
    assert.equal(m.isAfter(moment(new Date(2012, 3, 2, 3, 4, 5, 10)), 'second'), false, 'year is later');
    assert.equal(m.isAfter(moment(new Date(2010, 3, 2, 3, 4, 5, 10)), 'second'), true, 'year is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 4, 2, 3, 4, 5, 10)), 'second'), false, 'month is later');
    assert.equal(m.isAfter(moment(new Date(2011, 2, 2, 3, 4, 5, 10)), 'second'), true, 'month is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 3, 3, 4, 5, 10)), 'second'), false, 'day is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 1, 1, 4, 5, 10)), 'second'), true, 'day is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 4, 4, 5, 10)), 'second'), false, 'hour is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 1, 4, 1, 5, 10)), 'second'), true, 'hour is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 5, 5, 10)), 'second'), false, 'minute is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 3, 5, 10)), 'second'), true, 'minute is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 6, 10)), 'second'), false, 'second is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 4, 5)), 'second'), true, 'second is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 5, 0)), 'second'), false, 'exact start of second');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 5, 999)), 'second'), false, 'exact end of second');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 6, 0)), 'second'), false, 'start of next second');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 4, 999)), 'second'), true, 'end of previous second');
    assert.equal(m.isAfter(m, 'second'), false, 'same moments are not after the same second');
    assert.equal(+m, +mCopy, 'isAfter second should not change moment');
});

test('is after millisecond', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 10)), mCopy = moment(m);
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 5, 10)), 'millisecond'), false, 'millisecond match');
    assert.equal(m.isAfter(moment(new Date(2010, 3, 2, 3, 4, 5, 10)), 'milliseconds'), true, 'plural should work');
    assert.equal(m.isAfter(moment(new Date(2012, 3, 2, 3, 4, 5, 10)), 'millisecond'), false, 'year is later');
    assert.equal(m.isAfter(moment(new Date(2010, 3, 2, 3, 4, 5, 10)), 'millisecond'), true, 'year is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 4, 2, 3, 4, 5, 10)), 'millisecond'), false, 'month is later');
    assert.equal(m.isAfter(moment(new Date(2011, 2, 2, 3, 4, 5, 10)), 'millisecond'), true, 'month is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 3, 3, 4, 5, 10)), 'millisecond'), false, 'day is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 1, 1, 4, 5, 10)), 'millisecond'), true, 'day is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 4, 4, 5, 10)), 'millisecond'), false, 'hour is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 1, 4, 1, 5, 10)), 'millisecond'), true, 'hour is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 5, 5, 10)), 'millisecond'), false, 'minute is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 3, 5, 10)), 'millisecond'), true, 'minute is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 6, 10)), 'millisecond'), false, 'second is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 4, 5)), 'millisecond'), true, 'second is earlier');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 6, 11)), 'millisecond'), false, 'millisecond is later');
    assert.equal(m.isAfter(moment(new Date(2011, 3, 2, 3, 4, 4, 9)), 'millisecond'), true, 'millisecond is earlier');
    assert.equal(m.isAfter(m, 'millisecond'), false, 'same moments are not after the same millisecond');
    assert.equal(+m, +mCopy, 'isAfter millisecond should not change moment');
});
//===================================================================================================================================================================================================================== //>
module('is before');

test('is after without units', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 10)), mCopy = moment(m);
    assert.equal(m.isBefore(moment(new Date(2012, 3, 2, 3, 5, 5, 10))), true, 'year is later');
    assert.equal(m.isBefore(moment(new Date(2010, 3, 2, 3, 3, 5, 10))), false, 'year is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 4, 2, 3, 4, 5, 10))), true, 'month is later');
    assert.equal(m.isBefore(moment(new Date(2011, 2, 2, 3, 4, 5, 10))), false, 'month is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 3, 3, 4, 5, 10))), true, 'day is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 1, 3, 4, 5, 10))), false, 'day is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 4, 4, 5, 10))), true, 'hour is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 2, 4, 5, 10))), false, 'hour is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 5, 5, 10))), true, 'minute is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 3, 5, 10))), false, 'minute is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 6, 10))), true, 'second is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 4, 11))), false, 'second is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 5, 10))), false, 'millisecond match');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 5, 11))), true, 'millisecond is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 5, 9))), false, 'millisecond is earlier');
    assert.equal(m.isBefore(m), false, 'moments are not before themselves');
    assert.equal(+m, +mCopy, 'isBefore second should not change moment');
});

test('is before year', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isBefore(moment(new Date(2011, 5, 6, 7, 8, 9, 10)), 'year'), false, 'year match');
    assert.equal(m.isBefore(moment(new Date(2012, 5, 6, 7, 8, 9, 10)), 'years'), true, 'plural should work');
    assert.equal(m.isBefore(moment(new Date(2013, 5, 6, 7, 8, 9, 10)), 'year'), true, 'year is later');
    assert.equal(m.isBefore(moment(new Date(2010, 5, 6, 7, 8, 9, 10)), 'year'), false, 'year is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 0, 1, 0, 0, 0, 0)), 'year'), false, 'exact start of year');
    assert.equal(m.isBefore(moment(new Date(2011, 11, 31, 23, 59, 59, 999)), 'year'), false, 'exact end of year');
    assert.equal(m.isBefore(moment(new Date(2012, 0, 1, 0, 0, 0, 0)), 'year'), true, 'start of next year');
    assert.equal(m.isBefore(moment(new Date(2010, 11, 31, 23, 59, 59, 999)), 'year'), false, 'end of previous year');
    assert.equal(m.isBefore(moment(new Date(1980, 11, 31, 23, 59, 59, 999)), 'year'), false, 'end of year far before');
    assert.equal(m.isBefore(m, 'year'), false, 'same moments are not before the same year');
    assert.equal(+m, +mCopy, 'isBefore year should not change moment');
});

test('is before month', function (assert) {
    var m = moment(new Date(2011, 2, 3, 4, 5, 6, 7)), mCopy = moment(m);
    assert.equal(m.isBefore(moment(new Date(2011, 2, 6, 7, 8, 9, 10)), 'month'), false, 'month match');
    assert.equal(m.isBefore(moment(new Date(2012, 2, 6, 7, 8, 9, 10)), 'months'), true, 'plural should work');
    assert.equal(m.isBefore(moment(new Date(2012, 2, 6, 7, 8, 9, 10)), 'month'), true, 'year is later');
    assert.equal(m.isBefore(moment(new Date(2010, 2, 6, 7, 8, 9, 10)), 'month'), false, 'year is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 5, 6, 7, 8, 9, 10)), 'month'), true, 'month is later');
    assert.equal(m.isBefore(moment(new Date(2011, 1, 6, 7, 8, 9, 10)), 'month'), false, 'month is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 2, 1, 0, 0, 0, 0)), 'month'), false, 'exact start of month');
    assert.equal(m.isBefore(moment(new Date(2011, 2, 31, 23, 59, 59, 999)), 'month'), false, 'exact end of month');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 1, 0, 0, 0, 0)), 'month'), true, 'start of next month');
    assert.equal(m.isBefore(moment(new Date(2011, 1, 27, 23, 59, 59, 999)), 'month'), false, 'end of previous month');
    assert.equal(m.isBefore(moment(new Date(2010, 12, 31, 23, 59, 59, 999)), 'month'), false, 'later month but earlier year');
    assert.equal(m.isBefore(m, 'month'), false, 'same moments are not before the same month');
    assert.equal(+m, +mCopy, 'isBefore month should not change moment');
});

test('is before day', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 7, 8, 9, 10)), 'day'), false, 'day match');
    assert.equal(m.isBefore(moment(new Date(2012, 3, 2, 7, 8, 9, 10)), 'days'), true, 'plural should work');
    assert.equal(m.isBefore(moment(new Date(2012, 3, 2, 7, 8, 9, 10)), 'day'), true, 'year is later');
    assert.equal(m.isBefore(moment(new Date(2010, 3, 2, 7, 8, 9, 10)), 'day'), false, 'year is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 4, 2, 7, 8, 9, 10)), 'day'), true, 'month is later');
    assert.equal(m.isBefore(moment(new Date(2011, 2, 2, 7, 8, 9, 10)), 'day'), false, 'month is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 3, 7, 8, 9, 10)), 'day'), true, 'day is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 1, 7, 8, 9, 10)), 'day'), false, 'day is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 0, 0, 0, 0)), 'day'), false, 'exact start of day');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 23, 59, 59, 999)), 'day'), false, 'exact end of day');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 3, 0, 0, 0, 0)), 'day'), true, 'start of next day');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 1, 23, 59, 59, 999)), 'day'), false, 'end of previous day');
    assert.equal(m.isBefore(moment(new Date(2010, 3, 10, 0, 0, 0, 0)), 'day'), false, 'later day but earlier year');
    assert.equal(m.isBefore(m, 'day'), false, 'same moments are not before the same day');
    assert.equal(+m, +mCopy, 'isBefore day should not change moment');
});

test('is before hour', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 8, 9, 10)), 'hour'), false, 'hour match');
    assert.equal(m.isBefore(moment(new Date(2012, 3, 2, 3, 8, 9, 10)), 'hours'), true, 'plural should work');
    assert.equal(m.isBefore(moment(new Date(2012, 3, 2, 3, 8, 9, 10)), 'hour'), true, 'year is later');
    assert.equal(m.isBefore(moment(new Date(2010, 3, 2, 3, 8, 9, 10)), 'hour'), false, 'year is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 4, 2, 3, 8, 9, 10)), 'hour'), true, 'month is later');
    assert.equal(m.isBefore(moment(new Date(2011, 1, 2, 3, 8, 9, 10)), 'hour'), false, 'month is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 3, 3, 8, 9, 10)), 'hour'), true, 'day is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 1, 3, 8, 9, 10)), 'hour'), false, 'day is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 4, 8, 9, 10)), 'hour'), true, 'hour is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 8, 9, 10)), 'hour'), false, 'hour is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 0, 0, 0)), 'hour'), false, 'exact start of hour');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 59, 59, 999)), 'hour'), false, 'exact end of hour');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 4, 0, 0, 0)), 'hour'), true, 'start of next hour');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 2, 59, 59, 999)), 'hour'), false, 'end of previous hour');
    assert.equal(m.isBefore(m, 'hour'), false, 'same moments are not before the same hour');
    assert.equal(+m, +mCopy, 'isBefore hour should not change moment');
});

test('is before minute', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 9, 10)), 'minute'), false, 'minute match');
    assert.equal(m.isBefore(moment(new Date(2012, 3, 2, 3, 4, 9, 10)), 'minutes'), true, 'plural should work');
    assert.equal(m.isBefore(moment(new Date(2012, 3, 2, 3, 4, 9, 10)), 'minute'), true, 'year is later');
    assert.equal(m.isBefore(moment(new Date(2010, 3, 2, 3, 4, 9, 10)), 'minute'), false, 'year is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 4, 2, 3, 4, 9, 10)), 'minute'), true, 'month is later');
    assert.equal(m.isBefore(moment(new Date(2011, 2, 2, 3, 4, 9, 10)), 'minute'), false, 'month is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 3, 3, 4, 9, 10)), 'minute'), true, 'day is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 1, 3, 4, 9, 10)), 'minute'), false, 'day is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 4, 4, 9, 10)), 'minute'), true, 'hour is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 2, 4, 9, 10)), 'minute'), false, 'hour is earler');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 5, 9, 10)), 'minute'), true, 'minute is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 3, 9, 10)), 'minute'), false, 'minute is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 0, 0)), 'minute'), false, 'exact start of minute');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 59, 999)), 'minute'), false, 'exact end of minute');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 5, 0, 0)), 'minute'), true, 'start of next minute');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 3, 59, 999)), 'minute'), false, 'end of previous minute');
    assert.equal(m.isBefore(m, 'minute'), false, 'same moments are not before the same minute');
    assert.equal(+m, +mCopy, 'isBefore minute should not change moment');
});

test('is before second', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 10)), mCopy = moment(m);
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 5, 10)), 'second'), false, 'second match');
    assert.equal(m.isBefore(moment(new Date(2012, 3, 2, 3, 4, 5, 10)), 'seconds'), true, 'plural should work');
    assert.equal(m.isBefore(moment(new Date(2012, 3, 2, 3, 4, 5, 10)), 'second'), true, 'year is later');
    assert.equal(m.isBefore(moment(new Date(2010, 3, 2, 3, 4, 5, 10)), 'second'), false, 'year is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 4, 2, 3, 4, 5, 10)), 'second'), true, 'month is later');
    assert.equal(m.isBefore(moment(new Date(2011, 2, 2, 3, 4, 5, 10)), 'second'), false, 'month is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 3, 3, 4, 5, 10)), 'second'), true, 'day is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 1, 1, 4, 5, 10)), 'second'), false, 'day is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 4, 4, 5, 10)), 'second'), true, 'hour is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 1, 4, 1, 5, 10)), 'second'), false, 'hour is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 5, 5, 10)), 'second'), true, 'minute is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 3, 5, 10)), 'second'), false, 'minute is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 6, 10)), 'second'), true, 'second is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 4, 5)), 'second'), false, 'second is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 5, 0)), 'second'), false, 'exact start of second');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 5, 999)), 'second'), false, 'exact end of second');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 6, 0)), 'second'), true, 'start of next second');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 4, 999)), 'second'), false, 'end of previous second');
    assert.equal(m.isBefore(m, 'second'), false, 'same moments are not before the same second');
    assert.equal(+m, +mCopy, 'isBefore second should not change moment');
});

test('is before millisecond', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 10)), mCopy = moment(m);
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 5, 10)), 'millisecond'), false, 'millisecond match');
    assert.equal(m.isBefore(moment(new Date(2010, 3, 2, 3, 4, 5, 10)), 'milliseconds'), false, 'plural should work');
    assert.equal(m.isBefore(moment(new Date(2012, 3, 2, 3, 4, 5, 10)), 'millisecond'), true, 'year is later');
    assert.equal(m.isBefore(moment(new Date(2010, 3, 2, 3, 4, 5, 10)), 'millisecond'), false, 'year is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 4, 2, 3, 4, 5, 10)), 'millisecond'), true, 'month is later');
    assert.equal(m.isBefore(moment(new Date(2011, 2, 2, 3, 4, 5, 10)), 'millisecond'), false, 'month is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 3, 3, 4, 5, 10)), 'millisecond'), true, 'day is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 1, 1, 4, 5, 10)), 'millisecond'), false, 'day is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 4, 4, 5, 10)), 'millisecond'), true, 'hour is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 1, 4, 1, 5, 10)), 'millisecond'), false, 'hour is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 5, 5, 10)), 'millisecond'), true, 'minute is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 3, 5, 10)), 'millisecond'), false, 'minute is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 6, 10)), 'millisecond'), true, 'second is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 4, 5)), 'millisecond'), false, 'second is earlier');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 6, 11)), 'millisecond'), true, 'millisecond is later');
    assert.equal(m.isBefore(moment(new Date(2011, 3, 2, 3, 4, 4, 9)), 'millisecond'), false, 'millisecond is earlier');
    assert.equal(m.isBefore(m, 'millisecond'), false, 'same moments are not before the same millisecond');
    assert.equal(+m, +mCopy, 'isBefore millisecond should not change moment');
});
//===================================================================================================================================================================================================================== //>
module('is between');

test('is between without units', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 10)), mCopy = moment(m);
    assert.equal(m.isBetween(
                moment(new Date(2009, 3, 2, 3, 4, 5, 10)),
                moment(new Date(2011, 3, 2, 3, 4, 5, 10))), false, 'year is later');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 4, 5, 10)),
                moment(new Date(2013, 3, 2, 3, 4, 5, 10))), false, 'year is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2010, 3, 2, 3, 4, 5, 10)),
                moment(new Date(2012, 3, 2, 3, 4, 5, 10))), true, 'year is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 4, 5, 10)),
                moment(new Date(2011, 3, 2, 3, 4, 5, 10))), false, 'month is later');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 4, 5, 10)),
                moment(new Date(2011, 5, 2, 3, 4, 5, 10))), false, 'month is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2011, 2, 2, 3, 4, 5, 10)),
                moment(new Date(2011, 4, 2, 3, 4, 5, 10))), true, 'month is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 1, 3, 4, 5, 10)),
                moment(new Date(2011, 3, 2, 3, 4, 5, 10))), false, 'day is later');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 4, 5, 10)),
                moment(new Date(2011, 3, 4, 3, 4, 5, 10))), false, 'day is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 1, 3, 4, 5, 10)),
                moment(new Date(2011, 3, 3, 3, 4, 5, 10))), true, 'day is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 1, 4, 5, 10)),
                moment(new Date(2011, 3, 2, 3, 4, 5, 10))), false, 'hour is later');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 4, 5, 10)),
                moment(new Date(2011, 3, 2, 5, 4, 5, 10))), false, 'hour is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 2, 4, 5, 10)),
                moment(new Date(2011, 3, 2, 4, 4, 5, 10))), true, 'hour is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 4, 5, 10)),
                moment(new Date(2011, 3, 2, 3, 6, 5, 10))), false, 'minute is later');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 2, 5, 10)),
                moment(new Date(2011, 3, 2, 3, 4, 5, 10))), false, 'minute is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 3, 5, 10)),
                moment(new Date(2011, 3, 2, 3, 5, 5, 10))), true, 'minute is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 4, 5, 10)),
                moment(new Date(2011, 3, 2, 3, 4, 7, 10))), false, 'second is later');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 4, 3, 10)),
                moment(new Date(2011, 3, 2, 3, 4, 5, 10))), false, 'second is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 4, 4, 10)),
                moment(new Date(2011, 3, 2, 3, 4, 6, 10))), true, 'second is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 4, 5, 10)),
                moment(new Date(2011, 3, 2, 3, 4, 5, 12))), false, 'millisecond is later');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 4, 5, 8)),
                moment(new Date(2011, 3, 2, 3, 4, 5, 10))), false, 'millisecond is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2011, 3, 2, 3, 4, 5, 9)),
                moment(new Date(2011, 3, 2, 3, 4, 5, 11))), true, 'millisecond is between');
    assert.equal(m.isBetween(m, m), false, 'moments are not between themselves');
    assert.equal(+m, +mCopy, 'isBetween second should not change moment');
});

test('is between year', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isBetween(
                moment(new Date(2011, 5, 6, 7, 8, 9, 10)),
                moment(new Date(2011, 5, 6, 7, 8, 9, 10)), 'year'), false, 'year match');
    assert.equal(m.isBetween(
                moment(new Date(2010, 5, 6, 7, 8, 9, 10)),
                moment(new Date(2012, 5, 6, 7, 8, 9, 10)), 'years'), true, 'plural should work');
    assert.equal(m.isBetween(
                moment(new Date(2010, 5, 6, 7, 8, 9, 10)),
                moment(new Date(2012, 5, 6, 7, 8, 9, 10)), 'year'), true, 'year is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 5, 6, 7, 8, 9, 10)),
                moment(new Date(2013, 5, 6, 7, 8, 9, 10)), 'year'), false, 'year is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2010, 5, 6, 7, 8, 9, 10)),
                moment(new Date(2011, 5, 6, 7, 8, 9, 10)), 'year'), false, 'year is later');
    assert.equal(m.isBetween(m, 'year'), false, 'same moments are not between the same year');
    assert.equal(+m, +mCopy, 'isBetween year should not change moment');
});

test('is between month', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 6, 7, 8, 9, 10)),
                moment(new Date(2011, 1, 6, 7, 8, 9, 10)), 'month'), false, 'month match');
    assert.equal(m.isBetween(
                moment(new Date(2011, 0, 6, 7, 8, 9, 10)),
                moment(new Date(2011, 2, 6, 7, 8, 9, 10)), 'months'), true, 'plural should work');
    assert.equal(m.isBetween(
                moment(new Date(2011, 0, 31, 23, 59, 59, 999)),
                moment(new Date(2011, 2, 1, 0, 0, 0, 0)), 'month'), true, 'month is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 6, 7, 8, 9, 10)),
                moment(new Date(2011, 2, 6, 7, 8, 9, 10)), 'month'), false, 'month is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2011, 11, 6, 7, 8, 9, 10)),
                moment(new Date(2011, 1, 6, 7, 8, 9, 10)), 'month'), false, 'month is later');
    assert.equal(m.isBetween(m, 'month'), false, 'same moments are not between the same month');
    assert.equal(+m, +mCopy, 'isBetween month should not change moment');
});

test('is between day', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 7, 8, 9, 10)),
                moment(new Date(2011, 1, 2, 7, 8, 9, 10)), 'day'), false, 'day match');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 1, 7, 8, 9, 10)),
                moment(new Date(2011, 1, 3, 7, 8, 9, 10)), 'days'), true, 'plural should work');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 1, 7, 8, 9, 10)),
                moment(new Date(2011, 1, 3, 7, 8, 9, 10)), 'day'), true, 'day is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 7, 8, 9, 10)),
                moment(new Date(2011, 1, 4, 7, 8, 9, 10)), 'day'), false, 'day is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 1, 7, 8, 9, 10)),
                moment(new Date(2011, 1, 2, 7, 8, 9, 10)), 'day'), false, 'day is later');
    assert.equal(m.isBetween(m, 'day'), false, 'same moments are not between the same day');
    assert.equal(+m, +mCopy, 'isBetween day should not change moment');
});

test('is between hour', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 5, 9, 10)),
                moment(new Date(2011, 1, 2, 3, 9, 9, 10)), 'hour'), false, 'hour match');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 1, 59, 59, 999)),
                moment(new Date(2011, 1, 2, 4, 0, 0, 0)), 'hours'), true, 'plural should work');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 2, 59, 59, 999)),
                moment(new Date(2011, 1, 2, 4, 0, 0, 0)), 'hour'), true, 'hour is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 7, 8, 9, 10)),
                moment(new Date(2011, 1, 2, 7, 8, 9, 10)), 'hour'), false, 'hour is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 7, 8, 9, 10)),
                moment(new Date(2011, 1, 2, 7, 8, 9, 10)), 'hour'), false, 'hour is later');
    assert.equal(m.isBetween(m, 'hour'), false, 'same moments are not between the same hour');
    assert.equal(+m, +mCopy, 'isBetween hour should not change moment');
});

test('is between minute', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 4, 9, 10)),
                moment(new Date(2011, 1, 2, 3, 4, 9, 10)), 'minute'), false, 'minute match');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 3, 9, 10)),
                moment(new Date(2011, 1, 2, 3, 5, 9, 10)), 'minutes'), true, 'plural should work');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 3, 59, 999)),
                moment(new Date(2011, 1, 2, 3, 5, 0, 0)), 'minute'), true, 'minute is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 5, 0, 0)),
                moment(new Date(2011, 1, 2, 3, 8, 9, 10)), 'minute'), false, 'minute is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 2, 9, 10)),
                moment(new Date(2011, 1, 2, 3, 3, 59, 999)), 'minute'), false, 'minute is later');
    assert.equal(m.isBetween(m, 'minute'), false, 'same moments are not between the same minute');
    assert.equal(+m, +mCopy, 'isBetween minute should not change moment');
});

test('is between second', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 4, 5, 10)),
                moment(new Date(2011, 1, 2, 3, 4, 5, 10)), 'second'), false, 'second match');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 4, 4, 10)),
                moment(new Date(2011, 1, 2, 3, 4, 6, 10)), 'seconds'), true, 'plural should work');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 4, 4, 999)),
                moment(new Date(2011, 1, 2, 3, 4, 6, 0)), 'second'), true, 'second is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 4, 6, 0)),
                moment(new Date(2011, 1, 2, 3, 4, 7, 10)), 'second'), false, 'second is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 4, 3, 10)),
                moment(new Date(2011, 1, 2, 3, 4, 4, 999)), 'second'), false, 'second is later');
    assert.equal(m.isBetween(m, 'second'), false, 'same moments are not between the same second');
    assert.equal(+m, +mCopy, 'isBetween second should not change moment');
});

test('is between millisecond', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 4, 5, 6)),
                moment(new Date(2011, 1, 2, 3, 4, 5, 6)), 'millisecond'), false, 'millisecond match');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 4, 5, 5)),
                moment(new Date(2011, 1, 2, 3, 4, 5, 7)), 'milliseconds'), true, 'plural should work');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 4, 5, 5)),
                moment(new Date(2011, 1, 2, 3, 4, 5, 7)), 'millisecond'), true, 'millisecond is between');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 4, 5, 7)),
                moment(new Date(2011, 1, 2, 3, 4, 5, 10)), 'millisecond'), false, 'millisecond is earlier');
    assert.equal(m.isBetween(
                moment(new Date(2011, 1, 2, 3, 4, 5, 4)),
                moment(new Date(2011, 1, 2, 3, 4, 5, 6)), 'millisecond'), false, 'millisecond is later');
    assert.equal(m.isBetween(m, 'millisecond'), false, 'same moments are not between the same millisecond');
    assert.equal(+m, +mCopy, 'isBetween millisecond should not change moment');
});
//===================================================================================================================================================================================================================== //>
module('is date');

test('isDate recognizes Date objects', function (assert) {
    assert.ok(moment.isDate(new Date()), 'no args (now)');
    assert.ok(moment.isDate(new Date([2014, 2, 15])), 'array args');
    assert.ok(moment.isDate(new Date('2014-03-15')), 'string args');
    assert.ok(moment.isDate(new Date('does NOT look like a date')), 'invalid date');
});

test('isDate rejects non-Date objects', function (assert) {
    assert.ok(!moment.isDate(), 'nothing');
    assert.ok(!moment.isDate(undefined), 'undefined');
    assert.ok(!moment.isDate(null), 'string args');
    assert.ok(!moment.isDate(42), 'number');
    assert.ok(!moment.isDate('2014-03-15'), 'string');
    assert.ok(!moment.isDate([2014, 2, 15]), 'array');
    assert.ok(!moment.isDate({year: 2014, month: 2, day: 15}), 'object');
    assert.ok(!moment.isDate({toString: function () {
        return '[object Date]';
    }}), 'lying object');
});
//===================================================================================================================================================================================================================== //>
module('is moment');

test('is moment object', function (assert) {
    var MyObj = function () {},
        extend = function (a, b) {
            var i;
            for (i in b) {
                a[i] = b[i];
            }
            return a;
        };
    MyObj.prototype.toDate = function () {
        return new Date();
    };

    assert.ok(moment.isMoment(moment()), 'simple moment object');
    assert.ok(moment.isMoment(moment(null)), 'invalid moment object');
    assert.ok(moment.isMoment(extend({}, moment())), 'externally cloned moments are moments');
    assert.ok(moment.isMoment(extend({}, moment.utc())), 'externally cloned utc moments are moments');

    assert.ok(!moment.isMoment(new MyObj()), 'myObj is not moment object');
    assert.ok(!moment.isMoment(moment), 'moment function is not moment object');
    assert.ok(!moment.isMoment(new Date()), 'date object is not moment object');
    assert.ok(!moment.isMoment(Object), 'Object is not moment object');
    assert.ok(!moment.isMoment('foo'), 'string is not moment object');
    assert.ok(!moment.isMoment(1), 'number is not moment object');
    assert.ok(!moment.isMoment(NaN), 'NaN is not moment object');
    assert.ok(!moment.isMoment(null), 'null is not moment object');
    assert.ok(!moment.isMoment(undefined), 'undefined is not moment object');
});

test('is moment with hacked hasOwnProperty', function (assert) {
    var obj = {};
    // HACK to suppress jshint warning about bad property name
    obj['hasOwnMoney'.replace('Money', 'Property')] = function () {
        return true;
    };

    assert.ok(!moment.isMoment(obj), 'isMoment works even if passed object has a wrong hasOwnProperty implementation (ie8)');
});
//===================================================================================================================================================================================================================== //>
module('is same');

test('is same without units', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 10)), mCopy = moment(m);
    assert.equal(m.isSame(moment(new Date(2012, 3, 2, 3, 5, 5, 10))), false, 'year is later');
    assert.equal(m.isSame(moment(new Date(2010, 3, 2, 3, 3, 5, 10))), false, 'year is earlier');
    assert.equal(m.isSame(moment(new Date(2011, 4, 2, 3, 4, 5, 10))), false, 'month is later');
    assert.equal(m.isSame(moment(new Date(2011, 2, 2, 3, 4, 5, 10))), false, 'month is earlier');
    assert.equal(m.isSame(moment(new Date(2011, 3, 3, 3, 4, 5, 10))), false, 'day is later');
    assert.equal(m.isSame(moment(new Date(2011, 3, 1, 3, 4, 5, 10))), false, 'day is earlier');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 4, 4, 5, 10))), false, 'hour is later');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 2, 4, 5, 10))), false, 'hour is earlier');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 5, 5, 10))), false, 'minute is later');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 3, 5, 10))), false, 'minute is earlier');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 4, 6, 10))), false, 'second is later');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 4, 4, 11))), false, 'second is earlier');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 4, 5, 10))), true, 'millisecond match');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 4, 5, 11))), false, 'millisecond is later');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 4, 5, 9))), false, 'millisecond is earlier');
    assert.equal(m.isSame(m), true, 'moments are the same as themselves');
    assert.equal(+m, +mCopy, 'isSame second should not change moment');
});

test('is same year', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isSame(moment(new Date(2011, 5, 6, 7, 8, 9, 10)), 'year'), true, 'year match');
    assert.equal(m.isSame(moment(new Date(2011, 5, 6, 7, 8, 9, 10)), 'years'), true, 'plural should work');
    assert.equal(m.isSame(moment(new Date(2012, 5, 6, 7, 8, 9, 10)), 'year'), false, 'year mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 0, 1, 0, 0, 0, 0)), 'year'), true, 'exact start of year');
    assert.equal(m.isSame(moment(new Date(2011, 11, 31, 23, 59, 59, 999)), 'year'), true, 'exact end of year');
    assert.equal(m.isSame(moment(new Date(2012, 0, 1, 0, 0, 0, 0)), 'year'), false, 'start of next year');
    assert.equal(m.isSame(moment(new Date(2010, 11, 31, 23, 59, 59, 999)), 'year'), false, 'end of previous year');
    assert.equal(m.isSame(m, 'year'), true, 'same moments are in the same year');
    assert.equal(+m, +mCopy, 'isSame year should not change moment');
});

test('is same month', function (assert) {
    var m = moment(new Date(2011, 2, 3, 4, 5, 6, 7)), mCopy = moment(m);
    assert.equal(m.isSame(moment(new Date(2011, 2, 6, 7, 8, 9, 10)), 'month'), true, 'month match');
    assert.equal(m.isSame(moment(new Date(2011, 2, 6, 7, 8, 9, 10)), 'months'), true, 'plural should work');
    assert.equal(m.isSame(moment(new Date(2012, 2, 6, 7, 8, 9, 10)), 'month'), false, 'year mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 5, 6, 7, 8, 9, 10)), 'month'), false, 'month mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 2, 1, 0, 0, 0, 0)), 'month'), true, 'exact start of month');
    assert.equal(m.isSame(moment(new Date(2011, 2, 31, 23, 59, 59, 999)), 'month'), true, 'exact end of month');
    assert.equal(m.isSame(moment(new Date(2011, 3, 1, 0, 0, 0, 0)), 'month'), false, 'start of next month');
    assert.equal(m.isSame(moment(new Date(2011, 1, 27, 23, 59, 59, 999)), 'month'), false, 'end of previous month');
    assert.equal(m.isSame(m, 'month'), true, 'same moments are in the same month');
    assert.equal(+m, +mCopy, 'isSame month should not change moment');
});

test('is same day', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 7, 8, 9, 10)), 'day'), true, 'day match');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 7, 8, 9, 10)), 'days'), true, 'plural should work');
    assert.equal(m.isSame(moment(new Date(2012, 1, 2, 7, 8, 9, 10)), 'day'), false, 'year mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 2, 2, 7, 8, 9, 10)), 'day'), false, 'month mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 3, 7, 8, 9, 10)), 'day'), false, 'day mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 0, 0, 0, 0)), 'day'), true, 'exact start of day');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 23, 59, 59, 999)), 'day'), true, 'exact end of day');
    assert.equal(m.isSame(moment(new Date(2011, 1, 3, 0, 0, 0, 0)), 'day'), false, 'start of next day');
    assert.equal(m.isSame(moment(new Date(2011, 1, 1, 23, 59, 59, 999)), 'day'), false, 'end of previous day');
    assert.equal(m.isSame(m, 'day'), true, 'same moments are in the same day');
    assert.equal(+m, +mCopy, 'isSame day should not change moment');
});

test('is same hour', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 8, 9, 10)), 'hour'), true, 'hour match');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 8, 9, 10)), 'hours'), true, 'plural should work');
    assert.equal(m.isSame(moment(new Date(2012, 1, 2, 3, 8, 9, 10)), 'hour'), false, 'year mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 2, 2, 3, 8, 9, 10)), 'hour'), false, 'month mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 3, 3, 8, 9, 10)), 'hour'), false, 'day mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 4, 8, 9, 10)), 'hour'), false, 'hour mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 0, 0, 0)), 'hour'), true, 'exact start of hour');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 59, 59, 999)), 'hour'), true, 'exact end of hour');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 4, 0, 0, 0)), 'hour'), false, 'start of next hour');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 2, 59, 59, 999)), 'hour'), false, 'end of previous hour');
    assert.equal(m.isSame(m, 'hour'), true, 'same moments are in the same hour');
    assert.equal(+m, +mCopy, 'isSame hour should not change moment');
});

test('is same minute', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 4, 9, 10)), 'minute'), true, 'minute match');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 4, 9, 10)), 'minutes'), true, 'plural should work');
    assert.equal(m.isSame(moment(new Date(2012, 1, 2, 3, 4, 9, 10)), 'minute'), false, 'year mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 2, 2, 3, 4, 9, 10)), 'minute'), false, 'month mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 3, 3, 4, 9, 10)), 'minute'), false, 'day mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 4, 4, 9, 10)), 'minute'), false, 'hour mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 5, 9, 10)), 'minute'), false, 'minute mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 4, 0, 0)), 'minute'), true, 'exact start of minute');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 4, 59, 999)), 'minute'), true, 'exact end of minute');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 5, 0, 0)), 'minute'), false, 'start of next minute');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 3, 59, 999)), 'minute'), false, 'end of previous minute');
    assert.equal(m.isSame(m, 'minute'), true, 'same moments are in the same minute');
    assert.equal(+m, +mCopy, 'isSame minute should not change moment');
});

test('is same second', function (assert) {
    var m = moment(new Date(2011, 1, 2, 3, 4, 5, 6)), mCopy = moment(m);
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 4, 5, 10)), 'second'), true, 'second match');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 4, 5, 10)), 'seconds'), true, 'plural should work');
    assert.equal(m.isSame(moment(new Date(2012, 1, 2, 3, 4, 5, 10)), 'second'), false, 'year mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 2, 2, 3, 4, 5, 10)), 'second'), false, 'month mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 3, 3, 4, 5, 10)), 'second'), false, 'day mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 4, 4, 5, 10)), 'second'), false, 'hour mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 5, 5, 10)), 'second'), false, 'minute mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 4, 6, 10)), 'second'), false, 'second mismatch');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 4, 5, 0)), 'second'), true, 'exact start of second');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 4, 5, 999)), 'second'), true, 'exact end of second');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 4, 6, 0)), 'second'), false, 'start of next second');
    assert.equal(m.isSame(moment(new Date(2011, 1, 2, 3, 4, 4, 999)), 'second'), false, 'end of previous second');
    assert.equal(m.isSame(m, 'second'), true, 'same moments are in the same second');
    assert.equal(+m, +mCopy, 'isSame second should not change moment');
});

test('is same millisecond', function (assert) {
    var m = moment(new Date(2011, 3, 2, 3, 4, 5, 10)), mCopy = moment(m);
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 4, 5, 10)), 'millisecond'), true, 'millisecond match');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 4, 5, 10)), 'milliseconds'), true, 'plural should work');
    assert.equal(m.isSame(moment(new Date(2012, 3, 2, 3, 4, 5, 10)), 'millisecond'), false, 'year is later');
    assert.equal(m.isSame(moment(new Date(2010, 3, 2, 3, 4, 5, 10)), 'millisecond'), false, 'year is earlier');
    assert.equal(m.isSame(moment(new Date(2011, 4, 2, 3, 4, 5, 10)), 'millisecond'), false, 'month is later');
    assert.equal(m.isSame(moment(new Date(2011, 2, 2, 3, 4, 5, 10)), 'millisecond'), false, 'month is earlier');
    assert.equal(m.isSame(moment(new Date(2011, 3, 3, 3, 4, 5, 10)), 'millisecond'), false, 'day is later');
    assert.equal(m.isSame(moment(new Date(2011, 3, 1, 1, 4, 5, 10)), 'millisecond'), false, 'day is earlier');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 4, 4, 5, 10)), 'millisecond'), false, 'hour is later');
    assert.equal(m.isSame(moment(new Date(2011, 3, 1, 4, 1, 5, 10)), 'millisecond'), false, 'hour is earlier');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 5, 5, 10)), 'millisecond'), false, 'minute is later');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 3, 5, 10)), 'millisecond'), false, 'minute is earlier');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 4, 6, 10)), 'millisecond'), false, 'second is later');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 4, 4, 5)), 'millisecond'), false, 'second is earlier');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 4, 6, 11)), 'millisecond'), false, 'millisecond is later');
    assert.equal(m.isSame(moment(new Date(2011, 3, 2, 3, 4, 4, 9)), 'millisecond'), false, 'millisecond is earlier');
    assert.equal(m.isSame(m, 'millisecond'), true, 'same moments are in the same millisecond');
    assert.equal(+m, +mCopy, 'isSame millisecond should not change moment');
});

test('is same with utc offset moments', function (assert) {
    assert.ok(moment.parseZone('2013-02-01T-05:00').isSame(moment('2013-02-01'), 'year'), 'zoned vs local moment');
    assert.ok(moment('2013-02-01').isSame(moment('2013-02-01').utcOffset('-05:00'), 'year'), 'local vs zoned moment');
    assert.ok(moment.parseZone('2013-02-01T-05:00').isSame(moment.parseZone('2013-02-01T-06:30'), 'year'),
            'zoned vs (differently) zoned moment');
});
//===================================================================================================================================================================================================================== //>
module('is valid');

test('array bad month', function (assert) {
    assert.equal(moment([2010, -1]).isValid(), false, 'month -1 invalid');
    assert.equal(moment([2100, 12]).isValid(), false, 'month 12 invalid');
});

test('array good month', function (assert) {
    for (var i = 0; i < 12; i++) {
        assert.equal(moment([2010, i]).isValid(), true, 'month ' + i);
        assert.equal(moment.utc([2010, i]).isValid(), true, 'month ' + i);
    }
});

test('array bad date', function (assert) {
    var tests = [
        moment([2010, 0, 0]),
        moment([2100, 0, 32]),
        moment.utc([2010, 0, 0]),
        moment.utc([2100, 0, 32])
    ],
    i, m;

    for (i in tests) {
        m = tests[i];
        assert.equal(m.isValid(), false);
    }
});

test('h/hh with hour > 12', function (assert) {
    assert.ok(moment('06/20/2014 11:51 PM', 'MM/DD/YYYY hh:mm A', true).isValid(), '11 for hh');
    assert.ok(moment('06/20/2014 11:51 AM', 'MM/DD/YYYY hh:mm A', true).isValid(), '11 for hh');
    assert.ok(moment('06/20/2014 23:51 PM', 'MM/DD/YYYY hh:mm A').isValid(), 'non-strict validity 23 for hh');
    assert.ok(moment('06/20/2014 23:51 PM', 'MM/DD/YYYY hh:mm A').parsingFlags().bigHour, 'non-strict bigHour 23 for hh');
    assert.ok(!moment('06/20/2014 23:51 PM', 'MM/DD/YYYY hh:mm A', true).isValid(), 'validity 23 for hh');
    assert.ok(moment('06/20/2014 23:51 PM', 'MM/DD/YYYY hh:mm A', true).parsingFlags().bigHour, 'bigHour 23 for hh');
});

test('array bad date leap year', function (assert) {
    assert.equal(moment([2010, 1, 29]).isValid(), false, '2010 feb 29');
    assert.equal(moment([2100, 1, 29]).isValid(), false, '2100 feb 29');
    assert.equal(moment([2008, 1, 30]).isValid(), false, '2008 feb 30');
    assert.equal(moment([2000, 1, 30]).isValid(), false, '2000 feb 30');

    assert.equal(moment.utc([2010, 1, 29]).isValid(), false, 'utc 2010 feb 29');
    assert.equal(moment.utc([2100, 1, 29]).isValid(), false, 'utc 2100 feb 29');
    assert.equal(moment.utc([2008, 1, 30]).isValid(), false, 'utc 2008 feb 30');
    assert.equal(moment.utc([2000, 1, 30]).isValid(), false, 'utc 2000 feb 30');
});

/* REMOVED Não sei o por que dessa... mas o erro era o seguinte: System.ArgumentOutOfRangeException : O ano deve estar entre 1 e 9999. Nome do parâmetro: year em System.DateTime.DaysInMonth(Int32 year, Int32 month) em Jurassic.Library.DateInstance.ToDateTime(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond, DateTimeKind kind)
   
test('string + formats bad date', function (assert) {
    assert.equal(moment('2020-00-00', []).isValid(), false, 'invalid on empty array');
    assert.equal(moment('2020-00-00', ['YYYY-MM-DD', 'DD-MM-YYYY']).isValid(), false, 'invalid on all in array');
    assert.equal(moment('2020-00-00', ['DD-MM-YYYY', 'YYYY-MM-DD']).isValid(), false, 'invalid on all in array');
    assert.equal(moment('2020-01-01', ['YYYY-MM-DD', 'DD-MM-YYYY']).isValid(), true, 'valid on first');
    assert.equal(moment('2020-01-01', ['DD-MM-YYYY', 'YYYY-MM-DD']).isValid(), true, 'valid on last');
    assert.equal(moment('2020-01-01', ['YYYY-MM-DD', 'YYYY-DD-MM']).isValid(), true, 'valid on both');
    assert.equal(moment('2020-13-01', ['YYYY-MM-DD', 'YYYY-DD-MM']).isValid(), true, 'valid on last');

    assert.equal(moment('12-13-2012', ['DD-MM-YYYY', 'YYYY-MM-DD']).isValid(), false, 'month rollover');
    assert.equal(moment('12-13-2012', ['DD-MM-YYYY', 'DD-MM-YYYY']).isValid(), false, 'month rollover');
    assert.equal(moment('38-12-2012', ['DD-MM-YYYY']).isValid(), false, 'day rollover');
});
*/
test('string nonsensical with format', function (assert) {
    assert.equal(moment('fail', 'MM-DD-YYYY').isValid(), false, 'string \'fail\' with format \'MM-DD-YYYY\'');
    assert.equal(moment('xx-xx-2001', 'DD-MM-YYY').isValid(), true, 'string \'xx-xx-2001\' with format \'MM-DD-YYYY\'');
});

test('string with bad month name', function (assert) {
    assert.equal(moment('01-Nam-2012', 'DD-MMM-YYYY').isValid(), false, '\'Nam\' is an invalid month');
    assert.equal(moment('01-Aug-2012', 'DD-MMM-YYYY').isValid(), true, '\'Aug\' is a valid month');
});

test('string with spaceless format', function (assert) {
    assert.equal(moment('10Sep2001', 'DDMMMYYYY').isValid(), true, 'Parsing 10Sep2001 should result in a valid date');
});

test('invalid string iso 8601', function (assert) {
    var tests = [
        '2010-00-00',
        '2010-01-00',
        '2010-01-40',
        '2010-01-01T24:01',  // 24:00:00 is actually valid
        '2010-01-01T23:60',
        '2010-01-01T23:59:60'
    ], i;

    for (i = 0; i < tests.length; i++) {
        assert.equal(moment(tests[i]).isValid(), false, tests[i] + ' should be invalid');
        assert.equal(moment.utc(tests[i]).isValid(), false, tests[i] + ' should be invalid');
    }
});

test('invalid string iso 8601 + timezone', function (assert) {
    var tests = [
        '2010-00-00T+00:00',
        '2010-01-00T+00:00',
        '2010-01-40T+00:00',
        '2010-01-40T24:01+00:00',
        '2010-01-40T23:60+00:00',
        '2010-01-40T23:59:60+00:00',
        '2010-01-40T23:59:59.9999+00:00'
    ], i;

    for (i = 0; i < tests.length; i++) {
        assert.equal(moment(tests[i]).isValid(), false, tests[i] + ' should be invalid');
        assert.equal(moment.utc(tests[i]).isValid(), false, tests[i] + ' should be invalid');
    }
});

test('valid string iso 8601 + timezone', function (assert) {
    var tests = [
        '2010-01-01',
        '2010-01-30',
        '2010-01-30T23+00:00',
        '2010-01-30T23:59+00:00',
        '2010-01-30T23:59:59+00:00',
        '2010-01-30T23:59:59.999+00:00',
        '2010-01-30T23:59:59.999-07:00',
        '2010-01-30T00:00:00.000+07:00',
        '2010-01-30T00:00:00.000+07'
    ], i;

    for (i = 0; i < tests.length; i++) {
        assert.equal(moment(tests[i]).isValid(), true, tests[i] + ' should be valid');
        assert.equal(moment.utc(tests[i]).isValid(), true, tests[i] + ' should be valid');
    }
});

test('invalidAt', function (assert) {
    assert.equal(moment([2000, 12]).invalidAt(), 1, 'month 12 is invalid: 0-11');
    assert.equal(moment([2000, 1, 30]).invalidAt(), 2, '30 is not a valid february day');
    assert.equal(moment([2000, 1, 29, 25]).invalidAt(), 3, '25 is invalid hour');
    assert.equal(moment([2000, 1, 29, 24,  1]).invalidAt(), 3, '24:01 is invalid hour');
    assert.equal(moment([2000, 1, 29, 23, 60]).invalidAt(), 4, '60 is invalid minute');
    assert.equal(moment([2000, 1, 29, 23, 59, 60]).invalidAt(), 5, '60 is invalid second');
    assert.equal(moment([2000, 1, 29, 23, 59, 59, 1000]).invalidAt(), 6, '1000 is invalid millisecond');
    assert.equal(moment([2000, 1, 29, 23, 59, 59, 999]).invalidAt(), -1, '-1 if everything is fine');
});
/* REMOVED .Net não suporta formato Unix
test('valid Unix timestamp', function (assert) {
    assert.equal(moment(1371065286, 'X').isValid(), true, 'number integer');
    assert.equal(moment(1379066897.0, 'X').isValid(), true, 'number whole 1dp');
    assert.equal(moment(1379066897.7, 'X').isValid(), true, 'number 1dp');
    assert.equal(moment(1379066897.00, 'X').isValid(), true, 'number whole 2dp');
    assert.equal(moment(1379066897.07, 'X').isValid(), true, 'number 2dp');
    assert.equal(moment(1379066897.17, 'X').isValid(), true, 'number 2dp');
    assert.equal(moment(1379066897.000, 'X').isValid(), true, 'number whole 3dp');
    assert.equal(moment(1379066897.007, 'X').isValid(), true, 'number 3dp');
    assert.equal(moment(1379066897.017, 'X').isValid(), true, 'number 3dp');
    assert.equal(moment(1379066897.157, 'X').isValid(), true, 'number 3dp');
    assert.equal(moment('1371065286', 'X').isValid(), true, 'string integer');
    assert.equal(moment('1379066897.', 'X').isValid(), true, 'string trailing .');
    assert.equal(moment('1379066897.0', 'X').isValid(), true, 'string whole 1dp');
    assert.equal(moment('1379066897.7', 'X').isValid(), true, 'string 1dp');
    assert.equal(moment('1379066897.00', 'X').isValid(), true, 'string whole 2dp');
    assert.equal(moment('1379066897.07', 'X').isValid(), true, 'string 2dp');
    assert.equal(moment('1379066897.17', 'X').isValid(), true, 'string 2dp');
    assert.equal(moment('1379066897.000', 'X').isValid(), true, 'string whole 3dp');
    assert.equal(moment('1379066897.007', 'X').isValid(), true, 'string 3dp');
    assert.equal(moment('1379066897.017', 'X').isValid(), true, 'string 3dp');
    assert.equal(moment('1379066897.157', 'X').isValid(), true, 'string 3dp');
});
*/
/* REMOVED .Net não suporta formato Unix
test('invalid Unix timestamp', function (assert) {
    assert.equal(moment(undefined, 'X').isValid(), false, 'undefined');
    assert.equal(moment('undefined', 'X').isValid(), false, 'string undefined');
    try {
        assert.equal(moment(null, 'X').isValid(), false, 'null');
    } catch (e) {
        assert.ok(true, 'null');
    }

    assert.equal(moment('null', 'X').isValid(), false, 'string null');
    assert.equal(moment([], 'X').isValid(), false, 'array');
    assert.equal(moment('{}', 'X').isValid(), false, 'object');
    try {
        assert.equal(moment('', 'X').isValid(), false, 'string empty');
    } catch (e) {
        assert.ok(true, 'string empty');
    }

    assert.equal(moment(' ', 'X').isValid(), false, 'string space');
});
*/
/* REMOVED .Net não suporta formato Unix
test('valid Unix offset milliseconds', function (assert) {
    assert.equal(moment(1234567890123, 'x').isValid(), true, 'number integer');
    assert.equal(moment('1234567890123', 'x').isValid(), true, 'string integer');
});
*/
/* REMOVED .Net não suporta formato Unix
test('invalid Unix offset milliseconds', function (assert) {
    assert.equal(moment(undefined, 'x').isValid(), false, 'undefined');
    assert.equal(moment('undefined', 'x').isValid(), false, 'string undefined');
    try {
        assert.equal(moment(null, 'x').isValid(), false, 'null');
    } catch (e) {
        assert.ok(true, 'null');
    }

    assert.equal(moment('null', 'x').isValid(), false, 'string null');
    assert.equal(moment([], 'x').isValid(), false, 'array');
    assert.equal(moment('{}', 'x').isValid(), false, 'object');
    try {
        assert.equal(moment('', 'x').isValid(), false, 'string empty');
    } catch (e) {
        assert.ok(true, 'string empty');
    }

    assert.equal(moment(' ', 'x').isValid(), false, 'string space');
});
*/
test('empty', function (assert) {
    assert.equal(moment(null).isValid(), false, 'null');
    assert.equal(moment('').isValid(), false, 'empty string');
    assert.equal(moment(null, 'YYYY').isValid(), false, 'format + null');
    assert.equal(moment('', 'YYYY').isValid(), false, 'format + empty string');
    assert.equal(moment(' ', 'YYYY').isValid(), false, 'format + empty when trimmed');
});

test('days of the year', function (assert) {
    assert.equal(moment('2010 300', 'YYYY DDDD').isValid(), true, 'day 300 of year valid');
    assert.equal(moment('2010 365', 'YYYY DDDD').isValid(), true, 'day 365 of year valid');
    assert.equal(moment('2010 366', 'YYYY DDDD').isValid(), false, 'day 366 of year invalid');
    assert.equal(moment('2012 365', 'YYYY DDDD').isValid(), true, 'day 365 of leap year valid');
    assert.equal(moment('2012 366', 'YYYY DDDD').isValid(), true, 'day 366 of leap year valid');
    assert.equal(moment('2012 367', 'YYYY DDDD').isValid(), false, 'day 367 of leap year invalid');
});

test('24:00:00.000 is valid', function (assert) {
    assert.equal(moment('2014-01-01 24', 'YYYY-MM-DD HH').isValid(), true, '24 is valid');
    assert.equal(moment('2014-01-01 24:00', 'YYYY-MM-DD HH:mm').isValid(), true, '24:00 is valid');
    assert.equal(moment('2014-01-01 24:01', 'YYYY-MM-DD HH:mm').isValid(), false, '24:01 is not valid');
});

test('oddball permissiveness', function (assert) {
    //https://github.com/moment/moment/issues/1128
    assert.ok(moment('2010-10-3199', ['MM/DD/YYYY', 'MM-DD-YYYY', 'YYYY-MM-DD']).isValid());

    //https://github.com/moment/moment/issues/1122
    assert.ok(moment('3:25', ['h:mma', 'hh:mma', 'H:mm', 'HH:mm']).isValid());
});

test('0 hour is invalid in strict', function (assert) {
    assert.equal(moment('00:01', 'hh:mm', true).isValid(), false, '00 hour is invalid in strict');
    assert.equal(moment('00:01', 'hh:mm').isValid(), true, '00 hour is valid in normal');
    assert.equal(moment('0:01', 'h:mm', true).isValid(), false, '0 hour is invalid in strict');
    assert.equal(moment('0:01', 'h:mm').isValid(), true, '0 hour is valid in normal');
});
//===================================================================================================================================================================================================================== //>
module('leap year');

test('leap year', function (assert) {
    assert.equal(moment([2010, 0, 1]).isLeapYear(), false, '2010');
    assert.equal(moment([2100, 0, 1]).isLeapYear(), false, '2100');
    assert.equal(moment([2008, 0, 1]).isLeapYear(), true, '2008');
    assert.equal(moment([2000, 0, 1]).isLeapYear(), true, '2000');
});
//===================================================================================================================================================================================================================== //>
module('listers');

test('default', function (assert) {
    assert.deepEqual(moment.months(), ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December']);
    assert.deepEqual(moment.monthsShort(), ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']);
    assert.deepEqual(moment.weekdays(), ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']);
    assert.deepEqual(moment.weekdaysShort(), ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']);
    assert.deepEqual(moment.weekdaysMin(), ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa']);
});

test('index', function (assert) {
    assert.equal(moment.months(0), 'January');
    assert.equal(moment.months(2), 'March');
    assert.equal(moment.monthsShort(0), 'Jan');
    assert.equal(moment.monthsShort(2), 'Mar');
    assert.equal(moment.weekdays(0), 'Sunday');
    assert.equal(moment.weekdays(2), 'Tuesday');
    assert.equal(moment.weekdaysShort(0), 'Sun');
    assert.equal(moment.weekdaysShort(2), 'Tue');
    assert.equal(moment.weekdaysMin(0), 'Su');
    assert.equal(moment.weekdaysMin(2), 'Tu');
});

test('localized', function (assert) {
    var months = 'one_two_three_four_five_six_seven_eight_nine_ten_eleven_twelve'.split('_'),
        monthsShort = 'on_tw_th_fo_fi_si_se_ei_ni_te_el_tw'.split('_'),
        weekdays = 'one_two_three_four_five_six_seven'.split('_'),
        weekdaysShort = 'on_tw_th_fo_fi_si_se'.split('_'),
        weekdaysMin = '1_2_3_4_5_6_7'.split('_');

    moment.locale('numerologists', {
        months : months,
        monthsShort : monthsShort,
        weekdays : weekdays,
        weekdaysShort: weekdaysShort,
        weekdaysMin: weekdaysMin
    });

    assert.deepEqual(moment.months(), months);
    assert.deepEqual(moment.monthsShort(), monthsShort);
    assert.deepEqual(moment.weekdays(), weekdays);
    assert.deepEqual(moment.weekdaysShort(), weekdaysShort);
    assert.deepEqual(moment.weekdaysMin(), weekdaysMin);

    assert.equal(moment.months(0), 'one');
    assert.equal(moment.monthsShort(0), 'on');
    assert.equal(moment.weekdays(0), 'one');
    assert.equal(moment.weekdaysShort(0), 'on');
    assert.equal(moment.weekdaysMin(0), '1');

    assert.equal(moment.months(2), 'three');
    assert.equal(moment.monthsShort(2), 'th');
    assert.equal(moment.weekdays(2), 'three');
    assert.equal(moment.weekdaysShort(2), 'th');
    assert.equal(moment.weekdaysMin(2), '3');
});

test('with functions', function (assert) {
    var monthsShort = 'one_two_three_four_five_six_seven_eight_nine_ten_eleven_twelve'.split('_'),
        monthsShortWeird = 'onesy_twosy_threesy_foursy_fivesy_sixsy_sevensy_eightsy_ninesy_tensy_elevensy_twelvesy'.split('_');

    moment.locale('difficult', {

        monthsShort: function (m, format) {
            var arr = format.match(/-MMM-/) ? monthsShortWeird : monthsShort;
            return arr[m.month()];
        }
    });

    assert.deepEqual(moment.monthsShort(), monthsShort);
    assert.deepEqual(moment.monthsShort('MMM'), monthsShort);
    assert.deepEqual(moment.monthsShort('-MMM-'), monthsShortWeird);

    assert.deepEqual(moment.monthsShort('MMM', 2), 'three');
    assert.deepEqual(moment.monthsShort('-MMM-', 2), 'threesy');
    assert.deepEqual(moment.monthsShort(2), 'three');
});
//===================================================================================================================================================================================================================== //>
module('locale', {
    setup : function () {
        // TODO: Remove once locales are switched to ES6
        each([{
            name: 'en-gb',
            data: {}
        }, {
            name: 'en-ca',
            data: {}
        }, {
            name: 'es',
            data: {
                relativeTime: {past: 'hace %s', s: 'unos segundos', d: 'un día'},
                months: 'enero_febrero_marzo_abril_mayo_junio_julio_agosto_septiembre_octubre_noviembre_diciembre'.split('_')
            }
        }, {
            name: 'fr',
            data: {}
        }, {
            name: 'fr-ca',
            data: {}
        }, {
            name: 'it',
            data: {}
        }, {
            name: 'zh-cn',
            data: {
                months: '一月_二月_三月_四月_五月_六月_七月_八月_九月_十月_十一月_十二月'.split('_')
            }
        }], function (locale) {
            if (moment.locale(locale.name) !== locale.name) {
                moment.defineLocale(locale.name, locale.data);
            }
        });
        moment.locale('en');
    }
});

test('library getters and setters', function (assert) {
    var r = moment.locale('en');

    assert.equal(r, 'en', 'locale should return en by default');
    assert.equal(moment.locale(), 'en', 'locale should return en by default');

    moment.locale('fr');
    assert.equal(moment.locale(), 'fr', 'locale should return the changed locale');

    moment.locale('en-gb');
    assert.equal(moment.locale(), 'en-gb', 'locale should return the changed locale');

    moment.locale('en');
    assert.equal(moment.locale(), 'en', 'locale should reset');

    moment.locale('does-not-exist');
    assert.equal(moment.locale(), 'en', 'locale should reset');

    moment.locale('EN');
    assert.equal(moment.locale(), 'en', 'Normalize locale key case');

    moment.locale('EN_gb');
    assert.equal(moment.locale(), 'en-gb', 'Normalize locale key underscore');
});

test('library setter array of locales', function (assert) {
    assert.equal(moment.locale(['non-existent', 'fr', 'also-non-existent']), 'fr', 'passing an array uses the first valid locale');
    assert.equal(moment.locale(['es', 'fr', 'also-non-existent']), 'es', 'passing an array uses the first valid locale');
});

test('library setter locale substrings', function (assert) {
    assert.equal(moment.locale('fr-crap'), 'fr', 'use substrings');
    assert.equal(moment.locale('fr-does-not-exist'), 'fr', 'uses deep substrings');
    assert.equal(moment.locale('fr-CA-does-not-exist'), 'fr-ca', 'uses deepest substring');
});

test('library getter locale array and substrings', function (assert) {
    assert.equal(moment.locale(['en-CH', 'fr']), 'en', 'prefer root locale to shallower ones');
    assert.equal(moment.locale(['en-gb-leeds', 'en-CA']), 'en-gb', 'prefer root locale to shallower ones');
    assert.equal(moment.locale(['en-fake', 'en-CA']), 'en-ca', 'prefer alternatives with shared roots');
    assert.equal(moment.locale(['en-fake', 'en-fake2', 'en-ca']), 'en-ca', 'prefer alternatives with shared roots');
    assert.equal(moment.locale(['fake-CA', 'fake-MX', 'fr']), 'fr', 'always find something if possible');
    assert.equal(moment.locale(['fake-CA', 'fake-MX', 'fr']), 'fr', 'always find something if possible');
    assert.equal(moment.locale(['fake-CA', 'fake-MX', 'fr-fake-fake-fake']), 'fr', 'always find something if possible');
    assert.equal(moment.locale(['en', 'en-CA']), 'en', 'prefer earlier if it works');
});

test('library ensure inheritance', function (assert) {
    moment.locale('made-up', {
        // I put them out of order
        months : 'February_March_April_May_June_July_August_September_October_November_December_January'.split('_')
        // the rest of the properties should be inherited.
    });

    assert.equal(moment([2012, 5, 6]).format('MMMM'), 'July', 'Override some of the configs');
    assert.equal(moment([2012, 5, 6]).format('MMM'), 'Jun', 'But not all of them');
});

test('library ensure inheritance LT L LL LLL LLLL', function (assert) {
    var locale = 'test-inherit-lt';

    moment.defineLocale(locale, {
        longDateFormat : {
            LT : '-[LT]-',
            L : '-[L]-',
            LL : '-[LL]-',
            LLL : '-[LLL]-',
            LLLL : '-[LLLL]-'
        },
        calendar : {
            sameDay : '[sameDay] LT',
            nextDay : '[nextDay] L',
            nextWeek : '[nextWeek] LL',
            lastDay : '[lastDay] LLL',
            lastWeek : '[lastWeek] LLLL',
            sameElse : 'L'
        }
    });

    moment.locale('es');

    assert.equal(moment().locale(locale).calendar(), 'sameDay -LT-', 'Should use instance locale in LT formatting');
    assert.equal(moment().add(1, 'days').locale(locale).calendar(), 'nextDay -L-', 'Should use instance locale in L formatting');
    assert.equal(moment().add(-1, 'days').locale(locale).calendar(), 'lastDay -LLL-', 'Should use instance locale in LL formatting');
    assert.equal(moment().add(4, 'days').locale(locale).calendar(), 'nextWeek -LL-', 'Should use instance locale in LLL formatting');
    assert.equal(moment().add(-4, 'days').locale(locale).calendar(), 'lastWeek -LLLL-', 'Should use instance locale in LLLL formatting');
});

test('library localeData', function (assert) {
    moment.locale('en');

    var jan = moment([2000, 0]);

    assert.equal(moment.localeData().months(jan), 'January', 'no arguments returns global');
    assert.equal(moment.localeData('zh-cn').months(jan), '一月', 'a string returns the locale based on key');
    //assert.equal(moment.localeData(moment().locale('es')).months(jan), 'Enero', 'if you pass in a moment it uses the moment\'s locale');
	assert.equal(moment.localeData(moment().locale('es')).months(jan), 'enero', 'if you pass in a moment it uses the moment\'s locale');
});

test('library deprecations', function (assert) {
    moment.lang('dude', {months: ['Movember']});
    assert.equal(moment.locale(), 'dude', 'setting the lang sets the locale');
    assert.equal(moment.lang(), moment.locale());
    assert.equal(moment.langData(), moment.localeData(), 'langData is localeData');
});

test('defineLocale', function (assert) {
    moment.locale('en');
    moment.defineLocale('dude', {months: ['Movember']});
    assert.equal(moment().locale(), 'dude', 'defineLocale also sets it');
    assert.equal(moment().locale('dude').locale(), 'dude', 'defineLocale defines a locale');
});

test('library convenience', function (assert) {
    moment.locale('something', {week: {dow: 3}});
    moment.locale('something');
    assert.equal(moment.locale(), 'something', 'locale can be used to create the locale too');
});

test('firstDayOfWeek firstDayOfYear locale getters', function (assert) {
    moment.locale('something', {week: {dow: 3, doy: 4}});
    moment.locale('something');
    assert.equal(moment.localeData().firstDayOfWeek(), 3, 'firstDayOfWeek');
    assert.equal(moment.localeData().firstDayOfYear(), 4, 'firstDayOfYear');
});

test('instance locale method', function (assert) {
    moment.locale('en');

    assert.equal(moment([2012, 5, 6]).format('MMMM'), 'June', 'Normally default to global');
    assert.equal(moment([2012, 5, 6]).locale('es').format('MMMM'), 'junio', 'Use the instance specific locale');
    assert.equal(moment([2012, 5, 6]).format('MMMM'), 'June', 'Using an instance specific locale does not affect other moments');
});

test('instance locale method with array', function (assert) {
    var m = moment().locale(['non-existent', 'fr', 'also-non-existent']);
    assert.equal(m.locale(), 'fr', 'passing an array uses the first valid locale');
    m = moment().locale(['es', 'fr', 'also-non-existent']);
    assert.equal(m.locale(), 'es', 'passing an array uses the first valid locale');
});

test('instance getter locale substrings', function (assert) {
    var m = moment();

    m.locale('fr-crap');
    assert.equal(m.locale(), 'fr', 'use substrings');

    m.locale('fr-does-not-exist');
    assert.equal(m.locale(), 'fr', 'uses deep substrings');
});

test('instance locale persists with manipulation', function (assert) {
    moment.locale('en');

    assert.equal(moment([2012, 5, 6]).locale('es').add({days: 1}).format('MMMM'), 'junio', 'With addition');
    assert.equal(moment([2012, 5, 6]).locale('es').day(0).format('MMMM'), 'junio', 'With day getter');
    assert.equal(moment([2012, 5, 6]).locale('es').endOf('day').format('MMMM'), 'junio', 'With endOf');
});

test('instance locale persists with cloning', function (assert) {
    moment.locale('en');

    var a = moment([2012, 5, 6]).locale('es'),
        b = a.clone(),
        c = moment(a);

    assert.equal(b.format('MMMM'), 'junio', 'using moment.fn.clone()');
    assert.equal(b.format('MMMM'), 'junio', 'using moment()');
});

test('duration locale method', function (assert) {
    moment.locale('en');

    assert.equal(moment.duration({seconds:  44}).humanize(), 'a few seconds', 'Normally default to global');
    assert.equal(moment.duration({seconds:  44}).locale('es').humanize(), 'unos segundos', 'Use the instance specific locale');
    assert.equal(moment.duration({seconds:  44}).humanize(), 'a few seconds', 'Using an instance specific locale does not affect other durations');
});

test('duration locale persists with cloning', function (assert) {
    moment.locale('en');

    var a = moment.duration({seconds:  44}).locale('es'),
        b = moment.duration(a);

    assert.equal(b.humanize(), 'unos segundos', 'using moment.duration()');
});

test('changing the global locale doesn\'t affect existing duration instances', function (assert) {
    var mom = moment.duration();
    moment.locale('fr');
    assert.equal('en', mom.locale());
});

test('duration deprecations', function (assert) {
    assert.equal(moment.duration().lang(), moment.duration().localeData(), 'duration.lang is the same as duration.localeData');
});

test('from and fromNow with invalid date', function (assert) {
    assert.equal(moment(NaN).from(), 'Invalid date', 'moment.from with invalid moment');
    assert.equal(moment(NaN).fromNow(), 'Invalid date', 'moment.fromNow with invalid moment');
});

test('from relative time future', function (assert) {
    var start = moment([2007, 1, 28]);

    assert.equal(start.from(moment([2007, 1, 28]).subtract({s: 44})),  'in a few seconds', '44 seconds = a few seconds');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({s: 45})),  'in a minute',      '45 seconds = a minute');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({s: 89})),  'in a minute',      '89 seconds = a minute');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({s: 90})),  'in 2 minutes',     '90 seconds = 2 minutes');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({m: 44})),  'in 44 minutes',    '44 minutes = 44 minutes');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({m: 45})),  'in an hour',       '45 minutes = an hour');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({m: 89})),  'in an hour',       '89 minutes = an hour');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({m: 90})),  'in 2 hours',       '90 minutes = 2 hours');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({h: 5})),   'in 5 hours',       '5 hours = 5 hours');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({h: 21})),  'in 21 hours',      '21 hours = 21 hours');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({h: 22})),  'in a day',         '22 hours = a day');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({h: 35})),  'in a day',         '35 hours = a day');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({h: 36})),  'in 2 days',        '36 hours = 2 days');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 1})),   'in a day',         '1 day = a day');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 5})),   'in 5 days',        '5 days = 5 days');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 25})),  'in 25 days',       '25 days = 25 days');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 26})),  'in a month',       '26 days = a month');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 30})),  'in a month',       '30 days = a month');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 45})),  'in a month',       '45 days = a month');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 47})),  'in 2 months',      '47 days = 2 months');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 74})),  'in 2 months',      '74 days = 2 months');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 78})),  'in 3 months',      '78 days = 3 months');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({M: 1})),   'in a month',       '1 month = a month');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({M: 5})),   'in 5 months',      '5 months = 5 months');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 315})), 'in 10 months',     '315 days = 10 months');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 344})), 'in a year',        '344 days = a year');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 345})), 'in a year',        '345 days = a year');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({d: 548})), 'in 2 years',       '548 days = in 2 years');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({y: 1})),   'in a year',        '1 year = a year');
    assert.equal(start.from(moment([2007, 1, 28]).subtract({y: 5})),   'in 5 years',       '5 years = 5 years');
});

test('from relative time past', function (assert) {
    var start = moment([2007, 1, 28]);

    assert.equal(start.from(moment([2007, 1, 28]).add({s: 44})),  'a few seconds ago', '44 seconds = a few seconds');
    assert.equal(start.from(moment([2007, 1, 28]).add({s: 45})),  'a minute ago',      '45 seconds = a minute');
    assert.equal(start.from(moment([2007, 1, 28]).add({s: 89})),  'a minute ago',      '89 seconds = a minute');
    assert.equal(start.from(moment([2007, 1, 28]).add({s: 90})),  '2 minutes ago',     '90 seconds = 2 minutes');
    assert.equal(start.from(moment([2007, 1, 28]).add({m: 44})),  '44 minutes ago',    '44 minutes = 44 minutes');
    assert.equal(start.from(moment([2007, 1, 28]).add({m: 45})),  'an hour ago',       '45 minutes = an hour');
    assert.equal(start.from(moment([2007, 1, 28]).add({m: 89})),  'an hour ago',       '89 minutes = an hour');
    assert.equal(start.from(moment([2007, 1, 28]).add({m: 90})),  '2 hours ago',       '90 minutes = 2 hours');
    assert.equal(start.from(moment([2007, 1, 28]).add({h: 5})),   '5 hours ago',       '5 hours = 5 hours');
    assert.equal(start.from(moment([2007, 1, 28]).add({h: 21})),  '21 hours ago',      '21 hours = 21 hours');
    assert.equal(start.from(moment([2007, 1, 28]).add({h: 22})),  'a day ago',         '22 hours = a day');
    assert.equal(start.from(moment([2007, 1, 28]).add({h: 35})),  'a day ago',         '35 hours = a day');
    assert.equal(start.from(moment([2007, 1, 28]).add({h: 36})),  '2 days ago',        '36 hours = 2 days');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 1})),   'a day ago',         '1 day = a day');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 5})),   '5 days ago',        '5 days = 5 days');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 25})),  '25 days ago',       '25 days = 25 days');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 26})),  'a month ago',       '26 days = a month');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 30})),  'a month ago',       '30 days = a month');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 43})),  'a month ago',       '43 days = a month');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 46})),  '2 months ago',      '46 days = 2 months');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 74})),  '2 months ago',      '75 days = 2 months');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 76})),  '3 months ago',      '76 days = 3 months');
    assert.equal(start.from(moment([2007, 1, 28]).add({M: 1})),   'a month ago',       '1 month = a month');
    assert.equal(start.from(moment([2007, 1, 28]).add({M: 5})),   '5 months ago',      '5 months = 5 months');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 315})), '10 months ago',     '315 days = 10 months');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 344})), 'a year ago',        '344 days = a year');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 345})), 'a year ago',        '345 days = a year');
    assert.equal(start.from(moment([2007, 1, 28]).add({d: 548})), '2 years ago',       '548 days = 2 years');
    assert.equal(start.from(moment([2007, 1, 28]).add({y: 1})),   'a year ago',        '1 year = a year');
    assert.equal(start.from(moment([2007, 1, 28]).add({y: 5})),   '5 years ago',       '5 years = 5 years');
});

test('instance locale used with from', function (assert) {
    moment.locale('en');

    var a = moment([2012, 5, 6]).locale('es'),
        b = moment([2012, 5, 7]);

    assert.equal(a.from(b), 'hace un día', 'preserve locale of first moment');
    assert.equal(b.from(a), 'in a day', 'do not preserve locale of second moment');
});

test('instance localeData', function (assert) {
    moment.defineLocale('dude', {week: {dow: 3}});
    assert.equal(moment().locale('dude').localeData()._week.dow, 3);
});

test('month name callback function', function (assert) {
    function fakeReplace(m, format) {
        if (/test/.test(format)) {
            return 'test';
        }
        if (m.date() === 1) {
            return 'date';
        }
        return 'default';
    }

    moment.locale('made-up-2', {
        months : fakeReplace,
        monthsShort : fakeReplace,
        weekdays : fakeReplace,
        weekdaysShort : fakeReplace,
        weekdaysMin : fakeReplace
    });

    assert.equal(moment().format('[test] dd ddd dddd MMM MMMM'), 'test test test test test test', 'format month name function should be able to access the format string');
    assert.equal(moment([2011, 0, 1]).format('dd ddd dddd MMM MMMM'), 'date date date date date', 'format month name function should be able to access the moment object');
    assert.equal(moment([2011, 0, 2]).format('dd ddd dddd MMM MMMM'), 'default default default default default', 'format month name function should be able to access the moment object');
});

test('changing parts of a locale config', function (assert) {
    moment.locale('partial-lang', {
        months : 'a b c d e f g h i j k l'.split(' ')
    });

    assert.equal(moment([2011, 0, 1]).format('MMMM'), 'a', 'should be able to set locale values when creating the localeuage');

    moment.locale('partial-lang', {
        monthsShort : 'A B C D E F G H I J K L'.split(' ')
    });

    assert.equal(moment([2011, 0, 1]).format('MMMM MMM'), 'a A', 'should be able to set locale values after creating the localeuage');
});

test('start/endOf week feature for first-day-is-monday locales', function (assert) {
    moment.locale('monday-lang', {
        week : {
            dow : 1 // Monday is the first day of the week
        }
    });

    moment.locale('monday-lang');
    assert.equal(moment([2013, 0, 1]).startOf('week').day(), 1, 'for locale monday-lang first day of the week should be monday');
    assert.equal(moment([2013, 0, 1]).endOf('week').day(), 0, 'for locale monday-lang last day of the week should be sunday');
});

test('meridiem parsing', function (assert) {
    moment.locale('meridiem-parsing', {
        meridiemParse : /[bd]/i,
        isPM : function (input) {
            return input === 'b';
        }
    });

    moment.locale('meridiem-parsing');
    assert.equal(moment('2012-01-01 3b', 'YYYY-MM-DD ha').hour(), 15, 'Custom parsing of meridiem should work');
    assert.equal(moment('2012-01-01 3d', 'YYYY-MM-DD ha').hour(), 3, 'Custom parsing of meridiem should work');
});

test('invalid date formatting', function (assert) {
    moment.locale('has-invalid', {
        invalidDate: 'KHAAAAAAAAAAAN!'
    });

    assert.equal(moment.invalid().format(), 'KHAAAAAAAAAAAN!');
    assert.equal(moment.invalid().format('YYYY-MM-DD'), 'KHAAAAAAAAAAAN!');
});

test('return locale name', function (assert) {
    var registered = moment.locale('return-this', {});

    assert.equal(registered, 'return-this', 'returns the locale configured');
});

test('changing the global locale doesn\'t affect existing instances', function (assert) {
    var mom = moment();
    moment.locale('fr');
    assert.equal('en', mom.locale());
});

test('setting a language on instance returns the original moment for chaining', function (assert) {
    var mom = moment();

    assert.equal(mom.lang('fr'), mom, 'setting the language (lang) returns the original moment for chaining');
    assert.equal(mom.locale('it'), mom, 'setting the language (locale) returns the original moment for chaining');
});

test('lang(key) changes the language of the instance', function (assert) {
    var m = moment().month(0);
    m.lang('fr');
    assert.equal(m.locale(), 'fr', 'm.lang(key) changes instance locale');
});

test('moment#locale(false) resets to global locale', function (assert) {
    var m = moment();

    moment.locale('fr');
    m.locale('it');

    assert.equal(moment.locale(), 'fr', 'global locale is it');
    assert.equal(m.locale(), 'it', 'instance locale is it');
    m.locale(false);
    assert.equal(m.locale(), 'fr', 'instance locale reset to global locale');
});

test('moment().locale with missing key doesn\'t change locale', function (assert) {
    assert.equal(moment().locale('boo').localeData(), moment.localeData(),
            'preserve global locale in case of bad locale id');
});

test('moment().lang with missing key doesn\'t change locale', function (assert) {
    assert.equal(moment().lang('boo').localeData(), moment.localeData(),
            'preserve global locale in case of bad locale id');
});
//===================================================================================================================================================================================================================== //>
module('min max');

test('min', function (assert) {
    var now = moment(),
        future = now.clone().add(1, 'month'),
        past = now.clone().subtract(1, 'month');

    assert.equal(moment.min(now, future, past), past, 'min(now, future, past)');
    assert.equal(moment.min(future, now, past), past, 'min(future, now, past)');
    assert.equal(moment.min(future, past, now), past, 'min(future, past, now)');
    assert.equal(moment.min(past, future, now), past, 'min(past, future, now)');
    assert.equal(moment.min(now, past), past, 'min(now, past)');
    assert.equal(moment.min(past, now), past, 'min(past, now)');
    assert.equal(moment.min(now), now, 'min(now, past)');

    assert.equal(moment.min([now, future, past]), past, 'min([now, future, past])');
    assert.equal(moment.min([now, past]), past, 'min(now, past)');
    assert.equal(moment.min([now]), now, 'min(now)');
});

test('max', function (assert) {
    var now = moment(),
        future = now.clone().add(1, 'month'),
        past = now.clone().subtract(1, 'month');

    assert.equal(moment.max(now, future, past), future, 'max(now, future, past)');
    assert.equal(moment.max(future, now, past), future, 'max(future, now, past)');
    assert.equal(moment.max(future, past, now), future, 'max(future, past, now)');
    assert.equal(moment.max(past, future, now), future, 'max(past, future, now)');
    assert.equal(moment.max(now, past), now, 'max(now, past)');
    assert.equal(moment.max(past, now), now, 'max(past, now)');
    assert.equal(moment.max(now), now, 'max(now, past)');

    assert.equal(moment.max([now, future, past]), future, 'max([now, future, past])');
    assert.equal(moment.max([now, past]), now, 'max(now, past)');
    assert.equal(moment.max([now]), now, 'max(now)');
});
//===================================================================================================================================================================================================================== //>
module('mutable');

test('manipulation methods', function (assert) {
    var m = moment();

    assert.equal(m, m.year(2011), 'year() should be mutable');
    assert.equal(m, m.month(1), 'month() should be mutable');
    assert.equal(m, m.hours(7), 'hours() should be mutable');
    assert.equal(m, m.minutes(33), 'minutes() should be mutable');
    assert.equal(m, m.seconds(44), 'seconds() should be mutable');
    assert.equal(m, m.milliseconds(55), 'milliseconds() should be mutable');
    assert.equal(m, m.day(2), 'day() should be mutable');
    assert.equal(m, m.startOf('week'), 'startOf() should be mutable');
    assert.equal(m, m.add(1, 'days'), 'add() should be mutable');
    assert.equal(m, m.subtract(2, 'years'), 'subtract() should be mutable');
    assert.equal(m, m.local(), 'local() should be mutable');
    assert.equal(m, m.utc(), 'utc() should be mutable');
});

test('non mutable methods', function (assert) {
    var m = moment();
    assert.notEqual(m, m.clone(), 'clone() should not be mutable');
});
//===================================================================================================================================================================================================================== //>
module('normalize units');

test('normalize units', function (assert) {
    var fullKeys = ['year', 'quarter', 'month', 'isoWeek', 'week', 'day', 'hour', 'minute', 'second', 'millisecond', 'date', 'dayOfYear', 'weekday', 'isoWeekday', 'weekYear', 'isoWeekYear'],
        aliases = ['y', 'Q', 'M', 'W', 'w', 'd', 'h', 'm', 's', 'ms', 'D', 'DDD', 'e', 'E', 'gg', 'GG'],
        length = fullKeys.length,
        fullKey,
        fullKeyCaps,
        fullKeyPlural,
        fullKeyCapsPlural,
        fullKeyLower,
        alias,
        index;

    for (index = 0; index < length; index += 1) {
        fullKey = fullKeys[index];
        fullKeyCaps = fullKey.toUpperCase();
        fullKeyLower = fullKey.toLowerCase();
        fullKeyPlural = fullKey + 's';
        fullKeyCapsPlural = fullKeyCaps + 's';
        alias = aliases[index];
        assert.equal(moment.normalizeUnits(fullKey), fullKey, 'Testing full key ' + fullKey);
        assert.equal(moment.normalizeUnits(fullKeyCaps), fullKey, 'Testing full key capitalised ' + fullKey);
        assert.equal(moment.normalizeUnits(fullKeyPlural), fullKey, 'Testing full key plural ' + fullKey);
        assert.equal(moment.normalizeUnits(fullKeyCapsPlural), fullKey, 'Testing full key capitalised and plural ' + fullKey);
        assert.equal(moment.normalizeUnits(alias), fullKey, 'Testing alias ' + fullKey);
    }
});
//===================================================================================================================================================================================================================== //>
module('parsing flags');

function flags () {
    return moment.apply(null, arguments).parsingFlags();
}

test('overflow with array', function (assert) {
    //months
    assert.equal(flags([2010, 0]).overflow, -1, 'month 0 valid');
    assert.equal(flags([2010, 1]).overflow, -1, 'month 1 valid');
    assert.equal(flags([2010, -1]).overflow, 1, 'month -1 invalid');
    assert.equal(flags([2100, 12]).overflow, 1, 'month 12 invalid');

    //days
    assert.equal(flags([2010, 1, 16]).overflow, -1, 'date valid');
    assert.equal(flags([2010, 1, -1]).overflow, 2, 'date -1 invalid');
    assert.equal(flags([2010, 1, 0]).overflow, 2, 'date 0 invalid');
    assert.equal(flags([2010, 1, 32]).overflow, 2, 'date 32 invalid');
    assert.equal(flags([2012, 1, 29]).overflow, -1, 'date leap year valid');
    assert.equal(flags([2010, 1, 29]).overflow, 2, 'date leap year invalid');

    //hours
    assert.equal(flags([2010, 1, 1, 8]).overflow, -1, 'hour valid');
    assert.equal(flags([2010, 1, 1, 0]).overflow, -1, 'hour 0 valid');
    assert.equal(flags([2010, 1, 1, -1]).overflow, 3, 'hour -1 invalid');
    assert.equal(flags([2010, 1, 1, 25]).overflow, 3, 'hour 25 invalid');
    assert.equal(flags([2010, 1, 1, 24, 1]).overflow, 3, 'hour 24:01 invalid');

    //minutes
    assert.equal(flags([2010, 1, 1, 8, 15]).overflow, -1, 'minute valid');
    assert.equal(flags([2010, 1, 1, 8, 0]).overflow, -1, 'minute 0 valid');
    assert.equal(flags([2010, 1, 1, 8, -1]).overflow, 4, 'minute -1 invalid');
    assert.equal(flags([2010, 1, 1, 8, 60]).overflow, 4, 'minute 60 invalid');

    //seconds
    assert.equal(flags([2010, 1, 1, 8, 15, 12]).overflow, -1, 'second valid');
    assert.equal(flags([2010, 1, 1, 8, 15, 0]).overflow, -1, 'second 0 valid');
    assert.equal(flags([2010, 1, 1, 8, 15, -1]).overflow, 5, 'second -1 invalid');
    assert.equal(flags([2010, 1, 1, 8, 15, 60]).overflow, 5, 'second 60 invalid');

    //milliseconds
    assert.equal(flags([2010, 1, 1, 8, 15, 12, 345]).overflow, -1, 'millisecond valid');
    assert.equal(flags([2010, 1, 1, 8, 15, 12, 0]).overflow, -1, 'millisecond 0 valid');
    assert.equal(flags([2010, 1, 1, 8, 15, 12, -1]).overflow, 6, 'millisecond -1 invalid');
    assert.equal(flags([2010, 1, 1, 8, 15, 12, 1000]).overflow, 6, 'millisecond 1000 invalid');

    // 24 hrs
    assert.equal(flags([2010, 1, 1, 24, 0, 0, 0]).overflow, -1, '24:00:00.000 is fine');
    assert.equal(flags([2010, 1, 1, 24, 1, 0, 0]).overflow, 3, '24:01:00.000 is wrong hour');
    assert.equal(flags([2010, 1, 1, 24, 0, 1, 0]).overflow, 3, '24:00:01.000 is wrong hour');
    assert.equal(flags([2010, 1, 1, 24, 0, 0, 1]).overflow, 3, '24:00:00.001 is wrong hour');
});

test('overflow without format', function (assert) {
    //months
    assert.equal(flags('2001-01', 'YYYY-MM').overflow, -1, 'month 1 valid');
    assert.equal(flags('2001-12', 'YYYY-MM').overflow, -1, 'month 12 valid');
    assert.equal(flags('2001-13', 'YYYY-MM').overflow, 1, 'month 13 invalid');

    //days
    assert.equal(flags('2010-01-16', 'YYYY-MM-DD').overflow, -1, 'date 16 valid');
    assert.equal(flags('2010-01-0',  'YYYY-MM-DD').overflow, 2, 'date 0 invalid');
    assert.equal(flags('2010-01-32', 'YYYY-MM-DD').overflow, 2, 'date 32 invalid');
    assert.equal(flags('2012-02-29', 'YYYY-MM-DD').overflow, -1, 'date leap year valid');
    assert.equal(flags('2010-02-29', 'YYYY-MM-DD').overflow, 2, 'date leap year invalid');

    //days of the year
    assert.equal(flags('2010 300', 'YYYY DDDD').overflow, -1, 'day 300 of year valid');
    assert.equal(flags('2010 365', 'YYYY DDDD').overflow, -1, 'day 365 of year valid');
    assert.equal(flags('2010 366', 'YYYY DDDD').overflow, 2, 'day 366 of year invalid');
    assert.equal(flags('2012 366', 'YYYY DDDD').overflow, -1, 'day 366 of leap year valid');
    assert.equal(flags('2012 367', 'YYYY DDDD').overflow, 2, 'day 367 of leap year invalid');

    //hours
    assert.equal(flags('08', 'HH').overflow, -1, 'hour valid');
    assert.equal(flags('00', 'HH').overflow, -1, 'hour 0 valid');
    assert.equal(flags('25', 'HH').overflow, 3, 'hour 25 invalid');
    assert.equal(flags('24:01', 'HH:mm').overflow, 3, 'hour 24:01 invalid');

    //minutes
    assert.equal(flags('08:15', 'HH:mm').overflow, -1, 'minute valid');
    assert.equal(flags('08:00', 'HH:mm').overflow, -1, 'minute 0 valid');
    assert.equal(flags('08:60', 'HH:mm').overflow, 4, 'minute 60 invalid');

    //seconds
    assert.equal(flags('08:15:12', 'HH:mm:ss').overflow, -1, 'second valid');
    assert.equal(flags('08:15:00', 'HH:mm:ss').overflow, -1, 'second 0 valid');
    assert.equal(flags('08:15:60', 'HH:mm:ss').overflow, 5, 'second 60 invalid');

    //milliseconds
    assert.equal(flags('08:15:12:345', 'HH:mm:ss:SSSS').overflow, -1, 'millisecond valid');
    assert.equal(flags('08:15:12:000', 'HH:mm:ss:SSSS').overflow, -1, 'millisecond 0 valid');

    //this is OK because we don't match the last digit, so it's 100 ms
    assert.equal(flags('08:15:12:1000', 'HH:mm:ss:SSSS').overflow, -1, 'millisecond 1000 actually valid');
});

test('extra tokens', function (assert) {
    assert.deepEqual(flags('1982-05-25', 'YYYY-MM-DD').unusedTokens, [], 'nothing extra');
    assert.deepEqual(flags('1982-05', 'YYYY-MM-DD').unusedTokens, ['DD'], 'extra formatting token');
    assert.deepEqual(flags('1982', 'YYYY-MM-DD').unusedTokens, ['MM', 'DD'], 'multiple extra formatting tokens');
    assert.deepEqual(flags('1982-05', 'YYYY-MM-').unusedTokens, [], 'extra non-formatting token');
    assert.deepEqual(flags('1982-05-', 'YYYY-MM-DD').unusedTokens, ['DD'], 'non-extra non-formatting token');
    assert.deepEqual(flags('1982 05 1982', 'YYYY-MM-DD').unusedTokens, [], 'different non-formatting token');
});

test('extra tokens strict', function (assert) {
    assert.deepEqual(flags('1982-05-25', 'YYYY-MM-DD', true).unusedTokens, [], 'nothing extra');
    assert.deepEqual(flags('1982-05', 'YYYY-MM-DD', true).unusedTokens, ['-', 'DD'], 'extra formatting token');
    assert.deepEqual(flags('1982', 'YYYY-MM-DD', true).unusedTokens, ['-', 'MM', '-', 'DD'], 'multiple extra formatting tokens');
    assert.deepEqual(flags('1982-05', 'YYYY-MM-', true).unusedTokens, ['-'], 'extra non-formatting token');
    assert.deepEqual(flags('1982-05-', 'YYYY-MM-DD', true).unusedTokens, ['DD'], 'non-extra non-formatting token');
    assert.deepEqual(flags('1982 05 1982', 'YYYY-MM-DD', true).unusedTokens, ['-', '-'], 'different non-formatting token');
});

test('unused input', function (assert) {
    assert.deepEqual(flags('1982-05-25', 'YYYY-MM-DD').unusedInput, [], 'normal input');
    assert.deepEqual(flags('1982-05-25 this is more stuff', 'YYYY-MM-DD').unusedInput, [' this is more stuff'], 'trailing nonsense');
    assert.deepEqual(flags('1982-05-25 09:30', 'YYYY-MM-DD').unusedInput, [' 09:30'], ['trailing legit-looking input']);
    assert.deepEqual(flags('1982-05-25 some junk', 'YYYY-MM-DD [some junk]').unusedInput, [], 'junk that actually gets matched');
    assert.deepEqual(flags('stuff at beginning 1982-05-25', 'YYYY-MM-DD').unusedInput, ['stuff at beginning '], 'leading junk');
    assert.deepEqual(flags('junk 1982 more junk 05 yet more junk25', 'YYYY-MM-DD').unusedInput, ['junk ', ' more junk ', ' yet more junk'], 'interstitial junk');
});

test('unused input strict', function (assert) {
    assert.deepEqual(flags('1982-05-25', 'YYYY-MM-DD', true).unusedInput, [], 'normal input');
    assert.deepEqual(flags('1982-05-25 this is more stuff', 'YYYY-MM-DD', true).unusedInput, [' this is more stuff'], 'trailing nonsense');
    assert.deepEqual(flags('1982-05-25 09:30', 'YYYY-MM-DD', true).unusedInput, [' 09:30'], ['trailing legit-looking input']);
    assert.deepEqual(flags('1982-05-25 some junk', 'YYYY-MM-DD [some junk]', true).unusedInput, [], 'junk that actually gets matched');
    assert.deepEqual(flags('stuff at beginning 1982-05-25', 'YYYY-MM-DD', true).unusedInput, ['stuff at beginning '], 'leading junk');
    assert.deepEqual(flags('junk 1982 more junk 05 yet more junk25', 'YYYY-MM-DD', true).unusedInput, ['junk ', ' more junk ', ' yet more junk'], 'interstitial junk');
});

test('chars left over', function (assert) {
    assert.equal(flags('1982-05-25', 'YYYY-MM-DD').charsLeftOver, 0, 'normal input');
    assert.equal(flags('1982-05-25 this is more stuff', 'YYYY-MM-DD').charsLeftOver, ' this is more stuff'.length, 'trailing nonsense');
    assert.equal(flags('1982-05-25 09:30', 'YYYY-MM-DD').charsLeftOver, ' 09:30'.length, 'trailing legit-looking input');
    assert.equal(flags('stuff at beginning 1982-05-25', 'YYYY-MM-DD').charsLeftOver, 'stuff at beginning '.length, 'leading junk');
    assert.equal(flags('1982 junk 05 more junk25', 'YYYY-MM-DD').charsLeftOver, [' junk ', ' more junk'].join('').length, 'interstitial junk');
    assert.equal(flags('stuff at beginning 1982 junk 05 more junk25', 'YYYY-MM-DD').charsLeftOver, ['stuff at beginning ', ' junk ', ' more junk'].join('').length, 'leading and interstitial junk');
});

test('empty', function (assert) {
    assert.equal(flags('1982-05-25', 'YYYY-MM-DD').empty, false, 'normal input');
    assert.equal(flags('nothing here', 'YYYY-MM-DD').empty, true, 'pure garbage');
    assert.equal(flags('junk but has the number 2000 in it', 'YYYY-MM-DD').empty, false, 'only mostly garbage');
    assert.equal(flags('', 'YYYY-MM-DD').empty, true, 'empty string');
    assert.equal(flags('', 'YYYY-MM-DD').empty, true, 'blank string');
});

test('null', function (assert) {
    assert.equal(flags('1982-05-25', 'YYYY-MM-DD').nullInput, false, 'normal input');
    assert.equal(flags(null).nullInput, true, 'just null');
    assert.equal(flags(null, 'YYYY-MM-DD').nullInput, true, 'null with format');
});

test('invalid month', function (assert) {
    assert.equal(flags('1982 May', 'YYYY MMMM').invalidMonth, null, 'normal input');
    assert.equal(flags('1982 Laser', 'YYYY MMMM').invalidMonth, 'Laser', 'bad month name');
});

test('empty format array', function (assert) {
    assert.equal(flags('1982 May', ['YYYY MMM']).invalidFormat, false, 'empty format array');
    assert.equal(flags('1982 May', []).invalidFormat, true, 'empty format array');
});
//===================================================================================================================================================================================================================== //>
var symbolMap = {
        '1': '!',
        '2': '@',
        '3': '#',
        '4': '$',
        '5': '%',
        '6': '^',
        '7': '&',
        '8': '*',
        '9': '(',
        '0': ')'
    },
    numberMap = {
        '!': '1',
        '@': '2',
        '#': '3',
        '$': '4',
        '%': '5',
        '^': '6',
        '&': '7',
        '*': '8',
        '(': '9',
        ')': '0'
    };

module('preparse and postformat', {
    setup: function () {
        moment.locale('symbol', {
            preparse: function (string) {
                return string.replace(/[!@#$%\^&*()]/g, function (match) {
                    return numberMap[match];
                });
            },

            postformat: function (string) {
                return string.replace(/\d/g, function (match) {
                    return symbolMap[match];
                });
            }
        });
    }
});

test('transform', function (assert) {
    assert.equal(moment.utc('@)!@-)*-@&', 'YYYY-MM-DD').unix(), 1346025600, 'preparse string + format');
    assert.equal(moment.utc('@)!@-)*-@&').unix(), 1346025600, 'preparse ISO8601 string');
    assert.equal(moment.unix(1346025600).utc().format('YYYY-MM-DD'), '@)!@-)*-@&', 'postformat');
});

test('transform from', function (assert) {
    var start = moment([2007, 1, 28]);

    assert.equal(start.from(moment([2007, 1, 28]).add({s: 90}), true), '@ minutes', 'postformat should work on moment.fn.from');
    assert.equal(moment().add(6, 'd').fromNow(true), '^ days', 'postformat should work on moment.fn.fromNow');
    assert.equal(moment.duration(10, 'h').humanize(), '!) hours', 'postformat should work on moment.duration.fn.humanize');
});

test('calendar day', function (assert) {
    var a = moment().hours(2).minutes(0).seconds(0);

    assert.equal(moment(a).calendar(),                   'Today at @:)) AM',     'today at the same time');
    assert.equal(moment(a).add({m: 25}).calendar(),      'Today at @:@% AM',     'Now plus 25 min');
    assert.equal(moment(a).add({h: 1}).calendar(),       'Today at #:)) AM',     'Now plus 1 hour');
    assert.equal(moment(a).add({d: 1}).calendar(),       'Tomorrow at @:)) AM',  'tomorrow at the same time');
    assert.equal(moment(a).subtract({h: 1}).calendar(),  'Today at !:)) AM',     'Now minus 1 hour');
    assert.equal(moment(a).subtract({d: 1}).calendar(),  'Yesterday at @:)) AM', 'yesterday at the same time');
});
//===================================================================================================================================================================================================================== //>
module('quarter');

test('library quarter getter', function (assert) {
    assert.equal(moment([1985,  1,  4]).quarter(), 1, 'Feb  4 1985 is Q1');
    assert.equal(moment([2029,  8, 18]).quarter(), 3, 'Sep 18 2029 is Q3');
    assert.equal(moment([2013,  3, 24]).quarter(), 2, 'Apr 24 2013 is Q2');
    assert.equal(moment([2015,  2,  5]).quarter(), 1, 'Mar  5 2015 is Q1');
    assert.equal(moment([1970,  0,  2]).quarter(), 1, 'Jan  2 1970 is Q1');
    assert.equal(moment([2001, 11, 12]).quarter(), 4, 'Dec 12 2001 is Q4');
    assert.equal(moment([2000,  0,  2]).quarter(), 1, 'Jan  2 2000 is Q1');
});

test('quarter setter singular', function (assert) {
    var m = moment([2014, 4, 11]);
    assert.equal(m.quarter(2).month(), 4, 'set same quarter');
    assert.equal(m.quarter(3).month(), 7, 'set 3rd quarter');
    assert.equal(m.quarter(1).month(), 1, 'set 1st quarter');
    assert.equal(m.quarter(4).month(), 10, 'set 4th quarter');
});

test('quarter setter plural', function (assert) {
    var m = moment([2014, 4, 11]);
    assert.equal(m.quarters(2).month(), 4, 'set same quarter');
    assert.equal(m.quarters(3).month(), 7, 'set 3rd quarter');
    assert.equal(m.quarters(1).month(), 1, 'set 1st quarter');
    assert.equal(m.quarters(4).month(), 10, 'set 4th quarter');
});

test('quarter setter programmatic', function (assert) {
    var m = moment([2014, 4, 11]);
    assert.equal(m.set('quarter', 2).month(), 4, 'set same quarter');
    assert.equal(m.set('quarter', 3).month(), 7, 'set 3rd quarter');
    assert.equal(m.set('quarter', 1).month(), 1, 'set 1st quarter');
    assert.equal(m.set('quarter', 4).month(), 10, 'set 4th quarter');
});

test('quarter setter programmatic plural', function (assert) {
    var m = moment([2014, 4, 11]);
    assert.equal(m.set('quarters', 2).month(), 4, 'set same quarter');
    assert.equal(m.set('quarters', 3).month(), 7, 'set 3rd quarter');
    assert.equal(m.set('quarters', 1).month(), 1, 'set 1st quarter');
    assert.equal(m.set('quarters', 4).month(), 10, 'set 4th quarter');
});

test('quarter setter programmatic abbr', function (assert) {
    var m = moment([2014, 4, 11]);
    assert.equal(m.set('Q', 2).month(), 4, 'set same quarter');
    assert.equal(m.set('Q', 3).month(), 7, 'set 3rd quarter');
    assert.equal(m.set('Q', 1).month(), 1, 'set 1st quarter');
    assert.equal(m.set('Q', 4).month(), 10, 'set 4th quarter');
});

test('quarter setter only month changes', function (assert) {
    var m = moment([2014, 4, 11, 1, 2, 3, 4]).quarter(4);
    assert.equal(m.year(), 2014, 'keep year');
    assert.equal(m.month(), 10, 'set month');
    assert.equal(m.date(), 11, 'keep date');
    assert.equal(m.hour(), 1, 'keep hour');
    assert.equal(m.minute(), 2, 'keep minutes');
    assert.equal(m.second(), 3, 'keep seconds');
    assert.equal(m.millisecond(), 4, 'keep milliseconds');
});

test('quarter setter bubble to next year', function (assert) {
    var m = moment([2014, 4, 11, 1, 2, 3, 4]).quarter(7);
    assert.equal(m.year(), 2015, 'year bubbled');
    assert.equal(m.month(), 7, 'set month');
    assert.equal(m.date(), 11, 'keep date');
    assert.equal(m.hour(), 1, 'keep hour');
    assert.equal(m.minute(), 2, 'keep minutes');
    assert.equal(m.second(), 3, 'keep seconds');
    assert.equal(m.millisecond(), 4, 'keep milliseconds');
});

test('quarter diff', function (assert) {
    assert.equal(moment('2014-01-01').diff(moment('2014-04-01'), 'quarter'),
            -1, 'diff -1 quarter');
    assert.equal(moment('2014-04-01').diff(moment('2014-01-01'), 'quarter'),
            1, 'diff 1 quarter');
    assert.equal(moment('2014-05-01').diff(moment('2014-01-01'), 'quarter'),
            1, 'diff 1 quarter');
    assert.ok(Math.abs((4 / 3) - moment('2014-05-01').diff(
                    moment('2014-01-01'), 'quarter', true)) < 0.00001,
            'diff 1 1/3 quarter');
    assert.equal(moment('2015-01-01').diff(moment('2014-01-01'), 'quarter'),
            4, 'diff 4 quarters');
});

test('quarter setter bubble to previous year', function (assert) {
    var m = moment([2014, 4, 11, 1, 2, 3, 4]).quarter(-3);
    assert.equal(m.year(), 2013, 'year bubbled');
    assert.equal(m.month(), 1, 'set month');
    assert.equal(m.date(), 11, 'keep date');
    assert.equal(m.hour(), 1, 'keep hour');
    assert.equal(m.minute(), 2, 'keep minutes');
    assert.equal(m.second(), 3, 'keep seconds');
    assert.equal(m.millisecond(), 4, 'keep milliseconds');
});
//===================================================================================================================================================================================================================== //>
module('relative time');

test('default thresholds fromNow', function (assert) {
    var a = moment();

    // Seconds to minutes threshold
    a.subtract(44, 'seconds');
    assert.equal(a.fromNow(), 'a few seconds ago', 'Below default seconds to minutes threshold');
    a.subtract(1, 'seconds');
    assert.equal(a.fromNow(), 'a minute ago', 'Above default seconds to minutes threshold');

    // Minutes to hours threshold
    a = moment();
    a.subtract(44, 'minutes');
    //assert.equal(a.fromNow(), '44 minutes ago', 'Below default minute to hour threshold');
    a.subtract(1, 'minutes');
    assert.equal(a.fromNow(), 'an hour ago', 'Above default minute to hour threshold');

    // Hours to days threshold
    a = moment();
    a.subtract(21, 'hours');
    //assert.equal(a.fromNow(), '21 hours ago', 'Below default hours to day threshold');
    a.subtract(1, 'hours');
    assert.equal(a.fromNow(), 'a day ago', 'Above default hours to day threshold');

    // Days to month threshold
    a = moment();
    a.subtract(25, 'days');
    //assert.equal(a.fromNow(), '25 days ago', 'Below default days to month (singular) threshold');
    a.subtract(1, 'days');
    assert.equal(a.fromNow(), 'a month ago', 'Above default days to month (singular) threshold');

    // months to year threshold
    a = moment();
    a.subtract(10, 'months');
    //assert.equal(a.fromNow(), '10 months ago', 'Below default days to years threshold');
    a.subtract(1, 'month');
    assert.equal(a.fromNow(), 'a year ago', 'Above default days to years threshold');
});

test('default thresholds toNow', function (assert) {
    var a = moment();

    // Seconds to minutes threshold
    a.subtract(44, 'seconds');
    assert.equal(a.toNow(), 'in a few seconds', 'Below default seconds to minutes threshold');
    a.subtract(1, 'seconds');
    assert.equal(a.toNow(), 'in a minute', 'Above default seconds to minutes threshold');

    // Minutes to hours threshold
    a = moment();
    a.subtract(44, 'minutes');
    //assert.equal(a.toNow(), 'in 44 minutes', 'Below default minute to hour threshold');
    a.subtract(1, 'minutes');
    assert.equal(a.toNow(), 'in an hour', 'Above default minute to hour threshold');

    // Hours to days threshold
    a = moment();
    a.subtract(21, 'hours');
    //assert.equal(a.toNow(), 'in 21 hours', 'Below default hours to day threshold');
    a.subtract(1, 'hours');
    assert.equal(a.toNow(), 'in a day', 'Above default hours to day threshold');

    // Days to month threshold
    a = moment();
    a.subtract(25, 'days');
    //assert.equal(a.toNow(), 'in 25 days', 'Below default days to month (singular) threshold');
    a.subtract(1, 'days');
    assert.equal(a.toNow(), 'in a month', 'Above default days to month (singular) threshold');

    // months to year threshold
    a = moment();
    a.subtract(10, 'months');
    //assert.equal(a.toNow(), 'in 10 months', 'Below default days to years threshold');
    a.subtract(1, 'month');
    assert.equal(a.toNow(), 'in a year', 'Above default days to years threshold');
});

test('custom thresholds', function (assert) {
    // Seconds to minutes threshold
    moment.relativeTimeThreshold('s', 55);

    var a = moment();
    a.subtract(54, 'seconds');
    assert.equal(a.fromNow(), 'a few seconds ago', 'Below custom seconds to minutes threshold');
    a.subtract(1, 'seconds');
    assert.equal(a.fromNow(), 'a minute ago', 'Above custom seconds to minutes threshold');

    moment.relativeTimeThreshold('s', 45);

    // Minutes to hours threshold
    moment.relativeTimeThreshold('m', 55);
    a = moment();
    a.subtract(54, 'minutes');
    //assert.equal(a.fromNow(), '54 minutes ago', 'Below custom minutes to hours threshold');
    a.subtract(1, 'minutes');
    assert.equal(a.fromNow(), 'an hour ago', 'Above custom minutes to hours threshold');
    moment.relativeTimeThreshold('m', 45);

    // Hours to days threshold
    moment.relativeTimeThreshold('h', 24);
    a = moment();
    a.subtract(23, 'hours');
    //assert.equal(a.fromNow(), '23 hours ago', 'Below custom hours to days threshold');
    a.subtract(1, 'hours');
    assert.equal(a.fromNow(), 'a day ago', 'Above custom hours to days threshold');
    moment.relativeTimeThreshold('h', 22);

    // Days to month threshold
    moment.relativeTimeThreshold('d', 28);
    a = moment();
    a.subtract(27, 'days');
    //assert.equal(a.fromNow(), '27 days ago', 'Below custom days to month (singular) threshold');
    a.subtract(1, 'days');
    assert.equal(a.fromNow(), 'a month ago', 'Above custom days to month (singular) threshold');
    moment.relativeTimeThreshold('d', 26);

    // months to years threshold
    moment.relativeTimeThreshold('M', 9);
    a = moment();
    a.subtract(8, 'months');
    //assert.equal(a.fromNow(), '8 months ago', 'Below custom days to years threshold');
    a.subtract(1, 'months');
    assert.equal(a.fromNow(), 'a year ago', 'Above custom days to years threshold');
    moment.relativeTimeThreshold('M', 11);
});

test('retrive threshold settings', function (assert) {
    moment.relativeTimeThreshold('m', 45);
    var minuteThreshold = moment.relativeTimeThreshold('m');

    assert.equal(minuteThreshold, 45, 'Can retrieve minute setting');
});
//===================================================================================================================================================================================================================== //>
