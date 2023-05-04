import React from 'react';
import Resena from './Resena';
import Link from 'next/link';
export default function UltimasResenas() {
  return (
    <section className='p-10'>
      <div className='flex justify-between border-b-2 px-3 mb-5'>
        <p>ULTIMAS RESEÑAS</p>
        <Link href={'/resenas'}> VER TODAS</Link>
      </div>
      <div className='flex flex-wrap gap-5 justify-evenly'>
        <Resena/>
        <Resena/>
        <Resena/>
        <Resena/>
      </div>
    </section>
  );
}