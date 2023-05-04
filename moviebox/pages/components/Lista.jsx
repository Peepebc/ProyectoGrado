export default function Lista(){
    return (
    <div className='flex flex-col md:flex-row px-2 py-5 md:py-5 md:w-1/3'>
        <div className="flex flex-row w-80">
            <img className='h-52 rounded-xl shadow-black shadow-lg relative -right-5' src="https://moviebox-pelis.s3.us-east-005.backblazeb2.com/Peliculas/1580999639_454991_1581001437_album_normal.jpg" alt="" />
            <img className='h-52 rounded-xl shadow-black shadow-lg relative right-14 -z-10' src="https://moviebox-pelis.s3.us-east-005.backblazeb2.com/Peliculas/17a2822cc31817347eb10959d0bb9163.jpg" alt="" />
            <img className='h-52 rounded-xl shadow-black shadow-lg relative right-32 -z-20' src="https://moviebox-pelis.s3.us-east-005.backblazeb2.com/Peliculas/A1JVqNMI7UL._SL1500_.jpg" alt="" />
        </div>
        <div className='inline-flex flex-col text-center md:text-start p-3 '>
            <p className="text-xl">NOMBRE DE LA LISTA</p>
            <p className="">5 FILMS</p>
        </div>
    </div>
    )
}