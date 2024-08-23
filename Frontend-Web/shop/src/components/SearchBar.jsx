import React from 'react'
import Avatar from './Avatar'
import Notification from './Notification'
import Search from './Search'
function SearchBar(props) {
  const {expand} = props
  return (
    <div className={ expand ? 'searchbar expand' : 'searchbar'}>
       <Search />
       <div className='right-tool'>
       <Notification />
       <Avatar />
       </div>
      
    </div>
  )
}

export default SearchBar
