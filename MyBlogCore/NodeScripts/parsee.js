
require('parsoid/core-upgrade.js');

const parseJs = require('parsoid/lib/parse.js');
const jsdom = require("jsdom").JSDOM;


function convertWikiToHtml(input, options, optCb)
{
	var argv = Object.assign({},
		{
		/* default options */
	}, options || {});

	if (argv.pdoc)
	{
		argv.document = true;
	}

	if (argv.selser)
	{
		argv.html2wt = true;
	}

	// Default conversion mode
	if (!argv.html2wt && !argv.wt2wt && !argv.html2html)
	{
		argv.wt2html = true;
	}

	return parseJs({
		input: input || '',
		mode: (
			argv.wt2html ? 'wt2html' :
				argv.html2wt ? 'html2wt' :
					argv.html2html ? 'html2html' :
						argv.wt2wt ? 'wt2wt' :
							'<unknown mode>'
		),
		parsoidOptions: Object.assign({
			useWorker: false,
			addHTMLTemplateParameters: true,
			loadWMF: true,
		}, options.parsoidOptions || {}),
		envOptions: Object.assign({
			domain: argv.domain || 'en.wikipedia.org',
			pageName: argv.pageName,
			wrapSections: true,
		}, options.envOptions || {}),
		returnDocument: argv.pdoc || argv.document,
	}).then(function (res)
	{
		// The ability to return as an HTML Document used to be in core :(
		// return argv.pdoc ? new JsApi.PDoc(res.env, res.doc) : res;
		return res; //res.env; // res.doc;
	}).nodify(optCb);
};


async function debugParseWiki(markup)
{
    try 
    {
        var options = Object.assign({}, options, { pdoc: true });
        let data = await convertWikiToHtml(markup, options);

        // console.log(data);

        let html = data.html;
        const dom = new jsdom(html);
        let fragment = dom.window.document.body.innerHTML;
        console.log(fragment);
        // callback(null, fragment);


        // const dom = new jsdom(`<!DOCTYPE html><body><p id="main">My First JSDOM!</p></body>`);
        // console.log(dom);
        // dom.window.document.innerHTML

        
        // console.log(data.html);
        // console.log(data.contentmodel);
        // console.log(data.headers);


        // console.log(data.html);
        //console.log(data.out);
        // callback(null, data);

        // data.out = null;
        // data.env.page.src = null;
        // console.log(data);
        // console.log(dumpObject(data));
        // callback(null, dumpObject(data));
    }
    catch (ex)
    {
        console.log("error", ex);

        //callback(ex, null);
    }

}

// node ./parsee.js
// debugParseWiki("hello world");


module.exports = function (callback, wikiText)
{
    // callback(null, '{ "abc":"def", "ghi":123}'); // error - object returned is not a json-object
    // callback(null, { "abc": "def", "ghi": 1234 });

    // callback(null, mediaWiki);


    async function parseWiki(markup)
    {
        try 
		{
			var options = Object.assign({}, options, { pdoc: true });
            let data = await convertWikiToHtml(markup, options);
            let html = data.html;
            const dom = new jsdom(html);
            let fragment = dom.window.document.body.innerHTML;
            // console.log(fragment);
            callback(null, fragment);

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
