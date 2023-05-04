import { Rating } from '@mui/material';
import Link from 'next/link';
export default function ResenaPelicula(){
    return (
    <div className='flex flex-row gap-5 pl-10 px-2 md:py-5'>
        <div className='flex flex-col gap-3 w-full md:w-3/4'>
            <div className='flex flex-row items-center gap-3 border-b-2 py-3'>
                <Link href={"/usuario/id"} className='flex flex-row items-center gap-5'>
                    <img className='rounded-full w-10 h-10 shadow-black shadow-lg' src="https://moviebox-pelis.s3.us-east-005.backblazeb2.com/Pfps/pepe2.jpg" alt="" />
                    <p className='md:text-xl'>Pepebc</p>
                </Link>
                <Rating className='!text-green-700 ' name="half-rating-read" defaultValue={2.5} precision={0.5} readOnly />
            </div>
            <div className='flex flex-row gap-3 md:gap-0'>
                <p className='text-sm md:text-lg'>La pelicula es muy buena me ha encantado y volvere a verla lo antes posible. Un 10, La pelicula es muy buena me ha encantado y volvere a verla lo antes posible. Un 10, La pelicula es muy buena me ha encantado y volvere a verla lo antes posible. Un 10</p>
            </div>
      </div>
    </div>
    )
}