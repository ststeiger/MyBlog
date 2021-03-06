
interface Point
{
    x: number;
    y: number;
}


interface Points
{
    hex: Point[];
    hl: Point[];
}


// coz me lazy
// Object.getOwnPropertyNames(Math).map(function (p){ (<any>window)[p] = (<any>Math)[p]; });




let HEX_CRAD = 32,
    HEX_BG = '#171717',
    // HEX_BG = '#00FF0000',
    // HEX_BG = '#F0AA00',
    HEX_HL = '#2a2a2a',
    HEX_HLW = 2,
    HEX_GAP = 4,
    NEON_PALETE = [
        '#cb3301', '#ff0066',
        '#ff6666', '#feff99',
        '#ffff67', '#ccff66',
        '#99fe00', '#fe99ff',
        '#ff99cb', '#fe349a',
        '#cc99fe', '#6599ff',
        '#00ccff', '#ffffff'
    ],
    T_SWITCH = 64,

    unit_x = 3 * HEX_CRAD + HEX_GAP * Math.sqrt(3),
    unit_y = HEX_CRAD * Math.sqrt(3) * 0.5 + 0.5 * HEX_GAP,
    off_x = 1.5 * HEX_CRAD + HEX_GAP * Math.sqrt(3) * 0.5,

    // extract a work palette
    wp = NEON_PALETE.map(function (c)
    {
        let num = parseInt(c.replace('#', ''), 16);

        return {
            'r': num >> 16 & 0xFF,
            'g': num >> 8 & 0xFF,
            'b': num & 0xFF
        };
    }),
    nwp = wp.length, csi = 0,

    f = 1 / T_SWITCH,

    c = document.querySelectorAll('canvas'),
    n = c.length, w: number, h: number, _min: number,
    ctx: CanvasRenderingContext2D[] = [],
    grid, source: Point = null,
    t = 0, request_id = null;






class GridItem
{
    protected x: number;
    protected y: number;
    protected points: Points;

    constructor(x: number, y: number)
    {
        this.x = x || 0;
        this.y = y || 0;
        this.points = { 'hex': [], 'hl': [] };

        this.init();
    }


    public init()
    {
        let x, y, a, ba = Math.PI / 3,
            ri = HEX_CRAD - 0.5 * HEX_HLW;

        for (let i = 0; i < 6; i++)
        {
            a = i * ba;
            x = this.x + HEX_CRAD * Math.cos(a);
            y = this.y + HEX_CRAD * Math.sin(a);

            this.points.hex.push({
                'x': x,
                'y': y
            });

            if (i > 2)
            {
                x = this.x + ri * Math.cos(a);
                y = this.y + ri * Math.sin(a);

                this.points.hl.push({
                    'x': x,
                    'y': y
                });
            }
        }
    }

    public draw(ct: CanvasRenderingContext2D)
    {
        for (let i = 0; i < 6; i++)
        {
            (<any>ct)[(i === 0 ? 'move' : 'line') + 'To'](
                this.points.hex[i].x,
                this.points.hex[i].y
            );
        }
    }


    public highlight(ct: CanvasRenderingContext2D)
    {
        for (let i = 0; i < 3; i++)
        {
            (<any>ct)[(i === 0 ? 'move' : 'line') + 'To'](
                this.points.hl[i].x,
                this.points.hl[i].y
            );
        }
    }

}


class Grid
{

    private cols: number;
    private rows: number;
    private n: number;
    private items: GridItem[];


    constructor(rows: number, cols: number)
    {
        this.cols = cols || 16;
        this.rows = rows || 16;
        this.items = [];
        this.n = this.items.length;

        this.init = this.init.bind(this);
        this.draw = this.draw.bind(this);

        this.init();
    }

    public init()
    {
        let x: number, y: number;

        for (let row = 0; row < this.rows; row++)
        {
            y = row * unit_y;

            for (let col = 0; col < this.cols; col++)
            {
                x = ((row % 2 == 0) ? 0 : off_x) + col * unit_x;

                this.items.push(new GridItem(x, y));
            }
        }

        this.n = this.items.length;
    }

    public draw(ct: CanvasRenderingContext2D)
    {
        ct.fillStyle = HEX_BG;
        ct.beginPath();

        for (let i = 0; i < this.n; i++)
        {
            this.items[i].draw(ct);
        }

        ct.closePath();
        ct.fill();

        ct.strokeStyle = HEX_HL;
        ct.beginPath();

        for (let i = 0; i < this.n; i++)
        {
            this.items[i].highlight(ct);
        }

        ct.closePath();
        ct.stroke();
    }
}



function init()
{
    let s = getComputedStyle(c[0]),
        rows, cols;

    // That ~~is a double NOT bitwise operator.
    // It is used as a faster substitute for Math.floor().

    w = ~~s.width.split('px')[0];
    h = ~~s.height.split('px')[0];
    _min = .75 * Math.min(w, h);

    rows = ~~(h / unit_y) + 2;
    cols = ~~(w / unit_x) + 2;

    for (let i = 0; i < n; i++)
    {
        c[i].width = w;
        c[i].height = h;
        ctx[i] = c[i].getContext('2d');
    }

    grid = new Grid(rows, cols);
    grid.draw(ctx[1]);

    if (!source)
    {
        source = { 'x': ~~(w / 2), 'y': ~~(h / 2) };
    }

    neon();
}

function neon()
{
    let k = (t % T_SWITCH) * f,
        rgb = {
            'r': ~~(wp[csi].r * (1 - k) +
                wp[(csi + 1) % nwp].r * k),
            'g': ~~(wp[csi].g * (1 - k) +
                wp[(csi + 1) % nwp].g * k),
            'b': ~~(wp[csi].b * (1 - k) +
                wp[(csi + 1) % nwp].b * k)
        },
        rgb_str = 'rgb(' + rgb.r + ',' + rgb.g + ',' + rgb.b + ')',
        light = ctx[0].createRadialGradient(
            source.x, source.y, 0,
            source.x, source.y, .875 * _min
        ), stp;

    stp = .5 - .5 * Math.sin(7 * t * f) * Math.cos(5 * t * f) * Math.sin(3 * t * f);

    light.addColorStop(0, rgb_str);
    light.addColorStop(stp, 'rgba(0,0,0,.03)');

    fillBackground('rgba(0,0,0,.02)');
    // fillBackground('rgba(255,255,255,.02)');
    fillBackground(light);

    t++;

    if (t % T_SWITCH === 0)
    {
        csi++;

        if (csi === nwp)
        {
            csi = 0;
            t = 0;
        }
    }

    request_id = requestAnimationFrame(neon);
}

function fillBackground(bg_fill: string | CanvasGradient)
{
    ctx[0].fillStyle = bg_fill;
    ctx[0].beginPath();
    ctx[0].rect(0, 0, w, h);
    ctx[0].closePath();
    ctx[0].fill();
}

init();

addEventListener('resize', init, false);

addEventListener('mousemove', function (e: MouseEvent)
{
    source = { 'x': e.clientX, 'y': e.clientY };
}, false);
