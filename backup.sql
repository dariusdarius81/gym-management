--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.2

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: add_payment_utility(character varying, integer, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.add_payment_utility(p_luna character varying, p_chirie integer, p_utilitati character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO PAYMENT_UTILITIES (LUNA, CHIRIE, UTILITATI)
    VALUES (p_luna, p_chirie, p_utilitati);
END;
$$;


ALTER FUNCTION public.add_payment_utility(p_luna character varying, p_chirie integer, p_utilitati character varying) OWNER TO postgres;

--
-- Name: delete_abonament_by_type(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.delete_abonament_by_type(p_membership_type character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    DELETE FROM ABONAMENT
    WHERE MEMBERSHIP_TYPE = p_membership_type;
END;
$$;


ALTER FUNCTION public.delete_abonament_by_type(p_membership_type character varying) OWNER TO postgres;

--
-- Name: delete_echipament_by_id(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.delete_echipament_by_id(p_id integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    DELETE FROM ECHIPAMENTE
    WHERE ID = p_id;
END;
$$;


ALTER FUNCTION public.delete_echipament_by_id(p_id integer) OWNER TO postgres;

--
-- Name: delete_furnizor_by_name_and_location(character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.delete_furnizor_by_name_and_location(p_nume character varying, p_locatie character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
DECLARE
    v_furnizor_id INT;
BEGIN
    -- Get the id_furnizor corresponding to the given name and location
    SELECT id_furnizor INTO v_furnizor_id
    FROM FURNIZORI
    WHERE NUME = p_nume AND LOCATIE = p_locatie;

    -- Remove the corresponding id_furnizor from the ECHIPAMENTE table
    UPDATE ECHIPAMENTE
    SET ID_FURNIZOR = NULL
    WHERE ID_FURNIZOR = v_furnizor_id;

    -- Delete the record from the FURNIZORI table
    DELETE FROM FURNIZORI
    WHERE NUME = p_nume AND LOCATIE = p_locatie;
END;
$$;


ALTER FUNCTION public.delete_furnizor_by_name_and_location(p_nume character varying, p_locatie character varying) OWNER TO postgres;

--
-- Name: delete_member_and_user(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.delete_member_and_user(p_user_id integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
DECLARE
    member_exists BOOLEAN;
BEGIN
    SELECT EXISTS(SELECT 1 FROM MEMBER WHERE user_id = p_user_id) INTO member_exists;

    IF member_exists THEN
        DELETE FROM MEMBER WHERE user_id = p_user_id;
    END IF;
	DELETE FROM USERS WHERE user_id = p_user_id;
END;
$$;


ALTER FUNCTION public.delete_member_and_user(p_user_id integer) OWNER TO postgres;

--
-- Name: delete_payment_utility(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.delete_payment_utility(p_id_pay integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    DELETE FROM PAYMENT_UTILITIES
    WHERE ID_PAY = p_id_pay;
END;
$$;


ALTER FUNCTION public.delete_payment_utility(p_id_pay integer) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: abonament; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.abonament (
    membership_id integer NOT NULL,
    membership_type character varying(20)
);


ALTER TABLE public.abonament OWNER TO postgres;

--
-- Name: get_all_ab(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_all_ab() RETURNS SETOF public.abonament
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY SELECT * FROM abonament;
    RETURN;
END;
$$;


ALTER FUNCTION public.get_all_ab() OWNER TO postgres;

--
-- Name: echipamente; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.echipamente (
    id integer NOT NULL,
    nume character varying(20),
    pret integer,
    grupa_muschi character varying(20),
    nr_bucati integer,
    id_furnizor integer,
    CONSTRAINT echipamente_pret_check CHECK ((pret >= 0))
);


ALTER TABLE public.echipamente OWNER TO postgres;

--
-- Name: get_all_echipamente(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_all_echipamente() RETURNS SETOF public.echipamente
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY SELECT * FROM ECHIPAMENTE;
    RETURN;
END;
$$;


ALTER FUNCTION public.get_all_echipamente() OWNER TO postgres;

--
-- Name: furnizori; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.furnizori (
    id_furnizor integer NOT NULL,
    nume character varying(20),
    locatie character varying(20)
);


ALTER TABLE public.furnizori OWNER TO postgres;

--
-- Name: get_all_furnizori(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_all_furnizori() RETURNS SETOF public.furnizori
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY SELECT * FROM FURNIZORI;
    RETURN;
END;
$$;


ALTER FUNCTION public.get_all_furnizori() OWNER TO postgres;

--
-- Name: member; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.member (
    user_id integer,
    member_id integer NOT NULL,
    membership_id integer
);


ALTER TABLE public.member OWNER TO postgres;

--
-- Name: get_all_members(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_all_members() RETURNS SETOF public.member
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY SELECT * FROM MEMBER;
    RETURN;
END;
$$;


ALTER FUNCTION public.get_all_members() OWNER TO postgres;

--
-- Name: payment_utilities; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.payment_utilities (
    id_pay integer NOT NULL,
    luna character varying(10),
    chirie integer,
    utilitati character varying(20)
);


ALTER TABLE public.payment_utilities OWNER TO postgres;

--
-- Name: get_all_payment_utilities(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_all_payment_utilities() RETURNS SETOF public.payment_utilities
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY SELECT * FROM PAYMENT_UTILITIES;
    RETURN;
END;
$$;


ALTER FUNCTION public.get_all_payment_utilities() OWNER TO postgres;

--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    user_id integer NOT NULL,
    nume character varying(10),
    prenume character varying(10),
    parola character varying(15),
    contact character varying(11)
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: get_all_users(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_all_users() RETURNS SETOF public.users
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY SELECT * FROM USERS;
    RETURN;
END;
$$;


ALTER FUNCTION public.get_all_users() OWNER TO postgres;

--
-- Name: insert_abonament(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.insert_abonament(p_membership_type character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO ABONAMENT (MEMBERSHIP_TYPE)
    VALUES (p_membership_type);
END;
$$;


ALTER FUNCTION public.insert_abonament(p_membership_type character varying) OWNER TO postgres;

--
-- Name: insert_echipament(character varying, integer, character varying, integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.insert_echipament(p_nume character varying, p_pret integer, p_grupa_muschi character varying, p_nr_bucati integer, p_id_furnizor integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO ECHIPAMENTE (NUME, PRET, GRUPA_MUSCHI, NR_BUCATI, ID_FURNIZOR)
    VALUES (p_nume, p_pret, p_grupa_muschi, p_nr_bucati, p_id_furnizor);
END;
$$;


ALTER FUNCTION public.insert_echipament(p_nume character varying, p_pret integer, p_grupa_muschi character varying, p_nr_bucati integer, p_id_furnizor integer) OWNER TO postgres;

--
-- Name: insert_furnizor(character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.insert_furnizor(p_nume character varying, p_locatie character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO FURNIZORI (NUME, LOCATIE)
    VALUES (p_nume, p_locatie);
END;
$$;


ALTER FUNCTION public.insert_furnizor(p_nume character varying, p_locatie character varying) OWNER TO postgres;

--
-- Name: insert_member(integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.insert_member(p_user_id integer, p_membership_id integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO MEMBER (user_id, MEMBERSHIP_ID)
    VALUES (p_user_id, p_membership_id);
END;
$$;


ALTER FUNCTION public.insert_member(p_user_id integer, p_membership_id integer) OWNER TO postgres;

--
-- Name: insert_user(character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.insert_user(p_nume character varying, p_prenume character varying, p_parola character varying, p_contact character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO USERS (NUME, PRENUME, PAROLA, CONTACT)
    VALUES (p_nume, p_prenume, p_parola, p_contact);
END;
$$;


ALTER FUNCTION public.insert_user(p_nume character varying, p_prenume character varying, p_parola character varying, p_contact character varying) OWNER TO postgres;

--
-- Name: modify_furnizor_by_id(integer, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.modify_furnizor_by_id(p_id_furnizor integer, p_locatie character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    UPDATE FURNIZORI
    SET LOCATIE = p_locatie
    WHERE id_furnizor = p_id_furnizor;
END;
$$;


ALTER FUNCTION public.modify_furnizor_by_id(p_id_furnizor integer, p_locatie character varying) OWNER TO postgres;

--
-- Name: modify_membership_id(integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.modify_membership_id(p_user_id integer, p_membership_id integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    UPDATE MEMBER
    SET membership_id = p_membership_id
    WHERE user_id = p_user_id;
END;
$$;


ALTER FUNCTION public.modify_membership_id(p_user_id integer, p_membership_id integer) OWNER TO postgres;

--
-- Name: modify_nr_bucati_by_id(integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.modify_nr_bucati_by_id(p_id integer, p_nr_bucati integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    UPDATE ECHIPAMENTE
    SET NR_BUCATI = p_nr_bucati
    WHERE ID = p_id;
END;
$$;


ALTER FUNCTION public.modify_nr_bucati_by_id(p_id integer, p_nr_bucati integer) OWNER TO postgres;

--
-- Name: modify_user_contact_by_id(integer, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.modify_user_contact_by_id(p_user_id integer, p_contact character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    UPDATE USERS
    SET CONTACT = p_contact
    WHERE user_id = p_user_id;
END;
$$;


ALTER FUNCTION public.modify_user_contact_by_id(p_user_id integer, p_contact character varying) OWNER TO postgres;

--
-- Name: abonament_membership_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.abonament_membership_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.abonament_membership_id_seq OWNER TO postgres;

--
-- Name: abonament_membership_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.abonament_membership_id_seq OWNED BY public.abonament.membership_id;


--
-- Name: echipamente_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.echipamente_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.echipamente_id_seq OWNER TO postgres;

--
-- Name: echipamente_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.echipamente_id_seq OWNED BY public.echipamente.id;


--
-- Name: furnizori_id_furnizor_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.furnizori_id_furnizor_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.furnizori_id_furnizor_seq OWNER TO postgres;

--
-- Name: furnizori_id_furnizor_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.furnizori_id_furnizor_seq OWNED BY public.furnizori.id_furnizor;


--
-- Name: member_member_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.member_member_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.member_member_id_seq OWNER TO postgres;

--
-- Name: member_member_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.member_member_id_seq OWNED BY public.member.member_id;


--
-- Name: payment_utilities_id_pay_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.payment_utilities_id_pay_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.payment_utilities_id_pay_seq OWNER TO postgres;

--
-- Name: payment_utilities_id_pay_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.payment_utilities_id_pay_seq OWNED BY public.payment_utilities.id_pay;


--
-- Name: suplimente; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.suplimente (
    id integer NOT NULL,
    nume character varying(20),
    data_expirare date,
    pret integer,
    id_furnizor integer,
    nr_bucati integer,
    CONSTRAINT suplimente_pret_check CHECK ((pret >= 0))
);


ALTER TABLE public.suplimente OWNER TO postgres;

--
-- Name: suplimente_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.suplimente_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.suplimente_id_seq OWNER TO postgres;

--
-- Name: suplimente_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.suplimente_id_seq OWNED BY public.suplimente.id;


--
-- Name: users_user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_user_id_seq OWNER TO postgres;

--
-- Name: users_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_user_id_seq OWNED BY public.users.user_id;


--
-- Name: abonament membership_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.abonament ALTER COLUMN membership_id SET DEFAULT nextval('public.abonament_membership_id_seq'::regclass);


--
-- Name: echipamente id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.echipamente ALTER COLUMN id SET DEFAULT nextval('public.echipamente_id_seq'::regclass);


--
-- Name: furnizori id_furnizor; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.furnizori ALTER COLUMN id_furnizor SET DEFAULT nextval('public.furnizori_id_furnizor_seq'::regclass);


--
-- Name: member member_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.member ALTER COLUMN member_id SET DEFAULT nextval('public.member_member_id_seq'::regclass);


--
-- Name: payment_utilities id_pay; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payment_utilities ALTER COLUMN id_pay SET DEFAULT nextval('public.payment_utilities_id_pay_seq'::regclass);


--
-- Name: suplimente id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.suplimente ALTER COLUMN id SET DEFAULT nextval('public.suplimente_id_seq'::regclass);


--
-- Name: users user_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN user_id SET DEFAULT nextval('public.users_user_id_seq'::regclass);


--
-- Data for Name: abonament; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.abonament (membership_id, membership_type) FROM stdin;
1	Gold
3	Silver
4	Bronze
5	Diamond
\.


--
-- Data for Name: echipamente; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.echipamente (id, nume, pret, grupa_muschi, nr_bucati, id_furnizor) FROM stdin;
4	bench	200	Upper Body	12	3
6	ceva	12	Upper Body	13	3
5	test	1231	Lower Body	12	3
7	da	134	Upper Body	15	3
\.


--
-- Data for Name: furnizori; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.furnizori (id_furnizor, nume, locatie) FROM stdin;
3	MyProtein	Ploiesti
\.


--
-- Data for Name: member; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.member (user_id, member_id, membership_id) FROM stdin;
8	5	3
9	6	1
6	3	5
\.


--
-- Data for Name: payment_utilities; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.payment_utilities (id_pay, luna, chirie, utilitati) FROM stdin;
\.


--
-- Data for Name: suplimente; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.suplimente (id, nume, data_expirare, pret, id_furnizor, nr_bucati) FROM stdin;
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (user_id, nume, prenume, parola, contact) FROM stdin;
2	admin	a	admin123	
3	a	a	admin123	
6	theo	t	123	124
8	alex	lamba	123	
9	darius	fratila	123	ceva
11	ceva	ceva	12	
\.


--
-- Name: abonament_membership_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.abonament_membership_id_seq', 5, true);


--
-- Name: echipamente_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.echipamente_id_seq', 7, true);


--
-- Name: furnizori_id_furnizor_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.furnizori_id_furnizor_seq', 3, true);


--
-- Name: member_member_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.member_member_id_seq', 6, true);


--
-- Name: payment_utilities_id_pay_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.payment_utilities_id_pay_seq', 1, true);


--
-- Name: suplimente_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.suplimente_id_seq', 1, false);


--
-- Name: users_user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_user_id_seq', 11, true);


--
-- Name: abonament abonament_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.abonament
    ADD CONSTRAINT abonament_pkey PRIMARY KEY (membership_id);


--
-- Name: echipamente echipamente_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.echipamente
    ADD CONSTRAINT echipamente_pkey PRIMARY KEY (id);


--
-- Name: furnizori furnizori_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.furnizori
    ADD CONSTRAINT furnizori_pkey PRIMARY KEY (id_furnizor);


--
-- Name: member member_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.member
    ADD CONSTRAINT member_pkey PRIMARY KEY (member_id);


--
-- Name: payment_utilities payment_utilities_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payment_utilities
    ADD CONSTRAINT payment_utilities_pkey PRIMARY KEY (id_pay);


--
-- Name: suplimente suplimente_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.suplimente
    ADD CONSTRAINT suplimente_pkey PRIMARY KEY (id);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);


--
-- Name: echipamente echipamente_id_furnizor_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.echipamente
    ADD CONSTRAINT echipamente_id_furnizor_fkey FOREIGN KEY (id_furnizor) REFERENCES public.furnizori(id_furnizor);


--
-- Name: member member_membership_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.member
    ADD CONSTRAINT member_membership_id_fkey FOREIGN KEY (membership_id) REFERENCES public.abonament(membership_id);


--
-- Name: member member_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.member
    ADD CONSTRAINT member_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(user_id) ON DELETE CASCADE;


--
-- Name: suplimente suplimente_id_furnizor_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.suplimente
    ADD CONSTRAINT suplimente_id_furnizor_fkey FOREIGN KEY (id_furnizor) REFERENCES public.furnizori(id_furnizor);


--
-- PostgreSQL database dump complete
--

