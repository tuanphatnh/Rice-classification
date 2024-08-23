import React from 'react';
import { useEffect } from 'react';
import { useState } from 'react';
import { instance } from '../config/axiosConfig';

function CategoryFilter() {
  const [data,setData] = useState([])

  useEffect(() => {
      instance.get('/Category/getAll').then(rs => setData(rs.data))
      .catch(e => console.log(e))
  }, [])
  return (
    <>
      <div className="brand-filter">
        <span className="title">Category</span>
        <div className="list-brand">
          <button style={{ fontSize: '1em', padding: '10px' }}>
            All Category
          </button>
          {data.map((data,index) => 
             <button>
             {' '}
             <img
               src={data.imageUrl} alt='' 
             />
           </button>
          )
          }
        </div>
      </div>
    </>
  );
}

export default CategoryFilter;
