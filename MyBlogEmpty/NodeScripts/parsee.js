
var Parsoid = require('parsoid-jsapi');

module.exports = function (callback, wikiText)
{



    async function parseWiki(markup)
    {
        try 
        {
            // var pdoc = await Parsoid.parse(markup, { pdoc: true });
            // callback(null, pdoc.document.outerHTML);

            var data = await Parsoid.parse(markup);
            callback(null, data.out);

            // let data = await parsoid.parse(markup);
            //console.log(data.out);
            // callback(null, data.out);

            // data.out = null;
            // data.env.page.src = null;
            // console.log(data);
            // console.log(dumpObject(data));
            // callback(null, dumpObject(data));
        }
        catch (ex)
        {
            callback(ex, null);
        }
        
    }
    
    parseWiki(wikiText);
    
    /*
    parsoid.parse(mediaWiki)
        //.then(data => callback(null, data.out ) )
        //.then(data => callback(null, JSON.stringify(data)     ) )
        //.then(data => callback(null, JSON.stringify(data, censor(data), 2 )     ) )
        .then(data => callback(null, dumpObject(data)  ) )
        .catch (err => callback(err, null) );
    // callback(null, JSON.stringify(abc) );
    */
    
    // callback(err, null);
};
