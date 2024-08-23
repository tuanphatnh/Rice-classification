import classNames from 'classnames/bind';
import styles from './Login.module.scss';

import { useEffect, useRef, useState } from 'react';
import googleIcon from '~/assets/icon/google.png';
import facebookIcon from '~/assets/icon/facebook.png';
import { EyeOutlined, EyeInvisibleOutlined } from '@ant-design/icons';
import { Link, json } from 'react-router-dom';
import { useCookies } from 'react-cookie';
import { useNavigate } from 'react-router-dom';
import { message } from 'antd';

import axios from 'axios';
import { useDispatch, useSelector } from 'react-redux';
import { addUser } from '~/redux/actions/auth';
import Loading from '~/components/Global/Loading';

const cx = classNames.bind(styles);

function Login() {
    const navigate = useNavigate();
    const user = useSelector((state) => state.user);
    const dispatch = useDispatch();

    const [cookies, setCookie] = useCookies(['access_token']);
    const [isLoader, setIsLoader] = useState(false);
    const [isPassword, setIsPw] = useState(false);
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const usernameRef = useRef(null);
    const passwordRef = useRef(null);
    const logUsernameReg = useRef(null);
    const logPassowrdReg = useRef(null);

    const validator = () => {
        if (!username) {
            usernameRef.current.focus();
            logUsernameReg.current.style.display = 'inline';
            return false;
        } else {
            logUsernameReg.current.style.display = 'none';
        }

        if (password === '') {
            passwordRef.current.focus();
            logPassowrdReg.current.style.display = 'inline';
            passwordRef.current.innerHTML = 'Please enter a password';
            return false;
        } else {
            logPassowrdReg.current.style.display = 'none';
        }

        if (password.length < 6) {
            passwordRef.current.focus();
            logPassowrdReg.current.style.display = 'inline';
            logPassowrdReg.current.innerHTML = 'Password is shorter than 6 characters';
            return false;
        } else {
            logPassowrdReg.current.style.display = 'none';
        }

        return true;
    };

    const submitLogin = () => {
        if (validator()) {
            setUsername('');
            setPassword('');
            login();
        }
    };

    const login = () => {
        const formData = {
            username: username,
            password: password,
        };
        setIsLoader(true);
        axios
            .post(`https://localhost:44352/api/Auth/login`, JSON.stringify(formData), {
                headers: {
                    'content-type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                },
            })
            .then((res) => {
                if (res.status === 200) {
                    message.success('Successful Login');
                    let expires = new Date();
                    expires.setTime(expires.getTime() + res.data.expireDate * 1000);
                    setCookie('access_token', res.data.token, { path: '/', expires });
                    getProfile(res.data.token);
                    setIsLoader(false);
                }
            })
            .catch((err) => {
                setIsLoader(false);
                if (err.status === 400) {
                    message.error(err.response.data.message);
                }
            });
    };

    const getProfile = (token) => {
        axios
            .get(`https://localhost:44352/api/User/getMyProfile`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                    'content-type': 'application/json',
                },
            })
            .then((res) => {
                dispatch(addUser(res.data));
                let expires = new Date();
                expires.setTime(expires.getTime() + res.data.expireDate * 1000);
                const user = {
                    id: res.data.id,
                    username: res.data.username,
                    email: res.data.email,
                    avatarUrl: res.data.avatarUrl,
                };
                const data = JSON.stringify(user);
                setCookie('user', data, { path: '/', expires });
                navigate('/home');
            })
            .catch((err) => console.log(err));
    };

    return (
        <div className={cx('wrap')}>
            <div className={cx('box')}>
                <h1 className="text-center text-4xl">Login</h1>
                <div className={cx('group')}>
                    <div className={cx('item')}>
                        <input
                            ref={usernameRef}
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            type="text"
                            placeholder="Enter your username"
                            onKeyDown={(e) => e.key === 'Enter' && submitLogin()}
                        />
                        <span ref={logUsernameReg} className={cx('item-log')} style={{ display: 'none' }}>
                            Please enter your username
                        </span>
                    </div>
                    <div className={cx('item', 'password')}>
                        <input
                            ref={passwordRef}
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            type={isPassword ? 'text' : 'password'}
                            placeholder="Password"
                            onKeyDown={(e) => e.key === 'Enter' && submitLogin()}
                        />
                        <div className={cx('icon')}>
                            {isPassword ? (
                                <EyeOutlined onClick={() => setIsPw(false)} />
                            ) : (
                                <EyeInvisibleOutlined onClick={() => setIsPw(true)} />
                            )}
                        </div>
                        <span ref={logPassowrdReg} className={cx('item-log')} style={{ display: 'none' }}>
                            Please enter a password
                        </span>
                    </div>
                    <div className={cx('item')}>
                        <Link to={'/forgot'}>
                            <span className={cx('recovery')}>Recovery Password</span>
                        </Link>
                    </div>
                    <div className={cx('item')}>
                        <button className={cx('btn')} onClick={() => submitLogin()}>
                            Sign In
                        </button>
                    </div>
                    <div className={cx('ruler-bar')}>
                        <div className={cx('ruler', 'left')}></div>
                        <span>Or</span>
                        <div className={cx('ruler', 'right')}></div>
                    </div>
                    <div className={cx('oauth2')}>
                        <img src={googleIcon} alt="icon google" />
                        <img src={facebookIcon} alt="icon facebook" />
                    </div>
                    <div className={cx('register')}>
                        <h4>
                            Not a member?
                            <Link to="/register">
                                <span className="ml-3">Register now</span>
                            </Link>
                        </h4>
                    </div>
                </div>
            </div>
            {isLoader && <Loading />}
        </div>
    );
}

export default Login;
