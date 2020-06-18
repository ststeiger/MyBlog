
/*
// module.exports = function (a, b, callback) 
// This is old - new callback is first argument 
// Also, new callback is error in first argument 
// https://github.com/aspnet/JavaScriptServices/issues/1583
// https://github.com/aspnet/JavaScriptServices/tree/master/src/Microsoft.AspNetCore.NodeServices
module.exports = function (callback, a, b) 
{
    let result = a + b;
    // callback(result);
    let err = null;
    callback(err, result);
}; 
*/


module.exports = {

    add: function (callback, a, b) {
        let error = null;
        let result = null;

        try
        {
            result = a + b;
            if (isNaN(result) || !isFinite(result))
                throw new Error("not a number");
        }
        catch (err)
        {
            error = err.message;
        }
        
        callback(error, result);
    },

    divide: function (callback, a, b) {
        let error = null;
        let result = null;

        try
        {
            result = a / b;
            
            if (isNaN(result) || !isFinite(result))
                throw new Error("not a number");
        }
        catch (err)
        {
            error = err.message;
        }

        callback(error, result);
    }

};
