import { Rating } from '@mui/material';
import Link from 'next/link';
export default function Resena(){
    return (
    <div className='flex flex-row gap-5 px-2 md:py-5 md:w-1/3'>
      <Link href={"#"}><img className='w-96 rounded-xl shadow-black shadow-lg hidden md:block' src="https://moviebox-pelis.s3.us-east-005.backblazeb2.com/Peliculas/1580999639_454991_1581001437_album_normal.jpg" alt="" /></Link>
      <div className='flex flex-col gap-3 md:w-2/3'>
        <div className='flex items-center gap-3 border-b-2 py-3'>
        <Link href={"/usuario/id"} className='flex flex-row gap-5 items-center'>
          <img className='rounded-full w-10 h-10 shadow-black shadow-lg' src="https://moviebox-pelis.s3.us-east-005.backblazeb2.com/Pfps/pepe2.jpg" alt="" />
          <p className='md:text-xl'>Pepebc</p>
        </Link>
          <Rating className='!text-green-700 ' name="half-rating-read" defaultValue={2.5} precision={0.5} readOnly />
        </div>
        <div className='w-96 flex flex-row gap-3 md:gap-0'>
            <Link href={"#"}><img className='rounded-xl shadow-black shadow-lg md:hidden' src="https://moviebox-pelis.s3.us-east-005.backblazeb2.com/Peliculas/1580999639_454991_1581001437_album_normal.jpg" alt="" /></Link>
            <p className='max-w-md text-sm md:text-lg'>La pelicula es muy buena me ha encantado y volvere a verla lo antes posible. Un 10, La pelicula es muy buena me ha encantado y volvere a verla lo antes posible. Un 10, La pelicula es muy buena me ha encantado y volvere a verla lo antes posible. Un 10</p>
        </div>
      </div>
    </div>
    )
}