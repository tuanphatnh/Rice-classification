import React from 'react';
import { useState } from 'react';
import { NavLink } from 'react-router-dom';
function NavList(props) {
  const { links,expand } = props;
  const [linkActiveEx, setLinkActiveEx] = useState(0);

  return (
    <div className={expand ? "nav-list small" : "nav-list"}>
      <ul>
        {links.map((link, index) =>
          link.expend === false ? (
            <li key={index}>
              <NavLink
                title={link.title}
                to={link.link}
                className={({ isActive, isPending }) =>
                  isActive ? 'active' : isPending ? 'pending' : ''
                }>
                <div className="icon">
                  <i title={link.title} className={link.icon}></i>
                </div>
                <div className="title-link">{link.title}</div>
              </NavLink>
            </li>
          ) : (
            <div>
              <li>
                <span>
                  <div className="icon">
                    <i title={link.title} className={link.icon}></i>
                  </div>
                  <div className="title-link">{link.title}</div>

                  {link.expend !== false && (
                    <div className="icon end">
                      {linkActiveEx === index ? (
                        <i className="fas fa-lg fa-caret-up" onClick={()=>setLinkActiveEx(0)}></i>
                      ) : (
                        <i className="fas fa-lg fa-caret-down" onClick={()=>setLinkActiveEx(index)}></i>
                      )}
                    </div>
                  )}
                </span>
              </li>

              <ul
                className={
                  linkActiveEx === index ? expand ?  'ex-nav small' : 'ex-nav' : 'ex-nav nonactive'
                }>
                {link.expend.map((linkEx, indexEx) => (
                  <li key={indexEx + 'ex'}>
                    <NavLink
                      to={linkEx.link}
                      className={({ isActive, isPending }) =>
                        isActive ? 'active' : isPending ? 'pending' : ''
                      }>
                        {expand ?  <div className="icon">
                            <i title={linkEx.title} className={linkEx.icon + ' icon'}></i>
                          </div> : ''}
                          
                      <div className="title-link">{linkEx.title}</div>
                    </NavLink>
                  </li>
                ))}
              </ul>
            </div>
          )
        )}
      </ul>
    </div>
  );
}

export default NavList;
