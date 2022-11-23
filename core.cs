using System;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SuperCore
{
    /// <summary>
    /// A classe para criar objeto do tipo URL
    /// Verifica na listagem de dominios se é um dominio valido
    /// fonte da listagem de dominios http://www.iana.org/domains/root/db
    /// </summary>
    public class URLAddress
    {
        private string _address;

        private string _protocolo;

        private string _dominio;

        /// <summary>
        /// Inicializa o objeto e valida o endereço para retornar uma url valida, se não for valide retorna um erro.
        /// </summary>
        /// <param name="address">endereço da url</param>
        public URLAddress(string address)
        {
            if (Uri.IsWellFormedUriString(address, UriKind.Absolute))
            {
                bool find = false;

                string[] dom = Enum.GetNames(typeof(Dominios));

                string[] prt = Enum.GetNames(typeof(Protocolos));


                for (int a = 0; a < prt.Length; a++)
                {

                    if (address.ToUpper().StartsWith(prt[a].ToString().Trim()))
                    {
                        foreach (var item in address.Split('.'))
                        {

                            for (int i = 0; i < dom.Length; i++)
                            {
                                if (item.ToString().Equals(dom[i].ToString().Trim()))
                                {

                                    _protocolo = prt[a];

                                    _dominio = dom[i];

                                    find = true;

                                    break;
                                }
                            }

                        }

                        break;
                    }
                }

                if(find)
                    _address = address;
                else
                    throw new ArgumentException("O formato da URL não é válido.");

            }
            else

                throw new ArgumentException("O formato da URL não é válido.");


        }

        

        /// <summary>
        /// retorna a url valida
        /// </summary>
        public override string ToString()
        {
            return _address;
        }

        /// <summary>
        /// retorna o dominio de alto nivel
        /// </summary>
        public string Dominio
        {
            get
            {
                return _dominio;
            }
        }
        
        /// <summary>
        /// retorna o protocolo de rede
        /// </summary>
        public string Protocolo
        {
             get
            {
                return _protocolo;
            }
        }


        public static implicit operator URLAddress(string address)
        {
            // While not technically a requirement; see below why this is done.
            if (address == null)
                return null;

            return new URLAddress(address);
        }


        private enum Dominios
        { 
             aaa
            ,aarp
            ,abb
            ,abbott
            ,abogado
            ,ac
            ,academy
            ,accenture
            ,accountant
            ,accountants
            ,aco
            ,active
            ,actor
            ,ad
            ,ads
            ,adult
            ,ae
            ,aeg
            ,aero
            ,af
            ,afl
            ,ag
            ,agency
            ,ai
            ,aig
            ,airforce
            ,airtel
            ,al
            ,allfinanz
            ,alsace
            ,am
            ,amica
            ,amsterdam
            ,an
            ,analytics
            ,android
            ,ao
            ,apartments
            ,app
            ,apple
            ,aq
            ,aquarelle
            ,ar
            ,aramco
            ,archi
            ,army
            ,arpa
            ,arte
            ,asia
            ,associates
            ,at
            ,attorney
            ,au
            ,auction
            ,audi
            ,audio
            ,author
            ,auto
            ,autos
            ,aw
            ,ax
            ,axa
            ,az
            ,azure
            ,ba
            ,band
            ,bank
            ,bar
            ,barcelona
            ,barclaycard
            ,barclays
            ,bargains
            ,bauhaus
            ,bayern
            ,bb
            ,bbc
            ,bbva
            ,bcn
            ,bd
            ,be
            ,beats
            ,beer
            ,bentley
            ,berlin
            ,best
            ,bet
            ,bf
            ,bg
            ,bh
            ,bharti
            ,bi
            ,bible
            ,bid
            ,bike
            ,bing
            ,bingo
            ,bio
            ,biz
            ,bj
            ,bl
            ,black
            ,blackfriday
            ,bloomberg
            ,blue
            ,bm
            ,bms
            ,bmw
            ,bn
            ,bnl
            ,bnpparibas
            ,bo
            ,boats
            ,boehringer
            ,bom
            ,bond
            ,boo
            ,book
            ,boots
            ,bosch
            ,bostik
            ,bot
            ,boutique
            ,bq
            ,br
            ,bradesco
            ,bridgestone
            ,broadway
            ,broker
            ,brother
            ,brussels
            ,bs
            ,bt
            ,budapest
            ,bugatti
            ,build
            ,builders
            ,business
            ,buy
            ,buzz
            ,bv
            ,bw
            ,by
            ,bz
            ,bzh
            ,ca
            ,cab
            ,cafe
            ,cal
            ,call
            ,camera
            ,camp
            ,cancerresearch
            ,canon
            ,capetown
            ,capital
            ,car
            ,caravan
            ,cards
            ,care
            ,career
            ,careers
            ,cars
            ,cartier
            ,casa
            ,cash
            ,casino
            ,cat
            ,catering
            ,cba
            ,cbn
            ,cc
            ,cd
            ,ceb
            ,center
            ,ceo
            ,cern
            ,cf
            ,cfa
            ,cfd
            ,cg
            ,ch
            ,chanel
            ,channel
            ,chat
            ,cheap
            ,chloe
            ,christmas
            ,chrome
            ,church
            ,ci
            ,cipriani
            ,circle
            ,cisco
            ,citic
            ,city
            ,cityeats
            ,ck
            ,cl
            ,claims
            ,cleaning
            ,click
            ,clinic
            ,clinique
            ,clothing
            ,cloud
            ,club
            ,clubmed
            ,cm
            ,cn
            ,co
            ,coach
            ,codes
            ,coffee
            ,college
            ,cologne
            ,com
            ,commbank
            ,community
            ,company
            ,computer
            ,comsec
            ,condos
            ,construction
            ,consulting
            ,contact
            ,contractors
            ,cooking
            ,cool
            ,coop
            ,corsica
            ,country
            ,coupons
            ,courses
            ,cr
            ,credit
            ,creditcard
            ,creditunion
            ,cricket
            ,crown
            ,crs
            ,cruises
            ,csc
            ,cu
            ,cuisinella
            ,cv
            ,cw
            ,cx
            ,cy
            ,cymru
            ,cyou
            ,cz
            ,dabur
            ,dad
            ,dance
            ,date
            ,dating
            ,datsun
            ,day
            ,dclk
            ,de
            ,dealer
            ,deals
            ,degree
            ,delivery
            ,dell
            ,delta
            ,democrat
            ,dental
            ,dentist
            ,desi
            ,design
            ,dev
            ,diamonds
            ,diet
            ,digital
            ,direct
            ,directory
            ,discount
            ,dj
            ,dk
            ,dm
            ,dnp
            ,docs
            ,dog
            ,doha
            ,domains
            ,doosan
            ,download
            ,drive
            ,durban
            ,dvag
            ,dz
            ,earth
            ,eat
            ,ec
            ,edu
            ,education
            ,ee
            ,eg
            ,eh
            ,email
            ,emerck
            ,energy
            ,engineer
            ,engineering
            ,enterprises
            ,epson
            ,equipment
            ,er
            ,erni
            ,es
            ,esq
            ,estate
            ,et
            ,eu
            ,eurovision
            ,eus
            ,events
            ,everbank
            ,exchange
            ,expert
            ,exposed
            ,express
            ,fage
            ,fail
            ,fairwinds
            ,faith
            ,family
            ,fan
            ,fans
            ,farm
            ,fashion
            ,fast
            ,feedback
            ,ferrero
            ,fi
            ,film
            ,final
            ,finance
            ,financial
            ,firestone
            ,firmdale
            ,fish
            ,fishing
            ,fit
            ,fitness
            ,fj
            ,fk
            ,flights
            ,florist
            ,flowers
            ,flsmidth
            ,fly
            ,fm
            ,fo
            ,foo
            ,football
            ,ford
            ,forex
            ,forsale
            ,forum
            ,foundation
            ,fox
            ,fr
            ,frl
            ,frogans
            ,fund
            ,furniture
            ,futbol
            ,fyi
            ,ga
            ,gal
            ,gallery
            ,game
            ,garden
            ,gb
            ,gbiz
            ,gd
            ,gdn
            ,ge
            ,gea
            ,gent
            ,genting
            ,gf
            ,gg
            ,ggee
            ,gh
            ,gi
            ,gift
            ,gifts
            ,gives
            ,giving
            ,gl
            ,glass
            ,gle
            ,global
            ,globo
            ,gm
            ,gmail
            ,gmo
            ,gmx
            ,gn
            ,gold
            ,goldpoint
            ,golf
            ,goo
            ,goog
            ,google
            ,gop
            ,got
            ,gov
            ,gp
            ,gq
            ,gr
            ,grainger
            ,graphics
            ,gratis
            ,green
            ,gripe
            ,group
            ,gs
            ,gt
            ,gu
            ,gucci
            ,guge
            ,guide
            ,guitars
            ,guru
            ,gw
            ,gy
            ,hamburg
            ,hangout
            ,haus
            ,healthcare
            ,help
            ,here
            ,hermes
            ,hiphop
            ,hitachi
            ,hiv
            ,hk
            ,hm
            ,hn
            ,hockey
            ,holdings
            ,holiday
            ,homedepot
            ,homes
            ,honda
            ,horse
            ,host
            ,hosting
            ,hoteles
            ,hotmail
            ,house
            ,how
            ,hr
            ,hsbc
            ,ht
            ,hu
            ,hyundai
            ,ibm
            ,icbc
            ,ice
            ,icu
            ,id
            ,ie
            ,ifm
            ,iinet
            ,il
            ,im
            ,immo
            ,immobilien
            ,industries
            ,infiniti
            ,info
            ,ing
            ,ink
            ,institute
            ,insurance
            ,insure
            ,international
            ,investments
            ,io
            ,ipiranga
            ,iq
            ,ir
            ,irish
            ,ist
            ,istanbul
            ,it
            ,itau
            ,iwc
            ,jaguar
            ,java
            ,jcb
            ,je
            ,jetzt
            ,jewelry
            ,jlc
            ,jll
            ,jm
            ,jmp
            ,jo
            ,jobs
            ,joburg
            ,jot
            ,joy
            ,jp
            ,jprs
            ,juegos
            ,kaufen
            ,kddi
            ,ke
            ,kfh
            ,kg
            ,kh
            ,ki
            ,kia
            ,kim
            ,kinder
            ,kitchen
            ,kiwi
            ,km
            ,kn
            ,koeln
            ,komatsu
            ,kp
            ,kpn
            ,kr
            ,krd
            ,kred
            ,kw
            ,ky
            ,kyoto
            ,kz
            ,la
            ,lacaixa
            ,lamborghini
            ,lamer
            ,lancaster
            ,land
            ,landrover
            ,lasalle
            ,lat
            ,latrobe
            ,law
            ,lawyer
            ,lb
            ,lc
            ,lds
            ,lease
            ,leclerc
            ,legal
            ,lexus
            ,lgbt
            ,li
            ,liaison
            ,lidl
            ,life
            ,lifestyle
            ,lighting
            ,like
            ,limited
            ,limo
            ,lincoln
            ,linde
            ,link
            ,live
            ,living
            ,lixil
            ,lk
            ,loan
            ,loans
            ,lol
            ,london
            ,lotte
            ,lotto
            ,love
            ,lr
            ,ls
            ,lt
            ,ltd
            ,ltda
            ,lu
            ,lupin
            ,luxe
            ,luxury
            ,lv
            ,ly
            ,ma
            ,madrid
            ,maif
            ,maison
            ,man
            ,management
            ,mango
            ,market
            ,marketing
            ,markets
            ,marriott
            ,mba
            ,mc
            ,md
            ,me
            ,med
            ,media
            ,meet
            ,melbourne
            ,meme
            ,memorial
            ,men
            ,menu
            ,meo
            ,mf
            ,mg
            ,mh
            ,miami
            ,microsoft
            ,mil
            ,mini
            ,mk
            ,ml
            ,mm
            ,mma
            ,mn
            ,mo
            ,mobi
            ,mobily
            ,moda
            ,moe
            ,moi
            ,mom
            ,monash
            ,money
            ,montblanc
            ,mormon
            ,mortgage
            ,moscow
            ,motorcycles
            ,mov
            ,movie
            ,movistar
            ,mp
            ,mq
            ,mr
            ,ms
            ,mt
            ,mtn
            ,mtpc
            ,mtr
            ,mu
            ,museum
            ,mutuelle
            ,mv
            ,mw
            ,mx
            ,my
            ,mz
            ,na
            ,nadex
            ,nagoya
            ,name
            ,navy
            ,nc
            ,ne
            ,nec
            ,net
            ,netbank
            ,network
            ,neustar
            ,news
            ,nexus
            ,nf
            ,ng
            ,ngo
            ,nhk
            ,ni
            ,nico
            ,ninja
            ,nissan
            ,nl
            ,no
            ,nokia
            ,norton
            ,nowruz
            ,np
            ,nr
            ,nra
            ,nrw
            ,ntt
            ,nu
            ,nyc
            ,nz
            ,obi
            ,office
            ,okinawa
            ,om
            ,omega
            ,one
            ,ong
            ,onl
            ,online
            ,ooo
            ,oracle
            ,orange
            ,org
            ,organic
            ,origins
            ,osaka
            ,otsuka
            ,ovh
            ,pa
            ,page
            ,panerai
            ,paris
            ,pars
            ,partners
            ,parts
            ,party
            ,pe
            ,pet
            ,pf
            ,pg
            ,ph
            ,pharmacy
            ,philips
            ,photo
            ,photography
            ,photos
            ,physio
            ,piaget
            ,pics
            ,pictet
            ,pictures
            ,pid
            ,pin
            ,ping
            ,pink
            ,pizza
            ,pk
            ,pl
            ,place
            ,play
            ,playstation
            ,plumbing
            ,plus
            ,pm
            ,pn
            ,pohl
            ,poker
            ,porn
            ,post
            ,pr
            ,praxi
            ,press
            ,pro
            ,prod
            ,productions
            ,prof
            ,properties
            ,property
            ,protection
            ,ps
            ,pt
            ,pub
            ,pw
            ,py
            ,qa
            ,qpon
            ,quebec
            ,racing
            ,re
            ,read
            ,realtor
            ,realty
            ,recipes
            ,red
            ,redstone
            ,redumbrella
            ,rehab
            ,reise
            ,reisen
            ,reit
            ,ren
            ,rent
            ,rentals
            ,repair
            ,report
            ,republican
            ,rest
            ,restaurant
            ,review
            ,reviews
            ,rexroth
            ,rich
            ,ricoh
            ,rio
            ,rip
            ,ro
            ,rocher
            ,rocks
            ,rodeo
            ,room
            ,rs
            ,rsvp
            ,ru
            ,ruhr
            ,run
            ,rw
            ,rwe
            ,ryukyu
            ,sa
            ,saarland
            ,safe
            ,safety
            ,sakura
            ,sale
            ,salon
            ,samsung
            ,sandvik
            ,sandvikcoromant
            ,sanofi
            ,sap
            ,sapo
            ,sarl
            ,sas
            ,saxo
            ,sb
            ,sbs
            ,sc
            ,sca
            ,scb
            ,schaeffler
            ,schmidt
            ,scholarships
            ,school
            ,schule
            ,schwarz
            ,science
            ,scor
            ,scot
            ,sd
            ,se
            ,seat
            ,security
            ,seek
            ,sener
            ,services
            ,seven
            ,sew
            ,sex
            ,sexy
            ,sfr
            ,sg
            ,sh
            ,sharp
            ,shell
            ,shia
            ,shiksha
            ,shoes
            ,show
            ,shriram
            ,si
            ,singles
            ,site
            ,sj
            ,sk
            ,ski
            ,sky
            ,skype
            ,sl
            ,sm
            ,smile
            ,sn
            ,sncf
            ,so
            ,soccer
            ,social
            ,software
            ,sohu
            ,solar
            ,solutions
            ,sony
            ,soy
            ,space
            ,spiegel
            ,spreadbetting
            ,sr
            ,srl
            ,ss
            ,st
            ,stada
            ,star
            ,starhub
            ,statefarm
            ,statoil
            ,stc
            ,stcgroup
            ,stockholm
            ,storage
            ,studio
            ,study
            ,style
            ,su
            ,sucks
            ,supplies
            ,supply
            ,support
            ,surf
            ,surgery
            ,suzuki
            ,sv
            ,swatch
            ,swiss
            ,sx
            ,sy
            ,sydney
            ,symantec
            ,systems
            ,sz
            ,tab
            ,taipei
            ,tatamotors
            ,tatar
            ,tattoo
            ,tax
            ,taxi
            ,tc
            ,tci
            ,td
            ,team
            ,tech
            ,technology
            ,tel
            ,telefonica
            ,temasek
            ,tennis
            ,tf
            ,tg
            ,th
            ,thd
            ,theater
            ,theatre
            ,tickets
            ,tienda
            ,tips
            ,tires
            ,tirol
            ,tj
            ,tk
            ,tl
            ,tm
            ,tn
            ,to
            ,today
            ,tokyo
            ,tools
            ,top
            ,toray
            ,toshiba
            ,tours
            ,town
            ,toyota
            ,toys
            ,tp
            ,tr
            ,trade
            ,trading
            ,training
            ,travel
            ,travelers
            ,travelersinsurance
            ,trust
            ,trv
            ,tt
            ,tui
            ,tushu
            ,tv
            ,tw
            ,tz
            ,ua
            ,ubs
            ,ug
            ,uk
            ,um
            ,university
            ,uno
            ,uol
            ,us
            ,uy
            ,uz
            ,va
            ,vacations
            ,vana
            ,vc
            ,ve
            ,vegas
            ,ventures
            ,verisign
            ,versicherung
            ,vet
            ,vg
            ,vi
            ,viajes
            ,video
            ,villas
            ,vin
            ,vip
            ,virgin
            ,vision
            ,vista
            ,vistaprint
            ,viva
            ,vlaanderen
            ,vn
            ,vodka
            ,vote
            ,voting
            ,voto
            ,voyage
            ,vu
            ,wales
            ,walter
            ,wang
            ,wanggou
            ,watch
            ,watches
            ,webcam
            ,weber
            ,website
            ,wed
            ,wedding
            ,weir
            ,wf
            ,whoswho
            ,wien
            ,wiki
            ,williamhill
            ,win
            ,windows
            ,wine
            ,wme
            ,work
            ,works
            ,world
            ,ws
            ,wtc
            ,wtf
            ,xbox
            ,xerox
            ,xin
            ,xperia
            ,xxx
            ,xyz
            ,yachts
            ,yamaxun
            ,yandex
            ,ye
            ,yodobashi
            ,yoga
            ,yokohama
            ,youtube
            ,yt
            ,za
            ,zara
            ,zero
            ,zip
            ,zm
            ,zone
            ,zuerich
            ,zw
        }

          
        
        private enum Protocolos
        {
            HTTP, FTP, SSH, HTTPS, TELNET, SMTP, POP3, IMAP, DNS

        }



    }
    
    /// <summary>
    /// A classe para criar objeto do tipo IP
    /// </summary>
    public class IP
    {

        private System.Net.IPAddress _ip;

        /// <summary>
        /// Inicializa o objeto e valida o endereço de IP para retornar uma IP valido, se não for valido retorna um erro.
        /// </summary>
        /// <param name="ip">numero ip</param>
        public IP(string ip)
        {
            System.Net.IPAddress myAddress;

            if (System.Net.IPAddress.TryParse(ip, out myAddress))
                _ip = myAddress;

            else
                throw new ArgumentException("Não é um numero de IP valido.");

        }



        /// <summary>
        /// retorna a url valida
        /// </summary>
        public override string ToString()
        {
            return _ip.Address.ToString();
        }



        public static implicit operator IP(string ip)
        {
            // While not technically a requirement; see below why this is done.
            if (ip == null)
                return null;

            return new IP(ip);
        }


        /// <summary>
        /// Retorna o IP da Conexao do Cliente
        /// </summary>
        /// <returns>string com o IP do Cliente</returns>
        //public static string getClientIP()
        //{
        //    // Conexão utilizando proxy 
        //    System.Net.ServicePointManager.Expect100Continue = false;


        //    string ipUser = string.Empty;
        //    try
        //    {
        //        if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
        //        {
        //            // Conexão sem utilizar proxy 
        //            ipUser = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //        }
        //        else
        //        {
        //            ipUser = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //        }
        //    }
        //    catch
        //    {
        //        ipUser = string.Empty;
        //    }

        //    // Retornando o IP capturado que estava guardado na variável de servidor 
        //    return ipUser;
        //}

        ///// <summary>
        ///// metodo que retorna informações de localidade do ip
        ///// </summary>
        //public geolocation.geoInformation getLocation()
        //{

        //   return geolocation.get(_ip.ToString());           


        //}


        /// <summary>
        /// metodo que verifica se o numero de IP pertence a uma lista de numeros de ip permitidos
        /// </summary>
        /// <param name="SIPlist">lista de numeros de ip permitidos seperados por virgula. Exemplo: "10.2.5.41,192.168.0.22"</param>
        /// <returns>True, se o numero de IP pertence a lista de IPs fornecida.</returns>
        public bool IsAllowedIP(string SIPlist)
        {
            bool allowed = false;

            var splitSingleIPs = SIPlist.Split(',');
            foreach (string ip in splitSingleIPs)
            {
                try
                {
                        IP newip = new IP(ip);
                }
                catch
                {
                    throw new ArgumentException("Não é um numero de IP valido: "+ip.ToString());
                }

                if (_ip.ToString() == ip)
                    allowed= true;

                

            }

            return allowed;

        }

        /// <summary>
        /// metodo que verifica se o numero de IP pertence a uma lista de numeros de ips bloqueados
        /// </summary>
        /// <param name="SIPlist">lista de numeros de ip bloqueados seperados por virgula. Exemplo: "10.2.5.41,192.168.0.22"</param>
        /// <returns>True, se o numero de IP pertence a lista de IPs fornecida.</returns>
        public bool IsDeniedIP(string SIPlist)
        {

            bool Denied = false;

            var splitSingleIPs = SIPlist.Split(',');

            foreach (string ip in splitSingleIPs)
            {
                try
                {
                    IP newip = new IP(ip);
                }
                catch
                {
                    throw new ArgumentException("Não é um numero de IP valido: " + ip);
                }

                if (_ip.ToString() == ip)
                    Denied = true;



            }
            return Denied;

        }

    }

    /// <summary>
    /// A classe para criar objeto do tipo email
    /// </summary>
    public class EMailAddress
    {
        private string _address;

        /// <summary>
        /// metodo statico que Valida o endereço de email
        /// </summary>
        /// <param name="email">endereço de email</param>
        /// <returns>True, se o email é válido; False, se for inválido.</returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// retorna se o endereço de email é valido
        /// </summary>
        /// <param name="email">endereço de email</param>
        /// <returns>True, se o email é válido; False, se for inválido.</returns>
        public bool IsValid()
        {
           return IsValidEmail(_address);

        }


        public override string ToString()
        {
            return _address;
        }


        /// <summary>
        /// Inicializa o objeto de email
        /// </summary>
        /// <param name="address">endereço de email</param>
        public EMailAddress(string address)
        {
            //if (IsValidEmail(address))

                _address = address;

            //else

            //    throw new ArgumentException("O endereço de e-mail não é válido.");


        }

        public static implicit operator EMailAddress(string address)
        {
            // While not technically a requirement; see below why this is done.
            if (address == null)
                return null;

            return new EMailAddress(address);
        }
    }

    /// <summary>
    /// A classe para criar objeto do tipo CPF
    /// </summary>
    public struct CPF
    {

        private string  _cpf;

        /// <summary>
        /// Valida se o numero do CPF é valido
        /// </summary>
        /// <param name="cpf">numero de CPF</param>
        /// <returns>True, se o cpf é válido; False, se for inválido.</returns>
        public static bool IsValidCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int i = 0; i < 10; i++)
            {
                if (cpf.All(c => c.Equals(i.ToString().First())))
                    return false;

            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Valida se o numero do CPF é valido
        /// </summary>
        /// <param name="cpf">numero de CPF</param>
        /// <returns>True, se o cpf é válido; False, se for inválido.</returns>
        public bool IsValid()
        {
            return IsValidCPF(_cpf);
        }


        /// <summary>
        /// Inicializa o objeto de CPF
        /// </summary>
        /// <param name="cpf">numero do CPF</param>
        public CPF(string cpf)
        {
            if (cpf == null)
                _cpf = string.Empty;
            else
                _cpf = cpf.RemoverLetraseCaracteresEspeciais();
           

        }

        public static implicit operator CPF(string cpf)
        {
            // While not technically a requirement; see below why this is done.
            //if (cpf == null)
            //    return null;

            return new CPF(cpf);
        }

        /// <summary>
        /// retorna o cpf valido
        /// </summary>
        public override string ToString()
        {
            return _cpf;
        }

        
        /// <summary>
        /// retorna o cpf formatado  000.000.000-00
        /// </summary>
        public string Format()
        {
            return string.Format("{0:[###.###.###-##]}", _cpf);
            //return _cpf.Substring(0, 3) + "." + _cpf.Substring(3, 3) + "." + _cpf.Substring(6, 3) + "-" + _cpf.Substring(9, 2);

        }

    }

    /// <summary>
    /// A classe para criar objeto do tipo CNPJ
    /// </summary>
    public struct CNPJ {

        private string _cnpj;

        /// <summary>
        /// Valida se o numero do CNPJ é valido metodo estatico
        /// </summary>
        /// <param name="cnpj">numero de CNPJ</param>
        /// <returns>True, se o CNPJ é válido; False, se for inválido.</returns>
        public static bool IsValidCNPJ(string cnpj)
        {

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;


            for (int i = 0; i < 10; i++)
            {
                if (cnpj.All(c => c.Equals(i.ToString().First())))
                    return false;
                        
            }

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        /// <summary>
        /// Retorna se o numero do CNPJ é valido
        /// </summary>
        /// <param name="cnpj">numero de CNPJ</param>
        /// <returns>True, se o CNPJ é válido; False, se for inválido.</returns>
        public bool IsValid()
        {
            return IsValidCNPJ(_cnpj);
        }

        /// <summary>
        /// Inicializa o objeto de CNPJ
        /// </summary>
        /// <param name="cnpj">numero do CNPJ</param>
        public CNPJ(string cnpj)
        {

            if (cnpj == null)
                _cnpj = string.Empty;
            else
                _cnpj = cnpj.RemoverLetraseCaracteresEspeciais();
            //else
            //    throw new ArgumentException("O número do CNPJ não é válido.");

        }

        /// <summary>
        /// retorna o cnpj valido
        /// </summary>
        public override string ToString()
        {
            return _cnpj;
        }

        public static implicit operator CNPJ(string cnpj)
        {
                       
                return new CNPJ(cnpj);
        }

        /// <summary>
        /// retorna o cnpj formatado  000.000.000/0000-00
        /// </summary>
        public string Format()
        {
            return string.Format("{0:[##.###.###/####-##]}", _cnpj);
            //return _cnpj.Substring(0, 3) + "." + _cnpj.Substring(3, 3) + "." + _cnpj.Substring(6, 3) + "/" + _cnpj.Substring(9, 4) + "-" + _cnpj.Substring(13, 2);

        }

    }

    /// <summary>
    /// A classe para criar objeto do tipo PIS
    /// </summary>
    public struct PIS
    {
        private string _pis;

        /// <summary>
        /// Validar o numero do PIS
        /// </summary>
        /// <param name="pis">String representante do PIS a ser validado</param>
        /// <returns></returns>
        public static bool IsValidPIS(string pis)
        {
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            if (pis.Trim().Length != 11)
                return false;
            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            return pis.EndsWith(resto.ToString());
        }


        /// <summary>
        /// Validar o numero do PIS
        /// </summary>
        /// <param name="pis">String representante do PIS a ser validado</param>
        /// <returns></returns>
        public bool IsValid()
        {
            return IsValidPIS(_pis);
        }

        /// <summary>
        /// inicializar o objeto do tipo PIS
        /// </summary>
        /// <param name="pis">string que representa o numero PIS</param>
        public PIS(string pis)
        {

            //if (IsValidPIS(pis))
                _pis = pis.RemoverLetraseCaracteresEspeciais();
            //else
            //    throw new ArgumentException("O número do PIS não é válido.");

        }

        /// <summary>
        /// retorna o numero PIS
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _pis;
        }

        public static implicit operator PIS(string pis)
        {
            // While not technically a requirement; see below why this is done.
            if (pis == null)
                return null;

            return new PIS(pis);
        }


    }

    /// <summary>
    /// A classe para criar objeto do tipo CNS (Cartão Nacional de saúde)
    /// </summary>
    public struct CNS
    {
        private string _cns;

        /// <summary>
        /// inicializa o objeto do tipo CNS
        /// </summary>
        /// <param name="cns">string que representa o numero CNS</param>
        public CNS(string cns)
        {
            //if (IsValidCNS(cns))
                _cns = cns.RemoverLetraseCaracteresEspeciais();
            //else
            //    throw new ArgumentException("O número do CNS não é válido.");

        }

        /// <summary>
        /// retorna o numero CNS
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _cns;
        } 
        
        
          /// <summary>
          /// Verifica se a string informada é um numero de cns valido
          /// </summary>
          /// <param name="cns">string que representa o numero de cns</param>
          /// <returns></returns>
        public  bool IsValid()
        {
            return IsValidCNS(_cns);
        }



        /// <summary>
        /// metodo statico que Verifica se a string informada é um numero de cns valido
        /// </summary>
        /// <param name="cns">string que representa o numero de cns</param>
        /// <returns></returns>
        public static bool IsValidCNS(string cns)
        {
            bool result = false;

            cns = cns.Trim();


            if ((cns.Substring(0, 1) == "8") || (cns.Substring(0, 1) == "9"))
            {
                result = chkNumeroProvisorio(cns);
            }
            else
            {
                result = chkNumeroDefinitivo(cns);
            }

            return result;
        }


        private static bool chkNumeroProvisorio(string cns)
        {
            bool result = false;

            try
            {
                cns = cns.Trim();

                if (cns.Trim().Length == 15)
                {



                    float resto, soma;

                    soma = ((Convert.ToInt64(cns.Substring(0, 1))) * 15) +
                            ((Convert.ToInt64(cns.Substring(1, 1))) * 14) +
                            ((Convert.ToInt64(cns.Substring(2, 1))) * 13) +
                            ((Convert.ToInt64(cns.Substring(3, 1))) * 12) +
                            ((Convert.ToInt64(cns.Substring(4, 1))) * 11) +
                            ((Convert.ToInt64(cns.Substring(5, 1))) * 10) +
                            ((Convert.ToInt64(cns.Substring(6, 1))) * 9) +
                            ((Convert.ToInt64(cns.Substring(7, 1))) * 8) +
                            ((Convert.ToInt64(cns.Substring(8, 1))) * 7) +
                            ((Convert.ToInt64(cns.Substring(9, 1))) * 6) +
                            ((Convert.ToInt64(cns.Substring(10, 1))) * 5) +
                            ((Convert.ToInt64(cns.Substring(11, 1))) * 4) +
                            ((Convert.ToInt64(cns.Substring(12, 1))) * 3) +
                            ((Convert.ToInt64(cns.Substring(13, 1))) * 2) +
                            ((Convert.ToInt64(cns.Substring(14, 1))) * 1);

                    resto = soma % 11;

                    result = (resto == 0);
                }

            }
            catch (Exception)
            {
                result = false;
            }


            return result;
        }


        private static bool chkNumeroDefinitivo(string cns)
        {
            bool result = false;

            try
            {
                if (cns.Trim().Length == 15)
                {

                    float resto, soma, dv;

                    string pis = string.Empty;
                    string resultado = string.Empty;

                    pis = cns.Substring(0, 11);

                    soma = ((Convert.ToInt64(pis.Substring(0, 1))) * 15) +
                            ((Convert.ToInt64(pis.Substring(1, 1))) * 14) +
                            ((Convert.ToInt64(pis.Substring(2, 1))) * 13) +
                            ((Convert.ToInt64(pis.Substring(3, 1))) * 12) +
                            ((Convert.ToInt64(pis.Substring(4, 1))) * 11) +
                            ((Convert.ToInt64(pis.Substring(5, 1))) * 10) +
                            ((Convert.ToInt64(pis.Substring(6, 1))) * 9) +
                            ((Convert.ToInt64(pis.Substring(7, 1))) * 8) +
                            ((Convert.ToInt64(pis.Substring(8, 1))) * 7) +
                            ((Convert.ToInt64(pis.Substring(9, 1))) * 6) +
                            ((Convert.ToInt64(pis.Substring(10, 1))) * 5);


                    resto = soma % 11;
                    dv = 11 - resto;

                    if (dv == 11)
                    {
                        dv = 0;
                    }

                    if (dv == 10)
                    {
                        soma = ((Convert.ToInt64(pis.Substring(0, 1))) * 15) +
                                ((Convert.ToInt64(pis.Substring(1, 1))) * 14) +
                                ((Convert.ToInt64(pis.Substring(2, 1))) * 13) +
                                ((Convert.ToInt64(pis.Substring(3, 1))) * 12) +
                                ((Convert.ToInt64(pis.Substring(4, 1))) * 11) +
                                ((Convert.ToInt64(pis.Substring(5, 1))) * 10) +
                                ((Convert.ToInt64(pis.Substring(6, 1))) * 9) +
                                ((Convert.ToInt64(pis.Substring(7, 1))) * 8) +
                                ((Convert.ToInt64(pis.Substring(8, 1))) * 7) +
                                ((Convert.ToInt64(pis.Substring(9, 1))) * 6) +
                                ((Convert.ToInt64(pis.Substring(10, 1))) * 5) + 2;

                        resto = soma % 11;
                        dv = 11 - resto;
                        resultado = pis + "001" + Convert.ToString(Convert.ToInt16(dv)).Trim();
                    }
                    else
                    {
                        resultado = pis + "000" + Convert.ToString(Convert.ToInt16(dv)).Trim();
                    }


                    result = cns.Equals(resultado);

                }
            }
            catch (Exception)
            {
                result = false;
            }


            return result;
        }

    }

    /// <summary>
    /// A classe para criar objeto do tipo CEP
    /// </summary>
    public struct CEP
    {
        private string _cep;

        /// <summary>
        /// metodo statico que verifica se o numero de cep tem um formato valido
        /// </summary>
        /// <param name="cep">string que representa o numero de cep</param>
        /// <returns></returns>
        public static bool IsValidCEP(string cep)
        {
            //if (cep.Length == 8)
            //{
            //    cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
            //    //txt.Text = cep;
            //}
            return System.Text.RegularExpressions.Regex.IsMatch(cep, ("[0-9]{8}"));
        }

        /// <summary>
        /// verifica se o numero de cep tem um formato valido
        /// </summary>
        /// <param name="cep">string que representa o numero de cep</param>
        /// <returns></returns>
        public  bool IsValid()
        {
            return IsValidCEP(_cep);

        }


        /// <summary>
        /// inicializa o objeto do tipo Cep
        /// </summary>
        /// <param name="cep">string que representa o cep</param>
        public CEP(string cep)
        {

            //if (IsValidCEP(cep.RemoverLetraseCaracteresEspeciais()))
                _cep = cep.RemoverLetraseCaracteresEspeciais();
            //else
            //    throw new ArgumentException("O CEP não é válido.");

        }


        /// <summary>
        /// retorna o cep sem formatação, somente numeros
        /// </summary>
        public override string ToString()
        {
            return _cep.RemoverLetraseCaracteresEspeciais();
        }

        /// <summary>
        /// retorna o cep formatado  00000-000
        /// </summary>
        public string Format()
        {

            return string.Format("{0:[#####-###]}", _cep);

        }

        public static implicit operator CEP(string cep)
        {
            // While not technically a requirement; see below why this is done.
            if (cep == null)
                return null;

            return new CEP(cep);
        }

    }

   
    /// <summary>
    /// A classe para criar objeto do tipo senha
    /// </summary>
    public class Senha
    {
        
            
        /// <summary>
        /// todos caracteres validos na criação da senha
        /// </summary>
        private const string SenhaCaracteresValidos = "ABCDEFGHIJKLMNOPQRSTUVXYWZabcdefghijklmnopqrstuvwxyz1234567890@#!?";

        /// <summary>
        /// caracteres minusculos validos na criação da senha
        /// </summary>
        private const string SenhaMinusculas = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// caracteres maiusculos validos na criação da senha
        /// </summary>
        private const string SenhaMaiusculas = "ABCDEFGHIJKLMNOPQRSTUVXYWZ";

        /// <summary>
        /// caracteres numericos validos na criação da senha
        /// </summary>
        private const string SenhaDigitos = "1234567890";

        /// <summary>
        /// caracteres especiais validos na criação da senha
        /// </summary>
        private const string SenhaCaracteres = "@#!?$*";

        private SecureString  _senha;

        private ForcaDaSenha _forca;

        private int _tamanhoMin, _tamanhoMax, _maiusculas, _digitos, _carcteres;


        /// <summary>
        /// Gera senha aleatoria
        /// </summary>
        /// <param name="tamanho">Tamanho total da senha</param>
        /// <param name="maiusculas">quantidade minima de letras maiusculas na senha</param>
        /// <param name="digitos">quantidade minima de caracteres numéricos na senha</param>
        /// <param name="caracteres">quantida minima de caracteres especiais na senha @#!?$*</param>
        /// <returns></returns>
        public static string gerar(int tamanho, int maiusculas=0, int digitos=0, int caracteres=0)
        {

            if ((maiusculas + digitos + caracteres) > tamanho)
                throw new ArgumentException("O tamanho total da senha é menor que a soma das quantidades de maiuscula, digitos e/ou caracteres especiais.");


            if((tamanho - (maiusculas + digitos + caracteres))==tamanho)
            {
                return getSequence(tamanho, SenhaCaracteresValidos);
            }
            else
            {
                StringBuilder s = new StringBuilder();

                if (maiusculas > 0)
                    s.Append(getSequence(maiusculas, SenhaMaiusculas));

                if (digitos > 0)
                    s.Append(getSequence(digitos, SenhaDigitos));

                if (caracteres > 0)
                    s.Append(getSequence(caracteres, SenhaCaracteres));

                if ((tamanho - (maiusculas + digitos + caracteres)) > 0)
                    s.Append(getSequence(tamanho - (maiusculas + digitos + caracteres), SenhaMinusculas));

                // The random number sequence
                Random num = new Random();

                // Create new string from the reordered char array
                string rand = new string(s.ToString().ToCharArray().OrderBy(x => (num.Next(2) % 2) == 0).ToArray());

                return rand;


            }



        }

        /// <summary>
        /// verifica se o numero de cep tem um formato valido
        /// </summary>
        /// <param name="cep">string que representa o numero de cep</param>
        /// <returns></returns>
        public bool IsValid()
        {
            bool v = true;

            if (this.ToString().Length < _tamanhoMin)
                v = false;

            if (this.ToString().Length > _tamanhoMax)
                v = false;

            if (  (this.ToString().Length- Regex.Replace(this.ToString(), "[A-Z]", "").Length)  < _maiusculas && _maiusculas>0)
                v = false;

            if ((this.ToString().Length - Regex.Replace(this.ToString(), "[0-9]", "").Length) < _digitos && _digitos>0)
                v = false;
                                                                    
            if ((this.ToString().Length - Regex.Replace(this.ToString(), "[@#!?$*]", "").Length) < _carcteres && _carcteres > 0)
                v = false;


            return v;
        }


        private static string getSequence(int tamanho, string carct)
        {
            //Aqui pego o valor máximo de caracteres para gerar a senha
            int valormaximo = carct.Length;

            //Criamos um objeto do tipo randon
            Random random = new Random(DateTime.Now.Millisecond);

            //Criamos a string que montaremos a senha
            StringBuilder senha = new StringBuilder(tamanho);

            //Fazemos um for adicionando os caracteres a senha
            for (int i = 0; i < tamanho; i++)
                senha.Append(carct[random.Next(0, valormaximo)]);

            //retorna a senha
            return senha.ToString();

        }


        /// <summary>
        /// inicializa um objeto senha com uma senha gerada aleatoriamente com o tamanho informado
        /// </summary>
        /// <param name="tamanhoMin">Tamanho Minimo da senha, qdo for gerar uma senha aleatória o tamanho mínimo será o tamanho da senha</param>
        /// <param name="maiusculas">quantidade minima de letras maiusculas</param>
        /// <param name="digitos">quantidade minima de caracteres numericos</param>
        /// <param name="caracteres">quantidade minima de caracteres especiais @#!?$*</param>
        /// <param name="senha">se senha não for informada, será gerada uma senha com base nos demais parametros</param>
        /// <param name="tamanhoMax">Tamanho Maximo da senha</param>
        public Senha(int tamanhoMin, int maiusculas = 0, int digitos = 0, int caracteres = 0, string senha=null, int tamanhoMax = 0)
        {
            if (string.IsNullOrEmpty(senha) )
                _senha = SecureStringExtensions.ToSecureString(gerar(tamanhoMin, maiusculas, digitos, caracteres));
            else
                _senha = SecureStringExtensions.ToSecureString(senha);

            _tamanhoMin = tamanhoMin;
            _tamanhoMax = tamanhoMax;
            _maiusculas = maiusculas;
            _digitos = digitos;
            _carcteres = caracteres;


            _forca = GetForcaDaSenha();
        }


        /// <summary>
        /// inicializa objeto senha
        /// </summary>
        /// <param name="s">string que representa a senha</param>
        //public Senha(string s)
        //{
        //    _senha = SecureStringExtensions.ToSecureString(s);
        //    _forca = GetForcaDaSenha();
        //}

        /// <summary>
        /// força da senha
        /// </summary>
        public ForcaDaSenha forca { get; set; }

        /// <summary>
        /// Gera o hash da senha
        /// </summary>
        /// <returns>string do hash</returns>
        public string HASHmd5()
        {
            StringBuilder senha = new StringBuilder();

            MD5 md5 = MD5.Create();
            byte[] entrada = Encoding.ASCII.GetBytes(SecureStringExtensions.ToUnsecureString(_senha));
            byte[] hash = md5.ComputeHash(entrada);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                senha.Append(hash[i].ToString("X2"));
            }
            return senha.ToString();

        }

        /// <summary>
        /// retorna a string que representa a senha
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SecureStringExtensions.ToUnsecureString(_senha);
        }


       
        #region checarfocasenha

        /// <summary>
        /// retorna o numero de pontos que indica a qualidade da senha
        /// </summary>
        /// <returns>numero inteiro</returns>
        public int geraPontosSenha()
        {
            string s =SecureStringExtensions.ToUnsecureString(_senha);

            if (s == null) return 0;
            int pontosPorTamanho = GetPontoPorTamanho(s);
            int pontosPorMinusculas = GetPontoPorMinusculas(s);
            int pontosPorMaiusculas = GetPontoPorMaiusculas(s);
            int pontosPorDigitos = GetPontoPorDigitos(s);
            int pontosPorSimbolos = GetPontoPorSimbolos(s);
            int pontosPorRepeticao = GetPontoPorRepeticao(s);
            return pontosPorTamanho + pontosPorMinusculas + pontosPorMaiusculas + pontosPorDigitos + pontosPorSimbolos - pontosPorRepeticao;
        }

        private int GetPontoPorTamanho(string senha)
        {
            return Math.Min(10, senha.Length) * 6;
        }

        private int GetPontoPorMinusculas(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[a-z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorMaiusculas(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[A-Z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorDigitos(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[0-9]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorSimbolos(string senha)
        {
            int rawplacar = Regex.Replace(senha, "[a-zA-Z0-9]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorRepeticao(string senha)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(\w)*.*\1");
            bool repete = regex.IsMatch(senha);
            if (repete)
            {
                return 30;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// retorna o nivel de segurança da senha (Inaceitavel, Fraca, Aceitavel, Forte, Segura)
        /// </summary>
        /// <returns></returns>
        public ForcaDaSenha GetForcaDaSenha()
        {
            int placar = geraPontosSenha();

            if (placar < 50)
                return ForcaDaSenha.Inaceitavel;
            else if (placar < 60)
                return ForcaDaSenha.Fraca;
            else if (placar < 80)
                return ForcaDaSenha.Aceitavel;
            else if (placar < 100)
                return ForcaDaSenha.Forte;
            else
                return ForcaDaSenha.Segura;
        }

        /// <summary>
        /// niveis de força da senha
        /// </summary>
        public enum ForcaDaSenha
        {
            Inaceitavel,
            Fraca,
            Aceitavel,
            Forte,
            Segura
        }
        #endregion

    }

    /// <summary>
    /// A classe para criar objeto do tipo sexo
    /// </summary>
    public struct Sexo
    {
        private char _sexo;

        /// <summary>
        /// enumerador com os valores aceitos
        /// </summary>
        public enum esexo
        {
            F, M
        }

        /// <summary>
        /// retorna o sexo
        /// </summary>
        /// <returns></returns>
        public char getSexo()
        {
            return _sexo;

        }

        /// <summary>
        /// inicializa objeto do tipo sexo
        /// </summary>
        /// <param name="s">caracter que indica o sexo</param>
        public Sexo(char s)
        {
            if (Enum.IsDefined(typeof(esexo), s))
            {
                _sexo = s;
            }
            else
                throw new ArgumentException("O Sexo não é válido. valores aceitos: F(feminino) ou M(masculino)");
        }

        /// <summary>
        /// retorna o valor que indica o sexo Feminino ou Masculino
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _sexo == 'F' ? "Feminino" : "Masculino";
        }

        /// <summary>
        /// retorna o genero, Mulher ou Homem
        /// </summary>
        /// <returns></returns>
        public  string Genero()
        {
            return _sexo == 'F' ? "Mulher" : "Homem";
        }

        public static implicit operator Sexo(char s)
        {

            return new Sexo(s);
        }

    }

    /// <summary>
    /// A classe para criar objeto do tipo identidade
    /// </summary>
    public class Identidade
    {
        /// <summary>
        /// numero do documento de identidade
        /// </summary>
        public string numero { get; set; }

        /// <summary>
        /// data de emissao do docemento de identidade
        /// </summary>
        public DateTime dataEmissao { get; set; }

        /// <summary>
        /// orgão expedidor do documento de identidade
        /// </summary>
        public eorgao orgaoExpedidor { get; set; }

        /// <summary>
        /// enumerador com valores possíveis para o orgao expedidor
        /// </summary>
        public enum eorgao
        {
            PMMG,
            PCMG,
            CNT,
            FGTS,
            IFP,
            IPF,
            IML,
            MTE,
            MMA,
            MAE,
            MEX,
            POF,
            POM,
            SES,
            SSP,
            SJS,
            SJTS,
            COREN,
            CRA,
            CRAS,
            CRB,
            CRC,
            CRE,
            CREA,
            CRECI,
            CREFIT,
            CRF,
            CRM,
            CRN,
            CRO,
            CRP,
            CRPRE,
            CRQ,
            CRRC,
            CRMV,
            DPF,
            EST,
            ICLA,
            OAB,
            OMB,
            OUT,
            ABNC,
            CSC,
            DPT,
            FIPE,
            FLS,
            GOVGO,
            IICCECF_RO,
            IIMG,
            IGP,
            IPC,
            SDS,
            SEJUSP,
            SESP,
            DST_SEDE,
            SECC,
            DPMAF,
            CGPMAF,
            CNIG,
            SNJ,
            CGPI,
            DIREX,
            DST_SE

        }

        /*
        PMMG – Polícia Militar do Estado de Minas Gerais
        PCMG - Policia Civil do Estado de Minas Gerais
        CNT – Carteira Nacional de Habilitação
        DIC – Diretoria de Identificação Civil
        CTPS – Carteira de Trabaho e Previdência Social
        FGTS - Fundo de Garantia do Tempo de Serviço
        IFP – Instituto Félix Pacheco
        IPF – Instituto Pereira Faustino
        IML – Instituto Médico-Legal
        MTE – Ministério do Trabalho e Emprego
        MMA -  Ministério da Marinha
        MAE – Ministério da Aeronáutica
        MEX – Ministério do Exército
        POF – Polícia Federal
        POM – Polícia Militar
        SES - Carteira de Estrangeiro
        SSP – Secretaria da Segurança Pública
        SJS – Secretaria da Justiça e Segurança
        SJTS – Secretaria da Justiça do Trabalho e Segurança
        COREN – Conselho Regional de Enfermagem
        CRA – Conselho Regional de Administração
        CRAS – Conselho Regional de Assistentes Sociais
        CRB – Conselho Regional de Biblioteconomia
        CRC – Conselho Regional de Contabilidade
        CRE – Conselho Regional de Estatística
        CREA – Conselho Regional de Engenharia Arquitetura e Agronomia
        CRECI – Conselho Regional de Corretores de Imóveis
        CREFIT – Conselho Regional de Fisioterapia e Terapia Ocupacional
        CRF – Conselho Regional de Farmácia
        CRM – Conselho Regional de Medicina
        CRN – Conselho Regional de Nutrição
        CRO – Conselho Regional de Odontologia
        CRP – Conselho Regional de Psicologia
        CRPRE – Conselho Regional de Profissionais de Relações Públicas
        CRQ – Conselho Regional de Química
        CRRC – Conselho Regional de Representantes Comerciais
        CRMV – Conselho Regional de Medicina Veterinária
        DPF – Polícia Federal
        EST – Documentos Estrangeiros
        ICLA – Carteira de Identidade Classista
        OAB – Ordem dos Advogados do Brasil
        OMB – Ordens dos Músicos do Brasil
        IFP – Instituto de Identificação Félix Pacheco
        OUT – Outros Emissores
        ABNC – Academia Brasileira de Neurocirurgia
        CSC - Carteira Sede Carpina de Pernambuco
        DPT – Departamento de Polícia Técnica Geral
        FIPE – Fundação Instituto de Pesquisas Econômicas
        FLS - Fundação Lyndolpho Silva
        GOVGO - Governo do Estado de Goiás
        IICCECF/RO - Instituto de Identificação Civil e Criminal Engrácia da Costa Francisco de Rondônia
        IIMG - Inter-institutional Monitoring Group
        IGP – Instituto Geral de Perícias
        IPC - Índice de Preços ao Consumidor
        SDS – Secretaria de Defesa Social (Pernambuco)
        SEJUSP - Secretaria de Estado de Justiça e Segurança Pública – Mato Grosso
        SESP – Secretaria de Estado da Segurança Pública do Paraná
        DST-SEDE – Programa Municipal DST/Aids
        SECC – Secretaria de Estado da Casa Civil
        DPMAF – Divisão de Polícia Marítima, Área e de Fronteiras
        CGPMAF – Coordenadoria Geral de Polícia Marítima, Aeronáutica e de Fronteiras.
        CNIG – Conselho Nacional de Imigração
        SNJ – Secretaria Nacional de Justiça / Departamento de Estrangeiros
        CGPI/DIREX/DPF – Coordenação Geral de Polícia de Imigração
        CGPI – Coordenação-Geral de Privilégios e Imunidades
        DIREX – Diretoria-Executiva
        DPF – Departamento de Policia Federal
        DST-SE – Centro de Especialidades Médicas de Aracaju
        
        */
    }

    /// <summary>
    /// A classe de coordenadas latitude e longitude
    /// </summary>
    public class Coordenadas
    {
        private readonly double latitude;
        private readonly double longitude;

      
        public double Latitude { get { return latitude; } }
        public double Longitude { get { return longitude; } }

        /// <summary>
        /// inicializa o objeto coordenadas
        /// </summary>
        /// <param name="latitude">double da latitude</param>
        /// <param name="longitude">double da longitude</param>
        public Coordenadas(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        /// <summary>
        /// string que representa as coordenadas geograficas
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0},{1}", Latitude, Longitude);
        }
    }

}
