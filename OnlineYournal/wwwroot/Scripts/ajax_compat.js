
  ////////////////////////////////////////////////
 //   https://api.jquery.com/jquery.getjson/   //
////////////////////////////////////////////////
$.getJSON = function (u, d, s)
{
    if (typeof (d) == "function")
    { s = d; d = null; }

    return $.ajax({
        dataType: "json"
           , url: u
           , data: d
           , success: s
           , error: function (err)
           {
               console.log("ajax_compat.js: error $.getJSON");
               console.log(err);
           }
    });
};


  ////////////////////////////////////////////
 //   https://api.jquery.com/jquery.get/   //
////////////////////////////////////////////
$.get = function (u, d, s, t)
{
    if (typeof (d) == "function")
    { t = s; s = d; d = null; }

    return $.ajax({
        url: u
      , data: d
      , success: s
      , error: function (err)
      {
          console.log("ajax_compat.js: error $.get");
          console.log(err);
      }
      , dataType: t
    });
};


  /////////////////////////////////////////////
 //   https://api.jquery.com/jquery.post/   //
/////////////////////////////////////////////
$.post = function (u, d, s, t)
{
    if (typeof (d) == "function")
    { t = s; s = d; d = null; }

    return $.ajax({
        method: "POST"
      , url: u
      , data: d
      , success: s
      , error: function (err)
      {
          console.log("ajax_compat.js: error $.post");
          console.log(err);
      }
      , dataType: t
    });
};


$.http = function (method, url, d)
{
    return $.ajax({
        url: url // '/2013/12/01/1234'
      //,method: 'get'
		,method: method
		,data: d
      // , type: 'jsonp'
      // , jsonpCallback: 'foo'
    });
}


$.jsonp = function (u, params, cb, cbn, fn)
{
    var sep = "?", ps = "";
    if (params != null)
    {
        if (u !== null && u.indexOf("?") !== -1) sep = "&";
        for (var key in params)
        {
            // important check that this is objects own property
            // not from prototype prop inherited
            if (params.hasOwnProperty(key))
            {
                ps += sep + encodeURIComponent(key) + "=" + encodeURIComponent(params[key]);
                sep = "&";
            }
        } // Next key
    } // End if(params != null)

    return $.ajax({
        url: (u + ps)
      , type: 'jsonp'
      , jsonpCallback: cb
      , jsonpCallbackName: cbn
     , success: fn
        //,error : function(err) { console.log("error $.jsonp");console.log(err);}
    });
};
