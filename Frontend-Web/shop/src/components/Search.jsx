import React from 'react'
import {AiOutlineSearch} from 'react-icons/ai';

function Search() {
  return (
    <div className='search-tool'>
            <input className='search' type={'search'} placeholder='Search' />
            <i className='icon'> <AiOutlineSearch /></i>
    </div>
  )
}

export default Search 
